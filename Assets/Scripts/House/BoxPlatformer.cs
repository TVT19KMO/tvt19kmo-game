using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlatformer : MonoBehaviour
{
    public GameObject GameMenu;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlayerHouse")
        {
            GameControllerHouse.instance.OpenMenu(GameMenu);
        }
    }
}
