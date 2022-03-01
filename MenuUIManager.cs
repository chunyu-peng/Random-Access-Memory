using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    private static bool isLoading = false;
    private static bool didRestart = false;
    public static int currentLevel;
    private static float loadingSceneTimer;
    private static float loadTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading == true)
        {
            if (loadingSceneTimer <= 0.0f)
            {
                if (didRestart == false)
                {
                    Debug.Log("Current Level: " + currentLevel);

                    string level = "Level #" + currentLevel.ToString();
                    SceneManager.LoadScene(level);
                    Debug.Log("First Time Loading!");
                }
                else
                {
                    string level = "Level #" + currentLevel.ToString();
                    SceneManager.LoadScene(level);
                    didRestart = false;
                    Debug.Log("Restart!");
                }

                isLoading = false;
            }

            loadingSceneTimer -= Time.deltaTime;
        }

        // Update the current level of the hero
        //currentLevel = TheHero.currLevel;
    }

    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene("Loading");
        isLoading = true;
        didRestart = false;

        currentLevel = 0;
        //TheHero.currLevel = 0;
        //TheHero.winKillCount = 3;

        loadingSceneTimer = loadTime;
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("Loading");
        isLoading = true;
        didRestart = true;

        loadingSceneTimer = loadTime;
    }

    public void OnQuitButtonClick()
    {
        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnLevelSelectClick()
    {
        SceneManager.LoadScene("Level Select");
    }    

    public void OnLevelSelectBackClick()
    {
        SceneManager.LoadScene("Start");
    }

    public void OnLevelSelectLeve0_1Click()
    {
        SceneManager.LoadScene("Loading");
        PlayerPrefs.DeleteAll();
        isLoading = true;
        didRestart = false;
        currentLevel = 0;
        //TheHero.currLevel = 0;
        //TheHero.winKillCount = 3;

        loadingSceneTimer = loadTime;
    }

    public static void OnLevelSelectLevel_1Click()
    {
        SceneManager.LoadScene("Loading");
        isLoading = true;
        didRestart = false;
        currentLevel = 1;
        //TheHero.currLevel = 1;
        //TheHero.winKillCount = 17;

        loadingSceneTimer = loadTime;
    }

    public static void GoToNextLevel(int level)
    {
        SceneManager.LoadScene("Loading");
        currentLevel = level;
        loadingSceneTimer = loadTime;
        isLoading = true;
        didRestart = false;
        Debug.Log("Current Level: " + currentLevel);
    }

    public static void BackToMain()
    {
        SceneManager.LoadScene("Start");
    }
}
