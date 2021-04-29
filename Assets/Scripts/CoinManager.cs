using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else 
        {
            Destroy(gameObject); 
        }
    }

    public int Coins;

    public void UseCoins (int amount)
    {
        Coins -= amount;
    }
    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

}
