using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour, Idamagable
{
    public Transform FireyPoint;
    public Transform PlayerPos;
    public GameObject prefabToInstantiate;
    public float spawnInterval = 5f;
    private Rigidbody2D Trumperbody;
    private Animator Wizardanimator;
    private float HitPoints;
    public float MaxHealth;
    public Rigidbody2D rigidbody23;
    private bool IsAlive = true;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    public Image image;
    private void Start()
    {
        // Start invoking the SpawnPrefab function with the specified interval
       InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
        Trumperbody = GetComponent<Rigidbody2D>();
        Wizardanimator = GetComponent<Animator>();
        HitPoints = MaxHealth;
    }
    private void Update()
    {
        if (!IsAlive) return;
        FacePlayer();
        Death();
    }
    void FacePlayer()
    {
        if (!IsAlive) return;
        if (PlayerPos.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1f, Trumperbody.transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-1f, Trumperbody.transform.localScale.y);
        }
    }
    private void SpawnPrefab()
    {
        if (!IsAlive) return;
        Instantiate(prefabToInstantiate, FireyPoint.position, FireyPoint.rotation);

    }
    void Death()
    {
        if (HitPoints <= 0)
        {
            IsAlive = false;
            Wizardanimator.SetTrigger("IsDead");
            //rigidbody23.velocity = deathKick;
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