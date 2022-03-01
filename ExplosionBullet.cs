/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroProjectile : MonoBehaviour
{
    // [SerializeField]
    private float currLifetime;
    public float projectileDamage;
    public float maxLifetime;
    
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        currLifetime = maxLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currLifetime > 0)
        {
            currLifetime -= Time.deltaTime;
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
            Instantiate(explosion, transform.position, Quaternion.identity);
            if (enemy != null)
            {
                Destroy(gameObject);
                enemy.TakeDamage(projectileDamage);
            }
        }     
    }
}*/
