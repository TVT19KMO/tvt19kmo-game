using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

 void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.name == "PlayerPlatformer")
        {
            Debug.Log("Maalissa!");
            GameControllerPlatformer.instance.GameCompleted();
        }
    }
}
