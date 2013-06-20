using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMarioC : Abstract {	
	public float jumpSpeed = 12f;
	public float gravity = 9.81f;

	private float moveforward=0;
	private float verticalSpeed = 0f;
	private bool flagPathChangeJump=false;
	
	private bool grounded=false;
	private bool jumping=false;
	private bool downing=false;
	private bool stumble=false;
	private bool flying=false;
	private bool movingToFlyGround=false;
	private float vsletAcceleration=10;
	private Transform curStumbleTransform=null;
	
	private int numberOfTransformsToStumble=10;
	private List<Transform> ListToChangeTransforms=new List<Transform>();
	private List<int> ListToChangeLayers=new List<int>();
	
	private int curStumbleTransformLayer;
	//private bool downing=false;
	
	private bool freezed=false;
	
	private float forcex=0;
	
	private Player playerScript;
	
	//timers
	private float glideTimer,timeToGround;
	
	private bool glideFlag=false;
	private bool groundingFlag=false;
	
	float stopTime=0,startstopTime=0;//время остановки
	
	private float heightnormal=1.5f, heightslide=0.8f;
	private CapsuleCollider walkinbearCollider;
	
	private GameObject walkingBear;
	private Transform walkingBearTransform;
	
	private float flyingYsmex=8,groundYsmex=0;
	
	private CharacterController controller;
	// Update is called once per frame
	
	void Start()
	{
		playerScript=GlobalOptions.GetPlayerScript();
		controller = GetComponent<CharacterController>();
		walkingBear=singleTransform.FindChild("WalkingBear").gameObject;
		walkinbearCollider=walkingBear.collider as CapsuleCollider;
		
		walkingBearTransform=walkingBear.transform;
	}
	
	private void ClearStumbleList()
	{
		while((ListToChangeTransforms.Count>numberOfTransformsToStumble&&playerScript.isVodka())||
			(ListToChangeTransforms.Count>0&&!playerScript.isVodka()))
		{
			ListToChangeTransforms[0].gameObject.layer=ListToChangeLayers[0];
			ListToChangeTransforms[0].parent.gameObject.SetActive(true);
			ListToChangeTransforms.RemoveAt(0);
			ListToChangeLayers.RemoveAt(0);
		}
	}
	
	void Update() {
		if(GlobalOptions.gameState==GameStates.GAME||GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			if(GlobalOptions.gameState==GameStates.GAME)
			{
				ClearStumbleList();
			}
			
			AddAllTimes();
		
			if (grounded&&!jumping&&!flagPathChangeJump) {
				verticalSpeed = 0;
			}
			
			flagPathChangeJump=false;
			verticalSpeed -= gravity * Time.deltaTime;
			
			Vector3 right = singleTransform.TransformDirection(Vector3.right);
			Vector3 forward = singleTransform.TransformDirection(Vector3.forward);
			
			//moveforward=Mathf.Lerp(controller.velocity.z,moveforward,Time.deltaTime*1f);
			
			if(freezed)
			{
				moveforward=0;
				forcex=0;
			}
			
			Vector3 movement = moveforward*forward + new Vector3 (0, verticalSpeed, 0) + forcex*right;			
			
			
			movement *= Time.deltaTime;
			// Move the controller
			
			CollisionFlags flags = controller.Move(movement);
			grounded = (flags & CollisionFlags.CollidedBelow) != 0;
			
			stumble = (flags & CollisionFlags.CollidedSides) != 0;
			
			if(stumble&&!flying&&!groundingFlag&&moveforward>0)
			{
				bool needStumble=true;
				if(curStumbleTransform&&curStumbleTransform.name.Contains("Zabiratsa"))
				{
					needStumble=false;
				}
				//под водкой
				if(playerScript.isVodka()&&needStumble)
				{
					ListToChangeTransforms.Add (curStumbleTransform);
					ListToChangeLayers.Add (curStumbleTransform.gameObject.layer);
					needStumble=false;
					curStumbleTransform.gameObject.layer=10;
					playerScript.MakeVodkaBoom();
					
					curStumbleTransform.parent.gameObject.SetActive(false);
				}
				
				if(needStumble)
				{
					playerScript.Stumble(curStumbleTransform);
				}
			}
			
			// We are in jump mode but just became grounded
			if (grounded)
			{
				if(jumping&&!glideFlag)
				{
					if(GlobalOptions.playerStates!=PlayerStates.DIE)
					{
						GlobalOptions.playerStates=PlayerStates.WALK;
					}
				}
				
				if(downing)
				{
					Glide();
				}
				
				if(jumping&&downing)
				{
					downing=false;
					GlobalOptions.playerStates=PlayerStates.DOWN;
				}
				
				jumping = false;
			}
			
			if(!jumping)
			{
				downing=false;
			}
			
			if(glideFlag)
			{
				MakeGlide();
			}
			
			if(movingToFlyGround)
			{
				MoveToFlyGround();
			}
		}
		else
		{
			if(GlobalOptions.gameState==GameStates.PAUSE_MENU)
			{
				if(startstopTime==0)
				{
					startstopTime=Time.time;
				}

				stopTime=Time.time-startstopTime;
			}
		}
	}
	
	private void Glide()
	{
		if(!glideFlag)
		{
			glideFlag=true;
			glideTimer=Time.time;
			walkinbearCollider.height=heightslide;
			playerScript.MoveParticlesDown();
			walkinbearCollider.center=new Vector3(walkinbearCollider.center.x,walkinbearCollider.center.y-(heightnormal-heightslide)/2,walkinbearCollider.center.z);
			GlobalOptions.GetMissionEmmitters().NotifySlide(1);
		}
	}
	
	private void UnMakeGlide()
	{
		if(glideFlag)
		{
			walkinbearCollider.height=heightnormal;
			playerScript.MoveParticlesUp();
			walkinbearCollider.center=new Vector3(walkinbearCollider.center.x,walkinbearCollider.center.y+(heightnormal-heightslide)/2,walkinbearCollider.center.z);
			glideFlag=false;
		}
		downing=false;
		if(GlobalOptions.playerStates!=PlayerStates.DIE)
		{
			GlobalOptions.playerStates=PlayerStates.WALK;
		}
	}
	
	private void MakeGlide()
	{
		if(Time.time-glideTimer>1)
		{
			UnMakeGlide();
		}
	}
	
	public bool isGrounded()
	{
		return grounded;	
	}
	
	public bool isGliding()
	{
		return glideFlag;	
	}
	
	public bool isJumping()
	{
		return jumping;	
	}
	
	public bool isFlying()
	{
		return flying;	
	}
	
	public bool ismovingToFlyGround()
	{
		return movingToFlyGround;	
	}
	
	public void Jump()
	{
		if (grounded&&!jumping&&!flying) {
			UnMakeGlide();
			jumping = true;
			GlobalOptions.GetMissionEmmitters().NotifyJump(1);
			verticalSpeed = jumpSpeed;
			GlobalOptions.playerStates=PlayerStates.JUMP;
		}
	}
	
	public void Down()
	{
		if (grounded||jumping&&!flying) {
			downing = true;
			verticalSpeed = -jumpSpeed*1.5f;
		}
		if(flying)
		{
			GlobalOptions.playerStates=PlayerStates.FLY;
		}
	}
	
	public void Fly(bool inflag)
	{
		flying = inflag;
		movingToFlyGround=true;
		groundingFlag=false;
	}
	
	public void MoveToGroundEmmidiately()
	{
		groundingFlag=false;
		playerScript.Character.layer=11;
		movingToFlyGround=false;
		walkingBearTransform.localPosition=new Vector3(0,groundYsmex,0);
	}
	
	private void MoveToFlyGround()
	{
		float ypos=walkingBearTransform.localPosition.y;
		//взлёт
		if(flying)
		{
			playerScript.Character.layer=13;
			ypos+=vsletAcceleration*Time.deltaTime;
			if(flyingYsmex<=ypos)
			{
				ypos=flyingYsmex;
				movingToFlyGround=false;
			}
		}
		else
		{
			//падение
			if(!groundingFlag)
			{
				ypos-=vsletAcceleration*Time.deltaTime;
				if(groundYsmex>=ypos)
				{
					if(GlobalOptions.playerStates!=PlayerStates.DIE)
					{
						GlobalOptions.playerStates=PlayerStates.WALK;
					}
					ypos=groundYsmex;
					timeToGround=Time.time;
					groundingFlag=true;
					playerScript.Character.layer=16;
				}
			}
			else
			{
				if(Time.time-timeToGround>4)
				{
					groundingFlag=false;
					movingToFlyGround=false;
					playerScript.Character.layer=11;
				}
			}
		}
		walkingBearTransform.localPosition=new Vector3(0,ypos,0);
	}
	
	public void Freeze()
	{
		freezed=true;
	}
	
	public void Respawn()
	{
		flying=false;
		stumble=false;
		freezed=false;
		groundingFlag=false;
		movingToFlyGround=false;
		stopTime=0;//время остановки
		startstopTime=0;
	}
	
	private void AddAllTimes()
	{
		if(startstopTime!=0)
		{
			glideTimer+=stopTime;
			timeToGround+=stopTime;
			startstopTime=0;
			stopTime=0;
		}
	}
	
	public void SetMovement(float inmovement)
	{
		moveforward=inmovement;
	}
	
	public void LeftRight(float inx)
	{
		forcex=inx;
	}
	
	//Collision For Stumble
	void OnControllerColliderHit (ControllerColliderHit hit)
	{            
        // We dont want to push objects below us
        //if (hit.moveDirection.y < -0.3) 
        //    return;
        
        // Calculate push direction from move direction, 
        // we only push objects to the sides never up and down
        Vector3 pushDir  = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
		
		if(pushDir.x!=0||pushDir.z!=0)
		{
			curStumbleTransform=hit.collider.transform;
		}
	}
	
	public void PathChangeJump()
	{
		/*if(grounded&&!glideFlag)
		{
			flagPathChangeJump=true;
			verticalSpeed = jumpSpeed/1.5f;
		}*/
	}
	
	public float GetVerticalSpeed()
	{
		return verticalSpeed;
	}
}
