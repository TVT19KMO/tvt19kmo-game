using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
 void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.name == "PlayerPlatformer")
        {
            Debug.Log("It's a trap!");
            GameControllerPlatformer.instance.GameOver();
        }
    }
}