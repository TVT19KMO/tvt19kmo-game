using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControllerPlatformer : MonoBehaviour
{
    public static GameControllerPlatformer instance;

    public bool gamePaused = false;
    public GameObject PauseMenu;
    public GameObject UI;
    public GameObject GameOverMenu;
    public GameObject CompletedMenu;

    public AudioSource BackgroundMusic;
    public Text coinText;
    public Text gameOverText;
    public Text timerText;
    public Text completedText;
    private float timeValue = 0;
    private bool timerIsRunning = false;

    public int coins = 0;

    // Start is called before the first frame update
    void Awake()
    {
        timerIsRunning = true;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !gamePaused)
        {
            OpenMenu(PauseMenu);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && gamePaused)
        {
            CloseMenu(PauseMenu);
        }
        if(timerIsRunning)
        {
            timeValue += Time.deltaTime;
        }
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float secs = Mathf.FloorToInt(timeValue % 60);
        string currentTime = string.Format("{0:00}:{1:00}", minutes, secs);
        timerText.text = "Aika: " + currentTime;
    }

    public void PauseGame()
    {
        BackgroundMusic.Pause();
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    /*    if (gamePaused)
        {
            BackgroundMusic.Pause();
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
         /*   AudioSource[] audios = FindObjectsOfType<AudioSource>();
            foreach(AudioSource a in audios)
            {
                a.Pause();
            }
        }*/
    }
    public void unPauseGame()
    {
        PauseMenu.SetActive(false);
        BackgroundMusic.UnPause();
        Time.timeScale = 1;
    }

    public void OpenMenu(GameObject menu)
    {
        gamePaused = true;
        menu.SetActive(true);
        UI.SetActive(false);
    }

    public void CloseMenu(GameObject menu)
    {
        unPauseGame();
        gamePaused = false;
        menu.SetActive(false);
        UI.SetActive(true);
    }

    public void GameOver()
    {
        gamePaused = true;
        Time.timeScale = 0;
        BackgroundMusic.Pause();
        GameOverMenu.SetActive(true);
        gameOverText.text = "Peli ohi!\nKokonaispisteesi: " + coins;
    }
        public void GameCompleted()
    {
        gamePaused = true;
        Time.timeScale = 0;
        BackgroundMusic.Pause();
        CompletedMenu.SetActive(true);
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float secs = Mathf.FloorToInt(timeValue % 60);
        string competeTime = string.Format("{0:00}:{1:00}", minutes, secs);
        completedText.text = "Kenttä läpäisty!\nKokonaispisteet: " + coins + "\nAika: " + competeTime;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void toHouse()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HouseScene");
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Coins: " + coins);
        coinText.text = "Pisteet: " + coins;
    }
    public void StartTimer()
    {
        timerIsRunning = true;
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }
}