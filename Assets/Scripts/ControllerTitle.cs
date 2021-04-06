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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            LoadHouse();
        }
    }

    public void LoadHouse()
    {
        SceneManager.LoadScene("HouseScene", LoadSceneMode.Single);
    }
}
