using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerTitle : MonoBehaviour
{
    public static ControllerTitle instance;

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

    public void LoadCharacterEditor()
    {
        SceneManager.LoadScene("CharacterEditor", LoadSceneMode.Single);
    }
}
