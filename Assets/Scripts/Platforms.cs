using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerPlatformer>() != null)
        {
            //GameControllerPlatformer.instance.AddPoint();
        }
    }
}
