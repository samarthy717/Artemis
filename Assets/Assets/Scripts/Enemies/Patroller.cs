using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour,Idamagable
{
    public float RunSpeed = 20f;
    public Rigidbody2D Rigidbody;
    PlayerController Player;
    private float PattoDirection;
    public Animator animator;
    bool IsAlive = true;
    public float HitPoints = 2f;
    public BoxCollider2D FootCollider;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    // Start is called before the first frame update
    void Start()
    {
        PattoDirection = transform.localScale.x;
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive) return;
        Run();
        FlipSprite();
        Die();
        Patrol();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if (collision.tag == "Player" && IsAlive)
        {
            Player.Die();
        }
            PattoDirection = -PattoDirection;
    }
    void Run()
    {
        if (!IsAlive) return;
        if (Rigidbody.velocity.y == 0) Rigidbody.velocity = new Vector2(RunSpeed, 0f) * PattoDirection;
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(Rigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(Rigidbody.velocity.x), 1f);
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

        if (!FootCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Walls")))
        {
            //Debug.Log("Turn");
            PattoDirection = -PattoDirection;
        }
    }

    public void DamageAmount(float AttackDamage)
    {
        HitPoints -= AttackDamage;
    }
}
