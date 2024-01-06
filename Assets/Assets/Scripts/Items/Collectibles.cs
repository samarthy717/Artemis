using SupanthaPaul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController pc;
    void Start()
    {
        pc=FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                pc.PlayerCoins++;
            }
            else if (gameObject.CompareTag("Gem"))
            {
                pc.PlayerCoins += 10;
            }

            Destroy(gameObject);
        }
    }
}
