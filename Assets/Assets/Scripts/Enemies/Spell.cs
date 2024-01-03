using Pathfinding;
using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour,Idamagable
{
    // Start is called before the first frame update
    PlayerController PlayerFire;
    public AIPath aipath;
    private AIDestinationSetter seeker;  // Add a reference to the Seeker component

    void Start()
    {
        PlayerFire = FindObjectOfType<PlayerController>();
        seeker = GetComponent<AIDestinationSetter>();  // Get the Seeker component from the same GameObject
        seeker.target = PlayerFire.transform;  // Set the target to the PlayerFire's transform
    }

    // Update is called once per frame
    void Update()
    {
        FacePlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerFire.Die();
            Destroy(gameObject);
        }
    }
    void FacePlayer()
    {
        if (aipath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    public void DamageAmount(float AttackDamage)
    {
        Destroy(gameObject);
    }
}
