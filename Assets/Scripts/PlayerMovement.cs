using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool drawDebugRaycasts = true;	

	[Header("Movement Properties")]
	public float speed = 8f;				
	public float crouchSpeedDivisor = 3f;	
	public float coyoteDuration = .05f;		
	public float maxFallSpeed = -25f;		

	[Header("Jump Properties")]
	public float jumpForce = 6.3f;			
	public float crouchJumpBoost = 2.5f;	
	public float hangingJumpForce = 15f;	
	public float jumpHoldForce = 1.9f;		
	public float jumpHoldDuration = .1f;	

	[Header("Environment Check Properties")]
	public float footOffset = .4f;			
	public float eyeHeight = 1.5f;			
	public float reachOffset = .7f;			
	public float headClearance = .5f;		
	public float groundDistance = .2f;		
	public float grabDistance = .4f;		
	public LayerMask groundLayer;			

	[Header ("Status Flags")]
	public bool isOnGround;					
	public bool isHanging;
    public bool isJumping;
	public bool isCrouching;				
	public bool isHeadBlocked;

	PlayerInput input;						
	BoxCollider2D bodyCollider;				
	Rigidbody2D rigidBody;					
	
	float jumpTime;							
	float coyoteTime;						
	float playerHeight;						

	float originalXScale;					
	int direction = 1;						

	Vector2 colliderStandSize;				
	Vector2 colliderStandOffset;			
	Vector2 colliderCrouchSize;				
	Vector2 colliderCrouchOffset;			

	const float smallAmount = .05f;			


	void Start ()
	{
		
		input = GetComponent<PlayerInput>();
		rigidBody = GetComponent<Rigidbody2D>();
		bodyCollider = GetComponent<BoxCollider2D>();

		
		originalXScale = transform.localScale.x;

		
		playerHeight = bodyCollider.size.y;

		
		colliderStandSize = bodyCollider.size;
		colliderStandOffset = bodyCollider.offset;

		
		colliderCrouchSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y / 2f);
		colliderCrouchOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y / 2f);
	}

	void FixedUpdate()
	{
		
		PhysicsCheck();

		
		GroundMovement();		
		MidAirMovement();
	}

	void PhysicsCheck()
	{
		
		isOnGround = false;
		isHeadBlocked = false;

		
		RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
		RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);

		
		if (leftCheck || rightCheck)
			isOnGround = true;

		
		RaycastHit2D headCheck = Raycast(new Vector2(0f, bodyCollider.size.y), Vector2.up, headClearance);

		
		if (headCheck)
			isHeadBlocked = true;

		
		Vector2 grabDir = new Vector2(direction, 0f);

		
		RaycastHit2D blockedCheck = Raycast(new Vector2(footOffset * direction, playerHeight), grabDir, grabDistance);
		RaycastHit2D ledgeCheck = Raycast(new Vector2(reachOffset * direction, playerHeight), Vector2.down, grabDistance);
		RaycastHit2D wallCheck = Raycast(new Vector2(footOffset * direction, eyeHeight), grabDir, grabDistance);

		if (!isOnGround && !isHanging && rigidBody.velocity.y < 0f && 
			ledgeCheck && wallCheck && !blockedCheck)
		{ 
			Vector3 pos = transform.position;
			pos.x += (wallCheck.distance - smallAmount) * direction;
			pos.y -= ledgeCheck.distance;
			transform.position = pos;
			rigidBody.bodyType = RigidbodyType2D.Static;
			isHanging = true;
		}
	}

	void GroundMovement()
	{
		if (isHanging)
			return;

		if (input.crouchHeld && !isCrouching && !isJumping)
			Crouch();
		else if (!input.crouchHeld && isCrouching)
			StandUp();
		else if (!isOnGround && isCrouching)
			StandUp();

		float xVelocity = speed * input.horizontal;

		if (xVelocity * direction < 0f)
			FlipCharacterDirection();

		if (isCrouching)
			xVelocity /= crouchSpeedDivisor;

		rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

		if (isOnGround)
			coyoteTime = Time.time + coyoteDuration;
	}

	void MidAirMovement()
	{
		if (isHanging)
		{
			if (input.crouchPressed)
			{
				isHanging = false;
				rigidBody.bodyType = RigidbodyType2D.Dynamic;
				return;
			}

			if (input.jumpPressed)
			{
				isHanging = false;
				rigidBody.bodyType = RigidbodyType2D.Dynamic;
				rigidBody.AddForce(new Vector2(0f, hangingJumpForce), ForceMode2D.Impulse);
				return;
			}
		}

		if (input.jumpPressed && !isJumping && (isOnGround || coyoteTime > Time.time))
		{
			if (isCrouching && !isHeadBlocked)
			{
				StandUp();
				rigidBody.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
			}

			isOnGround = false;
			isJumping = true;

			jumpTime = Time.time + jumpHoldDuration;

			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

			AudioManager.PlayJumpAudio();
		}
		else if (isJumping)
		{
			if (input.jumpHeld)
				rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

			if (jumpTime <= Time.time)
				isJumping = false;
		}

		if (rigidBody.velocity.y < maxFallSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
	}

	void FlipCharacterDirection()
	{
		direction *= -1;

		Vector3 scale = transform.localScale;

		scale.x = originalXScale * direction;

		transform.localScale = scale;
	}

	void Crouch()
	{
		isCrouching = true;

		bodyCollider.size = colliderCrouchSize;
		bodyCollider.offset = colliderCrouchOffset;
	}

	void StandUp()
	{
		if (isHeadBlocked)
			return;

		isCrouching = false;
	
		bodyCollider.size = colliderStandSize;
		bodyCollider.offset = colliderStandOffset;
	}


	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
	{
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{
		Vector2 pos = transform.position;

		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		if (drawDebugRaycasts)
		{
			Color color = hit ? Color.red : Color.green;
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}

		return hit;
	}
}
