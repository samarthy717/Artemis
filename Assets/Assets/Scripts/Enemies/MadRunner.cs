using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MadRunner : MonoBehaviour,Idamagable
{
    public float RunSpeed = 20f;
    public Rigidbody2D Rigidbody;
    PlayerController Player;
    private float MaddyDirection;
    public Animator animator;
    bool IsAlive = true;
    public float MaxHealth = 3f;
    private float HitPoints;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        MaddyDirection = transform.localScale.x;
        Player = FindObjectOfType<PlayerController>();
        HitPoints = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive) return;
        Run();
        FlipSprite();
        Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&IsAlive)
        {
            Player.Die();
        }
        MaddyDirection = -MaddyDirection;

    }
    void Run()
    {
        if (!IsAlive) return;
        if (Rigidbody.velocity.y == 0) Rigidbody.velocity = new Vector2(RunSpeed, 0f) * MaddyDirection;
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


    public void DamageAmount(float AttackDamage)
    {
        HitPoints -= AttackDamage;
        float healthbar = HitPoints / MaxHealth;
        image.fillAmount = healthbar;
    }
}
