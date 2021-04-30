using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void LoadHouse()
    {
        SceneManager.LoadScene("HouseScene");
    }
    
    public void ToEditor()
    {
        SceneManager.LoadScene("CharacterEditor");
    }
    public void ToShop()
    {
        SceneManager.LoadScene("shopScene");
    }
    public void toParentConnect()
    {
        SceneManager.LoadScene("ParentConnectScene");
    }
}
