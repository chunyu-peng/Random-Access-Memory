using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TheHero : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveAmount;
    public Animator anim;
    public Joystick joystick;
    public GameObject restartPopup;
    public GameObject winPopup;
    [HideInInspector]
    public float currHealth;
    [HideInInspector]
    public bool m_FacingRight = true;
    [HideInInspector]
    public float deadAnimDuration = 0.0f;
    [HideInInspector]
    public bool noDamageTaken = false;

    public GameObject bloodEffect;
    public GameObject bloodShadow;
    public GameObject healthPickup;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;


    public float maxHealth;
    public float speed;

    public Text enemyCountText;

    int range;
    List<int> ranges = new List<int>();
    [HideInInspector]
    // Count how many enemies the hero kills so far
    public int killCount = 0;

    // How many enemies the hero should kill to win or go to the next level
    public int winKillCount;

    // Wait for a few seconds and then load the win scene
    public float winSceneTransitionTimer = 2.0f;

    public int currLevel = -1;

    public bool bossIsOver = false;

    public bool goToNextLevel = false;

    private void Start()
    {
        range = (int)maxHealth/ hearts.Length;
        int val = 0;
        
        for (int j = 0; j < hearts.Length; j++)
        {
            ranges.Add(val);
            //Debug.Log("val: ->" + val);
            val = val + range;
            
        }
        //Debug.Log(range);
        //Debug.Log(ranges);
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
        if(currLevel != 3)
        {
            for (int i = 0; i < 5; i++)
            {
                int x = UnityEngine.Random.Range(-500, 500);
                int y = UnityEngine.Random.Range(-100, 100);
                Instantiate(healthPickup, transform.position + new Vector3(x, y, 0), Quaternion.identity);
            }
        }
        
        enemyCountText.text = "Enemy : " + winKillCount;
        MenuUIManager.currentLevel = currLevel;

        //if (SceneManager.GetActiveScene().name == "Level #0")
        //{
        //    Debug.Log("Level #0");
        //    currLevel = 0;
        //    winKillCount = 3;
            
        //}
        //else if (SceneManager.GetActiveScene().name == "Level #1")
        //{
        //    Debug.Log("Level #1");
        //    currLevel = 1;
        //    winKillCount = 17;           
        //}
    }

    public void KillMath()
    {
        killCount++;
        if(killCount <= winKillCount)
            enemyCountText.text = "Enemy :" + ((winKillCount - killCount) >= 0?(winKillCount - killCount) : 0);
        else if(!bossIsOver)
            enemyCountText.text = "Enemy : boss";
        Debug.Log("need kill has " + (winKillCount - killCount));
    }

    private void Update()
    {
        if(currHealth > 0)
        {
//#if UNITY_EDITOR
            //Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//#else
            Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
//#endif

            // flip the character
            Vector3 characterScale = transform.localScale;
            if (moveInput.x > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (moveInput.x < 0 && m_FacingRight)
            {
                Flip();
            }
            transform.localScale = characterScale;

            moveAmount = moveInput.normalized * speed;

            // switch state idle <-> walks or run
            if (moveInput == Vector2.zero)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
            }
            else if (moveInput.x <= 0.2f && moveInput.x >= -0.2f && moveInput.y <= 0.2f && moveInput.y >= -0.2f)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", true);
            }
        }


        // When the hero dies, play the death animation and set the restart popup to active
        if (currHealth <= 0 && gameObject != null )
        {
            if(deadAnimDuration < 0.02f)
            {
                anim.SetTrigger("isDead");
                noDamageTaken = true;
            }
            else if (deadAnimDuration >= 1.6f)
            {
                // Store which level the hero is in currently
                MenuUIManager.currentLevel = currLevel;

                Destroy(gameObject);
                SceneManager.LoadScene("Lose");
            }

            deadAnimDuration += Time.deltaTime;
        }

        //Debug.Log("Kill Count: " + killCount);
        // When the hero wins, switch to the win scene
        if (killCount >= winKillCount && currLevel != -1 && bossIsOver)
        {
            noDamageTaken = true;

            if (winSceneTransitionTimer <= 0.0f)
            {
                // Store which level the hero is in currently
                MenuUIManager.currentLevel = currLevel;

                if (goToNextLevel)
                {
                    currLevel++;
                    MenuUIManager.GoToNextLevel(currLevel);
                }
                else
                {
                    SceneManager.LoadScene("Win");
                }             
            }
            else
            {
                winSceneTransitionTimer -= Time.deltaTime;
            }
        }

        // Check whether all enemies are killed
        /*
        GameObject[] objs = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        int count = 0;
        foreach (GameObject obj in objs)
        {
            if (obj.layer == 9)
            {
                count++;
            }
        }
        if (count == 0)
        {
            currLevel++;
            MenuUIManager.GoToNextLevel(currLevel);
        }
        */
    }

    private void FixedUpdate()
    {
        if(currHealth > 0)
            rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(float damageAmount)
    {
        if (noDamageTaken == false)
        {
            currHealth -= damageAmount;
            UpdateHealthUI(currHealth);
            anim.SetTrigger("isHurting");
            GameObject instantiatedObj = Instantiate(bloodEffect, bloodShadow.transform.position, Quaternion.identity);
            Destroy(instantiatedObj, 0.5f);
        }

        if (currHealth <= 0)
        {
            anim.SetTrigger("isDead");           
        }
    }
    public void Heal(int healAmount)
    {
        if (currHealth + healAmount > maxHealth)
        {
            currHealth = maxHealth;
        }
        else
        {
            currHealth += healAmount;
        }
        Debug.Log("Current Health: " + currHealth);
        UpdateHealthUI(currHealth);
    }

    public void UpdateHealthUI(float currentHeallth)
    {
        //Debug.Log(currHealth);
        for (int i = 0; i < ranges.Count; i++)
        {
            //Debug.Log(ranges[i]);
            if (ranges[i] <= currentHeallth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

}
