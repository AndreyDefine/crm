using UnityEngine;
using System.Collections;

public class CharacterMarioC : Abstract {	
	public float jumpSpeed = 12f;
	public float gravity = 9.81f;

	private float moveforward=0;
	private float verticalSpeed = 0f;
	
	private bool grounded=false;
	private bool jumping=false;
	private bool downing=false;
	private bool stumble=false;
	//private bool downing=false;
	
	private bool freezed=false;
	
	private float forcex=0;
	
	private Player playerScript;
	
	private float glideTimer;
	private bool glideFlag=false;
	
	private float heightnormal=1.5f, heightslide=0.5f;
	
	private CharacterController controller;
	// Update is called once per frame
	
	private void UpdateSmoothedMovementDirection ()
	{
		// Target direction relative to the camera	
		//Vector3 targetDirection=new Vector3(0,0,1);
		if(moveforward>=0)
		{
			moveforward=-0.1f;
		}else
		{
			moveforward=0.1f;
		}
	}
	
	void Start()
	{
		playerScript=GlobalOptions.GetPlayerScript();
		controller = GetComponent<CharacterController>();
	}
	
	void Update() {

		UpdateSmoothedMovementDirection();
	
		if (grounded&&!jumping) {
			verticalSpeed = 0;
		}
		// Apply gravity
		if(!freezed)
		{
			verticalSpeed -= gravity * Time.deltaTime;
		}
		else
		{
			moveforward=0;
		}
		
		Vector3 right = singleTransform.TransformDirection(Vector3.right);
		Vector3 forward = singleTransform.TransformDirection(Vector3.forward);
		
		Vector3 movement = moveforward*forward + new Vector3 (0, verticalSpeed, 0) + forcex*right;
		
		//Debug.Log (movement);
		
		if(freezed)
		{
			movement=Vector3.zero;
		}
		
		movement *= Time.deltaTime;
		// Move the controller
		CollisionFlags flags = controller.Move(movement);
		grounded = (flags & CollisionFlags.CollidedBelow) != 0;
		
		stumble = (flags & CollisionFlags.CollidedSides) != 0;
		
		if(stumble)
		{
			playerScript.Stumble();
		}
		
		// We are in jump mode but just became grounded
		if (grounded)
		{
			if(downing)
			{
				Glide();
			}
			jumping = false;
		}
		downing = false;
		
		if(glideFlag)
		{
			MakeGlide();
		}
	}
	
	private void Glide()
	{
		if(!glideFlag)
		{
			glideFlag=true;
			glideTimer=Time.time;
			controller.height=heightslide;
			controller.center=new Vector3(controller.center.x,controller.center.y-(heightnormal-heightslide)/2,controller.center.z);
		}
	}
	
	private void MakeGlide()
	{
		if(Time.time-glideTimer>1)
		{
			controller.height=heightnormal;
			controller.center=new Vector3(controller.center.x,controller.center.y+(heightnormal-heightslide)/2,controller.center.z);
			glideFlag=false;
		}
	}
	
	public bool isGrounded()
	{
		return grounded;	
	}
	
	public bool isJumping()
	{
		return jumping;	
	}
	
	public void Jump()
	{
		if (grounded&&!jumping&&!glideFlag) {
			jumping = true;
			verticalSpeed = jumpSpeed;
		}
	}
	
	public void Down()
	{
		if (grounded||jumping) {
			downing = true;
			verticalSpeed = -jumpSpeed;
		}
	}
	
	public void Freeze()
	{
		freezed=true;
	}
	
	public void Respawn()
	{
		freezed=false;
	}
	
	public void SetMovement(float inmovement)
	{
		moveforward=inmovement;
		Debug.Log (moveforward);
	}
	
	public void LeftRight(float inx)
	{
		//Debug.Log(inx);
		forcex=inx;
	}
}
