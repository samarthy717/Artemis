using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float FallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2f;
    public float MaxFallingValue = 50f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, float.MinValue, MaxFallingValue));

        }
    }
}
