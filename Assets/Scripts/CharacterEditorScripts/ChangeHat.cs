using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    public GameObject hairSlot;
    public SpriteRenderer caprenderer;
    public Sprite[] hats;
    public int ChosenHat;
    // Start is called before the first frame update
    void Start()
    {
        ChosenHat = PlayerPrefs.GetInt("Hat", 0);
        if(ChosenHat != 0)
        {
            caprenderer.sprite = hats[ChosenHat];
        }
        Data.Hats = hats;
    }
    public void nextHat()
    {
        int hatCount = hats.Length;
        if(ChosenHat < hatCount-1)
        {
            ChosenHat++;
        }
        else
        {
            ChosenHat = 0;
        }
        caprenderer.sprite = hats[ChosenHat];
    }
    public void prevHat()
    {
        int capCount = hats.Length;
        if(ChosenHat > 0)
        {
            ChosenHat--;
        }
        else
        {
            ChosenHat = capCount-1;
        }
        caprenderer.sprite = hats[ChosenHat];
    }
}
