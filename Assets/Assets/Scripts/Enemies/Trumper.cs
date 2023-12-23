using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trumper : MonoBehaviour
{
    public Transform FireyPoint;
    public Transform PlayerPos;
    private GameObject prefabToInstantiate;
    public GameObject leftFireball;
    public GameObject RightFireball;
    public float spawnInterval = 5f;
    private Rigidbody2D Trumperbody;
    private Animator Trumperanimator;
    public float HitPoints = 6f;
    public Rigidbody2D rigidbody23;
    private bool IsAlive = true;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    private void Start()
    {
        // Start invoking the SpawnPrefab function with the specified interval
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
        Trumperbody = GetComponent<Rigidbody2D>();
        Trumperanimator = GetComponent<Animator>();
    }
    private void Update()
    {
        FacePlayer();
        Death();
    }
    void FacePlayer()
    {
        if (!IsAlive) return;
        if (PlayerPos.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1f,Trumperbody.transform.localScale.y);
            prefabToInstantiate = RightFireball;
        }
        else
        {
            transform.localScale = new Vector2(-1f, Trumperbody.transform.localScale.y);
            prefabToInstantiate = leftFireball;
        }
    }
    private void SpawnPrefab()
    {
        if (!IsAlive) return;
        // Trumperanimator.SetBool("IsFiring", true);
        //StartCoroutine(delayfunction());
        Instantiate(prefabToInstantiate, FireyPoint.position, FireyPoint.rotation);
        //StartCoroutine(StopFiring());
    }
    /* IEnumerator delayfunction()
     {
         yield return new WaitForSecondsRealtime(1f);
     }
     IEnumerator StopFiring()
     {
         yield return new WaitForSecondsRealtime(1f);
         Trumperanimator.SetBool("IsFiring", false);
     }*/
    void Death()
    {
        if (HitPoints <= 0)
        {
            IsAlive = false;
            Trumperanimator.SetBool("IsDead", true);
            rigidbody23.velocity = deathKick;
            Destroy(gameObject, 5f);
        }
    }
}
