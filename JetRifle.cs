using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetRifle : BaseEnemy
{
    public float runningDistance;
    public float jettingDistance;
    private float facing = 0.01f;
    private float attackTime;
    private float distance;
    private TheEnemyShootingManager shootManager;
    private bool canCount = true;

    void FixedUpdate()
    {
        if (player != null)
        {
            anim.SetBool("isHurting", false);
            anim.SetBool("isAiming", false);
            anim.SetBool("isShooting", false);
            distance = Vector2.Distance(transform.position, player.position);
            float dot = Vector3.Dot(transform.right, (player.position - transform.position).normalized);
            if (dot < facing)
            {
                transform.Rotate(0f, 180f, 0f);
            }
            if (distance >= jettingDistance)
            {
                anim.SetBool("isAccel", true);
                speed = 400;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distance >= runningDistance && Vector2.Distance(transform.position, player.position) < jettingDistance)
            {
                anim.SetBool("isRunning", true);
                speed = 200;
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isAccel", false);
                anim.SetBool("isRunning", false);
                speed = 0;
                if (Time.time >= attackTime)
                {
                    anim.SetBool("isAiming", true);
                    anim.SetBool("isShooting", true);
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
    }

    IEnumerator Attack()
    {
        shootManager = GetComponent<TheEnemyShootingManager>();
        shootManager.Shoot();
        yield return null;
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        health -= damageAmount;
        anim.SetBool("isHurting", true);
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);

            if (gameObject != null)
            {
                Destroy(gameObject, 1.000f);

                if (canCount == true)
                {
                    hero.KillMath();
                    canCount = false;
                }

                
                this.enabled = false;
            }
        }
    }
}
