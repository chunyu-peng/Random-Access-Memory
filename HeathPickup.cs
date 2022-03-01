using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPickup : MonoBehaviour
{

    TheHero PlayerScript;

    public int healAmount;

    public GameObject soundObject;

    public bool useNext = false;
    public float showTime = 3;

    private void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TheHero>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerScript.Heal(healAmount);
            Instantiate(soundObject, transform.position, transform.rotation);
            if (useNext)
            {
                gameObject.SetActive(false);
                Invoke(nameof(ShowNext), showTime);
            }
            else
                Destroy(gameObject);
        }
    }

    private void ShowNext()
    {
        gameObject.SetActive(true);
    }
}
