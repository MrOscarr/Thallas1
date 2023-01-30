using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Playermovement : MonoBehaviour
{
    
	[HideInInspector] public float gravityStrength; //Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.
	[HideInInspector] public float gravityScale; //Strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D).
										  //Also the value the player's rigidbody2D.gravityScale is set to.
	
	public float fallGravityMult; //Multiplier to the player's gravityScale when falling.
	public float maxFallSpeed; //Maximum fall speed (terminal velocity) of the player when falling.
    [Range(0f, 1)] public float accelInAir; //Multipliers applied to acceleration rate when airborne.
	[Range(0f, 1)] public float deccelInAir;

    [Space(10)]

    [Header("Run")]
	public float runMaxSpeed; //Target speed we want the player to reach.
	public float runAcceleration; //The speed at which our player accelerates to max speed, can be set to runMaxSpeed for instant acceleration down to 0 for none at all
	[HideInInspector] public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
	public float runDecceleration; //The speed at which our player decelerates from their current speed, can be set to runMaxSpeed
	[HideInInspector] public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
	
	
	
    public float speed = 4f;
    Rigidbody2D rb;
    bool facingRight = true;
    

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Space(10)]

    [Header("Jump")]
    public float jumpHeight; //Height of the player's jump
	public float jumpTimeToApex; //Time between applying the jump force and reaching the desired jump height. These values also control the player's gravity and jump force.
	[HideInInspector] public float jumpForce; //The actual force applied (upwards) to the player when they jump.
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private Animator anim;

    [Space(10)]

    [Header("Dash")]
    public float dashMaxSpeed;
    public float dashTimeToApex;
    [HideInInspector] public float dashForce;
    public float dashSpeed;
    private float activeMoveSpeed = 4f;
    public float dashSpeedUp;
    public float dashLength = 0.5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

    [Space(10)]

    [Header("WallJump")]
	public Transform frontCheck;
    bool isTouchingFront;
    public LayerMask whatIsMossWall;
	bool wallSliding;
	public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    public float wallJumpCooldown = 2f;
    public bool wallJumpReady;
    public float wallJumpCDCurrent = 0f;


    void Start()
    {
        activeMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    
    
    }

    private void OnValidate()
    {
		//Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
		
		//Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
		gravityScale = gravityStrength / Physics2D.gravity.y;

		//Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		
		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        dashForce = Mathf.Abs(gravityStrength) * dashTimeToApex;
		
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		
	}

    //START RUN//
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        float targetSpeed = input * runMaxSpeed;

        float accelRate;

        if (coyoteTime > 0)
		{
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        }
		else
		{
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;
        }

        float speedDif = targetSpeed - rb.velocity.x;

		float movement = speedDif * accelRate;

		rb.AddForce(movement * Vector2.right, ForceMode2D.Force);



        if(input != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if(input > 0 && facingRight == false)
        {
            Flip();
        }
        else if (input < 0 && facingRight == true)
        {
            Flip();
        }
        //END RUN//

        //START COYOTE&JUMPBUFFER//
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        

        if( jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            float force = jumpForce;
		    if (rb.velocity.y < 0)
            {
                force -= rb.velocity.y;
                rb.gravityScale = gravityScale * fallGravityMult;
            }
            else
            {
                rb.gravityScale = gravityScale;
            }

		    rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

            jumpBufferCounter = 0f;

        }

        if (coyoteTimeCounter > 0f)
        {
            anim.SetBool("IsJump", false);
        }
        else
        {
            anim.SetBool("IsJump", true);

            coyoteTimeCounter = 0f;
        }
        //END COYOTE&JUMPBUFFER//

        //START DASH//
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Dash", true);
            if(dashCoolCounter <=0 && dashCounter <=0)
            {
                speed = dashSpeedUp;
                rb.velocity = new Vector2(rb.velocity.x, transform.localScale.y * speed);
                dashCounter = dashLength;
            }
            
        }

        if(Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W))
            {
                anim.SetBool("Dash", true);
                float forceDash = dashForce;
                if(dashCoolCounter <=0 && dashCounter <=0)
                {
                    if(input > 0)
                    {
                        rb.AddForce(Vector2.right * forceDash, ForceMode2D.Impulse);
                        dashCounter = dashLength;
                    }
                    else if (input < 0)
                    {
                        rb.AddForce(Vector2.left * forceDash, ForceMode2D.Impulse);
                        dashCounter = dashLength;
                    }
                    
                }
            }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0 )
            {
                
                anim.SetBool("Dash", false);
                speed = activeMoveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            
            anim.SetBool("Dash", false);
            dashCoolCounter -= Time.deltaTime;
        }

    
        //END DASH//

        //START WALL SlIDING//
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsMossWall);

		if(isTouchingFront == true && isGrounded == false && input !=0)
		{
            anim.SetBool("OnWall", true);
            anim.SetBool("IsJump", false);
			wallSliding = true;
		}
		else
		{
            anim.SetBool("OnWall", false);
			wallSliding = false;
		}

		if(wallSliding)
		{
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        //END WALL SlIDING//

        //START WALL JUMP//
        if(wallJumpCDCurrent >= wallJumpCooldown)
        {
            wallJumpReady = true;
        }
        else
        {   
            wallJumpCDCurrent +=Time.deltaTime;
            wallJumpReady = false;
            wallJumpCDCurrent = Mathf.Clamp(wallJumpCDCurrent, 0f, wallJumpCooldown);
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding == true && wallJumpReady)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
            wallJumpCDCurrent = 0f;
        }

        if(wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }

        

    }

    void SetWallJumpingToFalse()
        {
            wallJumping = false;
        }
    //END WALL JUMP//

    //START FLIP//
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
    //END FLIP//

}
