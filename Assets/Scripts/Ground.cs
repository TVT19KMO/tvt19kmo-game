using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerPlatformer player = other.gameObject.GetComponent<PlayerPlatformer>();

        if (player != null)
        {
            //GameControllerPlatformer.instance.GameOver();
        }
    }
}
