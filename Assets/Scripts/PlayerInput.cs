using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
	public bool testTouchControlsInEditor = false;	    
	public float verticalDPadThreshold = .5f;		   
				   

	[HideInInspector] public float horizontal;		    
	[HideInInspector] public bool jumpHeld;			    
	[HideInInspector] public bool jumpPressed;		    
	[HideInInspector] public bool crouchHeld;		    
	[HideInInspector] public bool crouchPressed;	    
	
	bool dPadCrouchPrev;							    
	bool readyToClear;								      


	void Update()
	{
		ClearInput();

		if (GameManager.IsGameOver())
			return;

		ProcessInputs();
		//ProcessTouchInputs();

		horizontal = Mathf.Clamp(horizontal, -1f, 1f);
	}

	void FixedUpdate()
	{
		readyToClear = true;
	}

	void ClearInput()
	{
		if (!readyToClear)
			return;

		horizontal		= 0f;
		jumpPressed		= false;
		jumpHeld		= false;
		crouchPressed	= false;
		crouchHeld		= false;

		readyToClear	= false;
	}

	void ProcessInputs()
	{
		horizontal		+= Input.GetAxis("Horizontal");

		jumpPressed		= jumpPressed || Input.GetButtonDown("Jump");
		jumpHeld		= jumpHeld || Input.GetButton("Jump");

		crouchPressed	= crouchPressed || Input.GetButtonDown("Crouch");
		crouchHeld		= crouchHeld || Input.GetButton("Crouch");
	}

	//void ProcessTouchInputs()
	//{
		//if (!Application.isMobilePlatform && !testTouchControlsInEditor)
			//return;

		//Vector2 thumbstickInput = thumbstick.GetDirection();

		//horizontal		+= thumbstickInput.x;

		//jumpPressed		= jumpPressed || jumpButton.GetButtonDown();
		//jumpHeld		= jumpHeld || jumpButton.GetButton();

		//bool dPadCrouch = thumbstickInput.y <= -verticalDPadThreshold;
		//crouchPressed	= crouchPressed || (dPadCrouch && !dPadCrouchPrev);
		//crouchHeld		= crouchHeld || dPadCrouch;

		//dPadCrouchPrev	= dPadCrouch;
	//}
}
