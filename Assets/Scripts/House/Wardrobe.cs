using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject CharacterMenu;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlayerHouse")
        {
            GameControllerHouse.instance.OpenMenu(CharacterMenu);
        }
    }
}