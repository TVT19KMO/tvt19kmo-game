using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePants : MonoBehaviour
{
    public SpriteRenderer LeftLeg;
    public SpriteRenderer RightLeg;
    public SpriteRenderer Bottom;
    public Sprite[] LeftLegs;
    public Sprite[] RightLegs;
    public Sprite[] Bottoms;
    public int ChosenPants;
    void Start()
    {
        ChosenPants = PlayerPrefs.GetInt("Pants", 0);
        if(ChosenPants != 0)
        {
            LeftLeg.sprite = LeftLegs[ChosenPants];
            RightLeg.sprite = RightLegs[ChosenPants];
            Bottom.sprite = Bottoms[ChosenPants];
        }
        Data.Bottoms = Bottoms;
        Data.LeftLegs = LeftLegs;
        Data.RightLegs = RightLegs;
    }

    public void nextPants()
    {
        if(ChosenPants < Bottoms.Length-1)
        {
            ChosenPants++;
        }
        else
        {
            ChosenPants = 0;
        }
        LeftLeg.sprite = LeftLegs[ChosenPants];
        RightLeg.sprite = RightLegs[ChosenPants];
        Bottom.sprite = Bottoms[ChosenPants];
    }
    public void prevPants()
    {
        if(ChosenPants > 0)
        {
            ChosenPants--;
        }
        else
        {
            ChosenPants = Bottoms.Length -1;
        }
        LeftLeg.sprite = LeftLegs[ChosenPants];
        RightLeg.sprite = RightLegs[ChosenPants];
        Bottom.sprite = Bottoms[ChosenPants];
    }
    void Update()
    {
        
    }
}
