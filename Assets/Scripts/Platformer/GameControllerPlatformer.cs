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

    public int coins = 0;

    // Start is called before the first frame update
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

    public void GameOver()
    {
        gamePaused = true;
        Time.timeScale = 0;
        BackgroundMusic.Pause();
        gameOverText.text = "Peli ohi!\nKokonaispisteesi: " + coins;
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
