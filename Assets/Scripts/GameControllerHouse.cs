using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerHouse : MonoBehaviour
{
    public static GameControllerHouse instance;
    public bool gamePaused = true;
    public GameObject UI;

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

        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !gamePaused)
        {
            gamePaused = true;
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && gamePaused)
        {
            gamePaused = false;
            PauseGame();
        }
    }

    public void LoadPlatformer()
    {
        SceneManager.LoadScene("PlatformerScene", LoadSceneMode.Single);
    }

    void PauseGame()
    {
        if (gamePaused)
        {
            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }
}
