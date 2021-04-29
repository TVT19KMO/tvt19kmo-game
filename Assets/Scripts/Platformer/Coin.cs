using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "PlayerPlatformer")
        {
            GameControllerPlatformer.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
