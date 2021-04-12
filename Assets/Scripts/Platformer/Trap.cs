using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerPlatformer player = other.GetComponent<PlayerPlatformer>();

        if (player != null)
        {
            //the controller will take care of ignoring the damage during the invincibility time.
            player.ChangeHealth(-1);
        }
    }
}
