using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerPlatformer : MonoBehaviour
{
    public static GameControllerPlatformer instance;

    public bool gamePaused = false;
    public GameObject UI;
    public GameObject GameOverMenu;

    public AudioSource BackgroundMusic;
    public Text coinText;
    public Text gameOverText;
    public Text timeText;

    public int coins = 0;
    public int totalCoins;

    public float maxtime;
    private float timer = 0;
    private int timerStars = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (gamePaused == false)
        {
            timer += Time.deltaTime;
            timeText.text = "Aika: " + Mathf.Round(timer) + " / " + maxtime;
            if (timer >= maxtime)
            {
                GameOver(false);
            }
        }
        
    }

    public void OpenMenu(GameObject menu)
    {
        gamePaused = true;
        Time.timeScale = 0;
        menu.SetActive(true);
        UI.SetActive(false);
    }

    public void CloseMenu(GameObject menu)
    {
        gamePaused = false;
        Time.timeScale = 1;
        menu.SetActive(false);
        UI.SetActive(true);
    }

    public void GameOver(bool gameFinished)
    {
        gamePaused = true;
        Time.timeScale = 0;
        BackgroundMusic.Pause();

        if (gameFinished)
        {
            if (timer/maxtime <= 0.25)
            {
                timerStars = 3;
            }
            else if (timer/maxtime <= 0.5)
            {
                timerStars = 2;
            }
            else if (timer/maxtime <= 0.75)
            {
                timerStars = 1;
            }

            gameOverText.text = "Kolikot: " + coins + " / " + totalCoins +
            "\nAika: " + timerStars + "* / 3*";
        }
        else
        {
            gameOverText.text = "Kolikot: " + coins + " / " + totalCoins +
            "\nAika: 0* / 3*";
        }

        GameOverMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToHouse()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HouseScene", LoadSceneMode.Single);
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins: " + coins);
        coinText.text = "Pisteet: " + coins;
    }
}
