using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;
    public Image healthUI;
    public Text scoreUI;
    public Text winScoreText;
    float maxHealthBar;
    float currentHealthBarPercent;
    RectTransform healthTransform;
    private TheHero hero;
    public Color greenColor;
    public Color redColor;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthTransform = healthUI.GetComponent<RectTransform>();
        maxHealthBar = healthTransform.sizeDelta.x;
        // currentHealthBarPercent = healthTransform.sizeDelta.x;
        hero = FindObjectOfType<TheHero>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ChangeHealthBar();
    //    healthUI.color = Color.Lerp(redColor, greenColor, currentHealthBarPercent);
        
    //    //scoreUI.text = "Score: " + hero.score.ToString();
    //    //winScoreText.text = "Your score: " + hero.score.ToString();
    //}

    public void ChangeHealthBar()
    {
        if (hero != null)
        {
            currentHealthBarPercent = hero.currHealth / hero.maxHealth;
            healthTransform.sizeDelta = new Vector2(currentHealthBarPercent * maxHealthBar, healthTransform.sizeDelta.y);
        }
    }

    public void ChangeHealthBar(float dis)
    {
        healthTransform.sizeDelta = new Vector2(dis * maxHealthBar, healthTransform.sizeDelta.y);
    }

    public void OnRestartGameButtonClick()
    {
        SceneManager.LoadScene("Level #1");
    }

    public void OnBackToStartMenuClick()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
