using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerHouse : MonoBehaviour
{
    public static GameControllerHouse instance;
    public GameObject UI;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void LoadPlatformer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("DemoScene", LoadSceneMode.Single);
    }

    public void LoadCharacterEditor()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CharacterEditor", LoadSceneMode.Single);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }

    public void OpenMenu(GameObject menu)
    {
        Time.timeScale = 0;
        UI.SetActive(false);
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1;
    }
}
