using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroShootingManager : MonoBehaviour
{
    public Transform firePoint;
    public GameObject pfBullet;
    public Animator anim;
    public float bulletForce;
    private float shootInterval;
    public float shootTimer;
    public bool canShoot;

    Animator cameraAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shootInterval = 0.25f;
        shootTimer = shootInterval;
        canShoot = true;
        bulletForce = 600f;
        cameraAnim = Camera.main.GetComponent<Animator>();
    }

    public virtual void Shoot()
    {
        // When player presses the button, play shooting animation and shoot the bullet
        UpdateShootingTimer();
        if (canShoot)
        {
            shootTimer = 0.0f;

            if (anim.GetBool("isRunning"))
            {
                anim.SetTrigger("isRunningShooting");
            }
            else
            {
                anim.SetTrigger("isShooting");
            }
            GameObject bullet = Instantiate(pfBullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            shootTimer = Time.time;
            cameraAnim.SetTrigger("shake");
            
        }
    }

    public void UpdateShootingTimer()
    {
        shootTimer = Time.time - shootTimer;

        if (shootTimer < shootInterval)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }
}
