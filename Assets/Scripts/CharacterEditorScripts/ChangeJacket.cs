using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeJacket : MonoBehaviour
{
    public SpriteRenderer LeftArm;
    public SpriteRenderer RightArm;
    public SpriteRenderer Top;
    Sprite[] larms;
    Sprite[] rarms;
    Sprite[] tops;
    public int ChosenJacket;
    void Start()
    {
        tops = Data.Tops;
        larms = Data.LeftArms;
        rarms = Data.RightArms;
        ChosenJacket = PlayerPrefs.GetInt("Jacket", 0);
        if(ChosenJacket != 0)
        {
            LeftArm.sprite = larms[ChosenJacket];
            RightArm.sprite = rarms[ChosenJacket];
            Top.sprite = tops[ChosenJacket];
        }
    }
    public void nextJacket()
    {
        if(ChosenJacket < tops.Length -1)
        {
            ChosenJacket++;
        }
        else
        {
            ChosenJacket = 0;
        }
        LeftArm.sprite = larms[ChosenJacket];
        RightArm.sprite = rarms[ChosenJacket];
        Top.sprite = tops[ChosenJacket];
    }
    public void prevJacket()
    {
        if(ChosenJacket > 0)
        {
            ChosenJacket--;
        }
        else
        {
            ChosenJacket = tops.Length -1;
        }
        LeftArm.sprite = larms[ChosenJacket];
        RightArm.sprite = rarms[ChosenJacket];
        Top.sprite = tops[ChosenJacket];
    }
}
