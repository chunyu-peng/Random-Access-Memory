using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{

    public float stopDistance;

    private float attackTime;

    public float attackSpeed;

    public float detectRange;

    private float facingAngle = 0.01f;

    public float deathtime = 3;

    private TheEnemyShootingManager shootManager;

    public float hurtAnimTimer = 0.0f;

    private bool canCount = true;

    private void FixedUpdate()
    {
        anim.SetBool("isDizzy", false);
        anim.SetBool("isShooting", false);
        if (FindObjectOfType<TheHero>() != null)
        {
            heroPosition = player.position;
        }
        enemyPosition = gameObject.transform.position;

        if (player != null)
        {
            float distance = Vector2.Distance(heroPosition, enemyPosition);

            if (hurtAnimTimer <= 0.0f)
            {
                if (distance <= detectRange)
                {
                    if (distance > stopDistance)
                    {
                        anim.SetBool("isRunning", true);
                        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        float dot = Vector3.Dot(transform.right, (heroPosition - enemyPosition).normalized);
                        if (dot < facingAngle)
                        {
                            gameObject.transform.Rotate(0f, 180f, 0f);
                        }
                    }
                    else if (distance <= stopDistance)
                    {
                        anim.SetBool("isRunning", false);
                        if (Time.time >= attackTime)
                        {
                            anim.SetBool("isShooting", true);
                            StartCoroutine(Attack());
                            attackTime = Time.time + timeBetweenAttack;
                        }
                    }
                    if (player.GetComponent<TheHero>() == null)
                    {
                        anim.SetBool("isShooting", false);
                        anim.SetBool("isRunning", false);
                    }
                    if (hero == null)
                    {
                        anim.SetBool("isShooting", false);
                        anim.SetBool("isRunning", false);
                    }
                }
            }
            else
            {
                hurtAnimTimer -= Time.deltaTime;
            }
        }

        if (deathtime >0)
        {
            deathtime -= 1;
        }
        else
        {
            //Destroy(bloodEffect);
        }
    }



    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        health -= damageAmount;
        
        if (health <= 0)
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isDizzy", true);
            anim.SetTrigger("isDead");
            Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);

            if (gameObject != null)
            {
                Destroy(gameObject, 0.8333f);

                if (canCount == true)
                {
                    hero.KillMath();
                    canCount = false;
                }

                this.enabled = false;
            }
        }
        else
        {
            anim.SetTrigger("isHurting");
            hurtAnimTimer = 0.667f;
        }       
    }

    IEnumerator Attack()
    {
        shootManager = GetComponent<TheEnemyShootingManager>();
        shootManager.Shoot();
        yield return null;
    }
}
