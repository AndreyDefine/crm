using UnityEngine;
using System.Collections;

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
	private Transform prevcurStumbleTransform=null;
	
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
	
	private float heightnormal=1.5f, heightslide=0.1f;
	private CapsuleCollider walkinbearCollider;
	
	private GameObject walkingBear;
	
	private float flyingYsmex=8,groundYsmex=0;
	
	private CharacterController controller;
	// Update is called once per frame
	
	
	private void UpdateSmoothedMovementDirection ()
	{
		/*if(!GlobalOptions.flagOnlyFizik)
		{
			if(moveforward>=0)
			{
				moveforward=-0.00001f;
			}else
			{
				moveforward=0.00001f;
			}
		}*/
	}
	
	void Start()
	{
		playerScript=GlobalOptions.GetPlayerScript();
		controller = GetComponent<CharacterController>();
		walkingBear=singleTransform.FindChild("WalkingBear").gameObject;
		walkinbearCollider=walkingBear.collider as CapsuleCollider;
	}
	
	void Update() {
		
		if(GlobalOptions.gameState==GameStates.GAME||GlobalOptions.gameState==GameStates.GAME_OVER)
		{
			AddAllTimes();
			UpdateSmoothedMovementDirection();
		
			if (grounded&&!jumping&&!flagPathChangeJump) {
				verticalSpeed = 0;
			}
			
			flagPathChangeJump=false;
			// Apply gravity
			//if(!freezed)
			//{
			verticalSpeed -= gravity * Time.deltaTime;
			//}
			if(freezed)
			{
				moveforward=0;
				forcex=0;
			}
			
			Vector3 right = singleTransform.TransformDirection(Vector3.right);
			Vector3 forward = singleTransform.TransformDirection(Vector3.forward);
			
			Vector3 movement = moveforward*forward + new Vector3 (0, verticalSpeed, 0) + forcex*right;
			
			
			movement *= Time.deltaTime;
			// Move the controller
			CollisionFlags flags = controller.Move(movement);
			grounded = (flags & CollisionFlags.CollidedBelow) != 0;
			
			stumble = (flags & CollisionFlags.CollidedSides) != 0;
			
			if(stumble&&!flying&&!groundingFlag&&moveforward>0)
			{
				bool needStumble=true;
				if(curStumbleTransform.name.Contains("Zabiratsa"))
				{
					needStumble=false;
					Debug.Log ("Zabiratsa");
				}
				//под водкой
				if(prevcurStumbleTransform==curStumbleTransform&&playerScript.isVodka())
				{
					needStumble=false;
					curStumbleTransformLayer=curStumbleTransform.gameObject.layer;
					curStumbleTransform.gameObject.layer=10;
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
					Debug.Log ("GlobalOptions.playerStates=PlayerStates.DOWN;");
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
			walkinbearCollider.center=new Vector3(walkinbearCollider.center.x,walkinbearCollider.center.y-(heightnormal-heightslide)/2,walkinbearCollider.center.z);
		}
	}
	
	private void UnMakeGlide()
	{
		if(glideFlag)
		{
			walkinbearCollider.height=heightnormal;
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
		walkingBear.transform.localPosition=new Vector3(0,groundYsmex,0);
	}
	
	private void MoveToFlyGround()
	{
		float ypos=walkingBear.transform.localPosition.y;
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
		walkingBear.transform.localPosition=new Vector3(0,ypos,0);
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
		//Debug.Log (moveforward);
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
			prevcurStumbleTransform=curStumbleTransform;
			if(prevcurStumbleTransform)
			{
				prevcurStumbleTransform.gameObject.layer=curStumbleTransformLayer;
			}
			curStumbleTransform=hit.collider.transform;
		}
	}
	
	public void PathChangeJump()
	{
		if(grounded&&!glideFlag)
		{
			flagPathChangeJump=true;
			verticalSpeed = jumpSpeed/1.5f;
		}
	}
	
	public float GetVerticalSpeed()
	{
		return verticalSpeed;
	}
}
