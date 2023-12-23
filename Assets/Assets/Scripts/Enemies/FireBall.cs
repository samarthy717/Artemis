using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D Trigidbody;
    public float FireSpeed = 10f;
    public float FireDirection = 1f;
    PlayerController PlayerFire;
    void Start()
    {
        Trigidbody = GetComponent<Rigidbody2D>();
        PlayerFire = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Trigidbody.velocity = new Vector2(FireSpeed, 0f) * FireDirection;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Die");
           PlayerFire.Die();
        }
        Destroy(gameObject);
    }
}
