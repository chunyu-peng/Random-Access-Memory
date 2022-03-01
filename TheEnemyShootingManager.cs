using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyShootingManager : TheHeroShootingManager
{
    public override void Shoot()
    {
        GameObject bullet = Instantiate(pfBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
