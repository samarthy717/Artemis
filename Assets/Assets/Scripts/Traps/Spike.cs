using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement controller;
    void Start()
    {
        controller = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Death");
            controller.Die();
        }
        else
        {
            Debug.Log("Collision with unknown object");
        }
    }
}
