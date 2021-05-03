using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject GameMenu;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PlayerPlatformer")
        {
            GameControllerPlatformer.instance.GameOver(true);
        }
    }
}
