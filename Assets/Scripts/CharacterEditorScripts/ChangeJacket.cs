using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeJacket : MonoBehaviour
{
    public SpriteRenderer LeftArm;
    public SpriteRenderer RightArm;
    public SpriteRenderer Top;
    public Sprite[] larms;
    public Sprite[] rarms;
    public Sprite[] tops;
    public int ChosenJacket;
    void Start()
    {
        ChosenJacket = PlayerPrefs.GetInt("Jacket", 0);
        if(ChosenJacket != 0)
        {
            LeftArm.sprite = larms[ChosenJacket];
            RightArm.sprite = rarms[ChosenJacket];
            Top.sprite = tops[ChosenJacket];
        }
        Data.Tops = tops;
        Data.LeftArms = larms;
        Data.RightArms = rarms;
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
