using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBoss : BaseEnemy
{
    public BaseEnemy[] enemies;
    public float spawnOffset;
    private float seventyHealth;
    private float fourtyHealth;
    private bool isStage2;
    private bool isStage3;
    public int attackDistance;
    private float attackTime;
    private float distance;
    public GameObject torso1;
    public GameObject torso2;
    public GameObject torso3;
    private bool canCount = true;
    private float maxHeal = 0;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hero = FindObjectOfType<TheHero>();

        health *= MenuUIManager.currentLevel;
        maxHeal = health;
        Debug.Log(maxHeal);
        seventyHealth = health * 0.7f;
        fourtyHealth = health * 0.4f;
        anim = GetComponent<Animator>();
        isStage2 = false;
        isStage3 = false;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            //anim.SetBool("isDizzy", false);
            //anim.SetBool("isAttacking", false);
            //anim.SetBool("isDoubleAttacking", false);
            distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackDistance)
            {
                if (Time.time >= attackTime)
                {
                    Debug.Log("atk2 : " + isStage2);
                    if (isStage2)
                    {
                        //anim.SetBool("isAttacking", true);
                        //StartCoroutine(Attack());
                        anim.SetTrigger("atk2");
                        attackTime = Time.time + timeBetweenAttack;
                    }
                    else if (isStage3)
                    {
                        //anim.SetBool("isDoubleAttacking", true);
                        //StartCoroutine(Attack());
                        anim.SetTrigger("atk3");
                        attackTime = Time.time + timeBetweenAttack;
                    }
                }
            }
        }
    }

    public void Attack()
    {
        hero.TakeDamage(damage);
        //yield return null;
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        health -= damageAmount;

        InGameUIManager.instance.ChangeHealthBar(health/maxHeal);

        //if (isStage3)
        //{
        //    int prob = Random.Range(1, 11);
        //    if (prob <= 2)
        //    {
        //        anim.SetBool("isDizzy", true);
        //    }
        //}
        int a = Random.Range(0, 2);
        if (health > 0 && a == 1)
        {
            BaseEnemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
        }

        //Debug.Log("HP : " + health + "  st2 is " + seventyHealth + "   st3 is " + fourtyHealth);

        if (health <= seventyHealth && !isStage2)
        {
            isStage2 = true;
            torso1.SetActive(false);
            torso2.SetActive(true);
            anim.SetBool("stage2", true);
            damage = 100;

            attackTime = Time.time + timeBetweenAttack;
        }

        if (health <= fourtyHealth && !isStage3)
        {
            isStage3 = true;
            torso2.SetActive(false);
            torso3.SetActive(true);
            anim.SetBool("stage2", false);
            anim.SetBool("stage3", true);
            damage = 150;
        }
        if (health <= 0)
        {
            if (canCount == true)
            {
                hero.KillMath();
                canCount = false;
            }

            player.GetComponent<TheHero>().bossIsOver = true;

            Destroy(this.gameObject);
            Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);
        }
    }
}
