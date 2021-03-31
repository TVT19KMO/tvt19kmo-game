using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerHouse : MonoBehaviour
{
    public static GameControllerHouse instance;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlatformer()
    {
        SceneManager.LoadScene("PlatformerScene", LoadSceneMode.Single);
    }
}
