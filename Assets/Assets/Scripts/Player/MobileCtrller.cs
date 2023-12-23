using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCtrller : MonoBehaviour
{
    public GameObject Player;
    PlayerMovement PlayerMov;
    void Start()
    {
        PlayerMov = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        
    }
    
}
