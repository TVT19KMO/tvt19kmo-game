using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinCollision : MonoBehaviour
{
    public Tilemap coinTiles;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "PlayerPlatformer")
        {
            Vector3 hitPos = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPos.x = hit.point.x + 0.05f * hit.normal.x;
                hitPos.y = hit.point.y + 0.05f * hit.normal.y;
                coinTiles.SetTile(coinTiles.WorldToCell(hitPos), null);
            }
            GameControllerPlatformer.instance.AddCoin();
        }
    }
}