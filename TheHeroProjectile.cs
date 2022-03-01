using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHeroProjectile : MonoBehaviour
{
    // [SerializeField]
    private float currLifetime;
    public float projectileDamage;
    public float maxLifetime;

    public GameObject soundObject;



    // Start is called before the first frame update
    void Start()
    {
        currLifetime = maxLifetime;      
        GameObject instantiatedObj = Instantiate(soundObject, transform.position, transform.rotation);
        Destroy(instantiatedObj, 0.5f);
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
        //Debug.Log("damage");
        if (collision.tag == "Enemy" || collision.tag == "Mech")
        {
            BaseEnemy enemy = collision.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                Destroy(gameObject);
                enemy.TakeDamage(projectileDamage);
            }
        }
        else if (collision.tag == "Player")
        {
            Debug.Log("player damage");
            TheHero hero = collision.GetComponent<TheHero>();
            if (hero != null)
            {
                Destroy(gameObject);
                hero.TakeDamage(projectileDamage);
            }

        }
        else if (collision.CompareTag("NewBoss"))
        {
            Destroy(gameObject);
            collision.GetComponent<NewBossHealth>().GetDamage(projectileDamage);
        }
    }
}
