using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public TheHero hero;
    [HideInInspector]
    public Vector2 heroPosition;
    [HideInInspector]
    public Vector2 enemyPosition;
    [HideInInspector]
    public Vector2 moveAmount;

    public GameObject deathEffect;

    public GameObject bloodEffect;

    public GameObject bloodShadow;


    // Enemy attributes
    public float health;
    public float speed;
    public float timeBetweenAttack;
    public float damage;

    [HideInInspector]
    public Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        hero = FindObjectOfType<TheHero>();
        rb = GetComponent<Rigidbody2D>();
    }



    public virtual void TakeDamage(float damageAmount) 
    {    
        GameObject instantiatedObj = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(instantiatedObj, 0.5f);
    }
}
