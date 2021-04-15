using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerHouse : MonoBehaviour
{
    public static GameControllerHouse instance;
    public bool gamePaused = false;
    public GameObject PauseMenu;
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
    }

    public void LoadPlatformer()
    {
        SceneManager.LoadScene("DemoScene", LoadSceneMode.Single);
    }

    public void LoadCharacterEditor()
    {
        SceneManager.LoadScene("CharacterEditor", LoadSceneMode.Single);
    }

    public void OpenMenu(GameObject menu)
    {
        gamePaused = true;
        menu.SetActive(true);
        UI.SetActive(false);
    }

    public void CloseMenu(GameObject menu)
    {
        gamePaused = false;
        menu.SetActive(false);
        UI.SetActive(true);
    }
}
