using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPlatformer player = other.GetComponent<PlayerPlatformer>();

        if (player != null)
        {
            GameControllerPlatformer.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
