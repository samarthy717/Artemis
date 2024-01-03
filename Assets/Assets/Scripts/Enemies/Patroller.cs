using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patroller : MonoBehaviour,Idamagable
{
    public float RunSpeed = 20f;
    PlayerController Player;
    private float PattoDirection;
    public Animator animator;
    bool IsAlive = true;
    public float MaxHealth = 3f;
    public float HitPoints;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    public Image image;
    public float RayDist;
    public Transform GroundCheck;
    public Transform OnGround;
    bool movingRight;
    [SerializeField] private  LayerMask Groundlayer;
    // Start is called before the first frame update
    void Start()
    {
        PattoDirection = transform.localScale.x;
        Player = FindObjectOfType<PlayerController>();
        HitPoints = MaxHealth;
        movingRight = PattoDirection == 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive) return;
        FlipSprite();
        Die();
        Patrol();
        //Run();
    }
    void Run()
    {
        if (!IsAlive) return;
        //if (Rigidbody.velocity.y == 0) Rigidbody.velocity = new Vector2(RunSpeed*PattoDirection, 0f);
    }

    void FlipSprite()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(1, 1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1,1);
        }
    }
    void Die()
    {
        if (HitPoints <= 0)
        {
            IsAlive = false;
            animator.SetBool("IsDead", true);
            //Rigidbody.velocity = deathKick;
            gameObject.layer = LayerMask.NameToLayer("Ground");
            Destroy(gameObject, 5f);
        }
    }
    void Patrol()
    {
        Debug.Log(IsTouchingGround());
        //if (!IsTouchingGround()) return;
        transform.Translate(Vector2.right * RunSpeed * Time.deltaTime*PattoDirection);
        RaycastHit2D GroundDetect = Physics2D.Raycast(GroundCheck.position, Vector2.down, RayDist,Groundlayer);
        if (GroundDetect.collider == false)
        {
            Debug.Log("turn");
            if (movingRight)
            {
                //transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                PattoDirection = -1;
            }
            else
            {
                //transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                PattoDirection = 1;
            }
        }
        //Rigidbody.velocity = new Vector2(RunSpeed * PattoDirection, Rigidbody.velocity.y);

    }
    bool IsTouchingGround()
    {
        float raycastDistance = 0.2f; // Adjust this distance based on your capsule size
        RaycastHit2D hit = Physics2D.Raycast(OnGround.position, Vector2.down, raycastDistance,Groundlayer);
        return hit.collider != null;
    }



    public void DamageAmount(float AttackDamage)
    {
        HitPoints -= AttackDamage;
        float healthbar = HitPoints / MaxHealth;
        image.fillAmount = healthbar;
    }
}
