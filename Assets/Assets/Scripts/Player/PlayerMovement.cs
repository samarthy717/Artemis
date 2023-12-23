using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float AttackDamage = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    private float DoubleJump = 2;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    public CapsuleCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    // Attack
    public Transform AttackPoint;
    public float AttackRadius;
    public LayerMask EnemyLayer;
    // Dashing
    [Header("Dashing")]
    bool CanDash = true;
    bool IsDashing = false;
    public float DashingPower = 24f;
    public float DashingTime = 2f;
    public float DashingCoolDown = 1f;
    public TrailRenderer Trailrendobro;
    // Wall Sliding
    private bool IsWallSliding = false;
    public float WallSlidingSpeed = 5f;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask Walls;
    // Wall Jumping
    private bool isWallJumping;
    public float wallJumpingTime = 0.2f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);
    // Enemies
    MadRunner MadRunner;
    Patroller Patroller;
    Trumper Trumper;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        //myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        MadRunner = FindObjectOfType<MadRunner>();
        Patroller = FindObjectOfType<Patroller>();
        Trumper = FindObjectOfType<Trumper>();
    }

    void Update()
    {
        if (!isAlive) { return; }
       // Run();
       // FlipSprite();
       // ClimbLadder();
       // Die();
      // WallSlide();
       // wallJumping();
    }

    private void wallJumping()
    {
        if (isWallJumping)
        {
            myRigidbody.velocity = new Vector2(-moveInput.x * wallJumpingPower.x, wallJumpingPower.y);
        }
        else if (IsDashing) {}
        else
        {
            myRigidbody.velocity = new Vector2(moveInput.x *runSpeed, myRigidbody.velocity.y);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        //Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (IsDashing) { return; }
      /*  if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && DoubleJump == 0)
        {
            //Debug.Log("zero");
            return;
        }*/


        if (value.isPressed)
        {
            // Perform the jump
            if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                DoubleJump = 1; // Reset double jump count if touching the ground
            }
            else if (DoubleJump > 0)
            {
                DoubleJump--; // Consume double jump
            }
             else if (IsWallSliding)
            {
                //Debug.Log("Sad");
                isWallJumping = true;
                Invoke("StopWallJumping", wallJumpingTime);
                return;
            }
            else
            {
                return;
            }
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    void RegenerateDoubleJump()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            DoubleJump = 2; // Regenerate double jumps when touching the ground
        }

        Debug.Log("Done");
    }

    void OnAttack()
    {
        if (!isAlive) return;
        // Play Attack Animation
        if (IsDashing) { return; }
        myAnimator.SetBool("IsAttacking",true);
        StartCoroutine(DelayedStopAttack());

        // Detect Enemies in the range of Attack
        Collider2D[] HitEnemies= Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius,EnemyLayer); 
        // Deal damage to enemies
        foreach(Collider2D enemy in HitEnemies)
        {
            Debug.Log(enemy.name);
            if (enemy.name == "MadRunner")
            {
                MadRunner.HitPoints -= AttackDamage;
            }
            if (enemy.name == "Patroller")
            {
                Patroller.HitPoints -= AttackDamage;
            }
            if (enemy.name == "Trumper")
            {
                Trumper.HitPoints -= AttackDamage;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(AttackPoint.position,AttackRadius);
        Gizmos.DrawWireSphere(WallCheck.position, 0.2f);
    }
    IEnumerator DelayedStopAttack()
    {
        yield return new WaitForSecondsRealtime(1f);
        myAnimator.SetBool("IsAttacking", false);
    }
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, Walls);
    }
    private void WallSlide()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool isGrounded =myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //bool pos = ((moveInput.x > 0 && transform.localScale.x<0) || (moveInput.x < 0 && transform.localScale.x>0));
        if (IsWalled() && moveInput.x != 0 && !isGrounded)
        {
            myAnimator.SetBool("IsRunning", false);
            myAnimator.SetBool("wallslider", true);
            Debug.Log("walled");
            IsWallSliding = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -WallSlidingSpeed);
        }
        else
        {
            myAnimator.SetBool("wallslider", false);
            IsWallSliding = false;
        }
    }
    

    void OnDash()
    {
        if (!isAlive) return;
        myAnimator.SetBool("dasherboy",true);
        StartCoroutine(StopDashing());
        StartCoroutine(Dasher());
    }
    IEnumerator StopDashing()
    {
        yield return new WaitForSecondsRealtime(DashingTime);
        myAnimator.SetBool("dasherboy", false);
    }

    private IEnumerator Dasher()
    {
        CanDash = false;
        IsDashing = true;
        float OriginalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0f;
        myRigidbody.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        Trailrendobro.emitting = true;
        yield return new WaitForSecondsRealtime(DashingTime);
        Trailrendobro.emitting = false;
        myRigidbody.gravityScale = OriginalGravity;
        IsDashing = false;
        yield return new WaitForSecondsRealtime(DashingCoolDown);
        CanDash = true;
    }

    void Run()
    {
        if (IsWallSliding) { return; }
        if (IsDashing) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }
   
    public void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            myAnimator.SetBool("IsDead", true);
            isAlive = false;
            myRigidbody.velocity = deathKick;
            //FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}

