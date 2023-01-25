using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Playermovement : MonoBehaviour
{

    public float speed = 4f;
    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private Animator anim;

    private float activeMoveSpeed = 4f ;
    public float dashSpeed;
    public float dashLength = 0.5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

    bool isTouchingFront;
	public Transform frontCheck;
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

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

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
            rb.velocity = Vector2.up * jumpForce;

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

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Dash", true);
            if(dashCoolCounter <=0 && dashCounter <=0)
            {
                speed = dashSpeed;
                dashCounter = dashLength;
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

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }



    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
