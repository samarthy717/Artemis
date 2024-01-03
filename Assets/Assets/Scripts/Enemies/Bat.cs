using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bat : MonoBehaviour,Idamagable
{
    // Start is called before the first frame update
    public AIPath aipath;
    bool IsAlive = true;
    public float MaxHealth = 3f;
    private float HitPoints;
    public Animator anim;
    void Start()
    {
        HitPoints=MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive) return;
        FacePlayer();
        Die();
    }
    void FacePlayer()
    {
        if (aipath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    void Die()
    {
        if (HitPoints <= 0)
        {
            anim.enabled = false;
            aipath.gravity = 5*Physics2D.gravity;
            IsAlive = false;
            //animator.SetBool("IsDead", true);
            //Rigidbody.velocity = deathKick;
            gameObject.layer = LayerMask.NameToLayer("Ground");
            Destroy(gameObject, 5f);
        }
    }
    public void DamageAmount(float AttackDamage)
    {
        HitPoints -= AttackDamage;
        float healthbar = HitPoints / MaxHealth;
        //image.fillAmount = healthbar;
    }
}
