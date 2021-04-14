using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharBoxScript : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlayerHouse")
        {
            Debug.Log("Box Collision");
            SceneManager.LoadScene("CharacterEditor");
        }
    }
}
