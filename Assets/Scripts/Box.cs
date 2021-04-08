using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject GameMenu;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlayerHouse")
        {
            Debug.Log("Box Collision");
            GameControllerHouse.instance.OpenMenu(GameMenu);
        }
    }
}
