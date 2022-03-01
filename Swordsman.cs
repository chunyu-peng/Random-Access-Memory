using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : BaseEnemy
{
    // Start is called before the first frame update
    public float attackRange;
    private float facingAngle = 0.01f;
    private float attackTime = 3;
    public Transform attackPoint;
    public float attackPointRange;
    public LayerMask heroLayer;
    public float hurtAnimTimer = 0.0f;
    private bool canCount = true;

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Get player's and enemy's position and forward vector
        if (hero != null)
        {
            heroPosition = hero.gameObject.transform.position;
        }
      
        enemyPosition = gameObject.transform.position;

        float distance = Vector3.Distance(heroPosition, enemyPosition);
        // Swordsman chases when it is not close enough to attack the hero
        if (hurtAnimTimer <= 0.0f)
        {
            if (distance >= attackRange)
            {
                gameObject.transform.position = Vector2.MoveTowards(enemyPosition, heroPosition, speed * Time.deltaTime);

                // Flip Swordsman's facing direction
                float dot = Vector3.Dot(transform.right, (heroPosition - enemyPosition).normalized);
                if (dot < facingAngle)
                {
                    gameObject.transform.Rotate(0f, 180f, 0f);
                }

                anim.SetBool("isRunning", true);
            }
            // Swordsman attack the hero when the hero is within attack range
            else
            {
                anim.SetBool("isRunning", false);

                if (Time.time >= attackTime)
                {
                    // Attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttack;
                }
            }
        }
        else
        {
            hurtAnimTimer -= Time.deltaTime;
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        health -= damageAmount;

        if (health <= 0)
        {
            speed = 0.0f;
            damage = 0.0f;

            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isDead", true);
            Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);

            if (gameObject != null)
            {               
                Destroy(gameObject, 1.111f);

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
        if (hero != null)
        {
            if (hero.currHealth > 0)
            {
                // Play Swordsman's attack animation
                anim.SetTrigger("isAttacking");

                // Detect hero in range of attack
                Collider2D[] heroCollider = Physics2D.OverlapCircleAll(attackPoint.position, 150.0f, heroLayer);

                // Do damage
                if (heroCollider.Length > 0)
                {                   
                    yield return new WaitForSeconds(0.778f);
                    hero.TakeDamage(damage);
                }
            }
            else
            {
                Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);
            }
        }

        yield return null;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackPointRange);
        }      
    }
}
