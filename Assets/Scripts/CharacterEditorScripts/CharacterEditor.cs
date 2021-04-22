using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    public static CharacterEditor instance;

    public GameObject character;

    public string[] headColors;

    public SpriteRenderer head;
    public int ChosenHeadColor;

    public GameObject LSneaker;
    public GameObject RSneaker;
    public SpriteRenderer left_sneaker;
    public SpriteRenderer right_sneaker;
    public Sprite[] spriteArray_left;
    public Sprite[] spriteArray_right;
    public int ChosenSneaker;

    public GameObject hairSlot;
    public SpriteRenderer caprenderer;
    public Sprite[] hats;
    public int ChosenHat;

    public SpriteRenderer LeftArm;
    public SpriteRenderer RightArm;
    public SpriteRenderer Top;
    public Sprite[] larms;
    public Sprite[] rarms;
    public Sprite[] tops;
    public int ChosenJacket;

    public SpriteRenderer LeftLeg;
    public SpriteRenderer RightLeg;
    public SpriteRenderer Bottom;
    public Sprite[] LeftLegs;
    public Sprite[] RightLegs;
    public Sprite[] Bottoms;
    public int ChosenPants;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        ChosenHeadColor = PlayerPrefs.GetInt("HeadColor", 1);
        Debug.Log(ChosenHeadColor);
        Color HColor;
        if (ColorUtility.TryParseHtmlString(headColors[ChosenHeadColor], out HColor))
        {
            Debug.Log("toimii");
            head.color = HColor;
        }
        Data.HeadColors = headColors;

        ChosenSneaker = PlayerPrefs.GetInt("SneakersColor", 1);
        if (ChosenSneaker != 1)
        {
            left_sneaker.sprite = spriteArray_left[ChosenSneaker - 1];
            right_sneaker.sprite = spriteArray_right[ChosenSneaker - 1];
        }
        Data.LeftSneakers = spriteArray_left;
        Data.RightSneakers = spriteArray_right;

        ChosenHat = PlayerPrefs.GetInt("Hat", 0);
        if (ChosenHat != 0)
        {
            caprenderer.sprite = hats[ChosenHat];
        }
        Data.Hats = hats;

        ChosenJacket = PlayerPrefs.GetInt("Jacket", 0);
        if (ChosenJacket != 0)
        {
            LeftArm.sprite = larms[ChosenJacket];
            RightArm.sprite = rarms[ChosenJacket];
            Top.sprite = tops[ChosenJacket];
        }
        Data.Tops = tops;
        Data.LeftArms = larms;
        Data.RightArms = rarms;

        ChosenPants = PlayerPrefs.GetInt("Pants", 0);
        if (ChosenPants != 0)
        {
            LeftLeg.sprite = LeftLegs[ChosenPants];
            RightLeg.sprite = RightLegs[ChosenPants];
            Bottom.sprite = Bottoms[ChosenPants];
        }
        Data.Bottoms = Bottoms;
        Data.LeftLegs = LeftLegs;
        Data.RightLegs = RightLegs;
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("HeadColor", ChosenHeadColor);
        PlayerPrefs.SetInt("SneakersColor", ChosenSneaker);
        PlayerPrefs.SetInt("Hat", ChosenHat);
        PlayerPrefs.SetInt("Jacket", ChosenJacket);
        PlayerPrefs.SetInt("Pants", ChosenPants);
    }

    public void prevHeadColor()
    {
        if (ChosenHeadColor > 0)
        {
            ChosenHeadColor--;
            setHeadColor();
        }
        else
        {
            ChosenHeadColor = headColors.Length - 1;
            setHeadColor();
        }
    }
    public void nextHeadColor()
    {
        if (ChosenHeadColor < headColors.Length - 1)
        {
            ChosenHeadColor++;
            setHeadColor();
        }
        else
        {
            ChosenHeadColor = 0;
            setHeadColor();
        }
    }

    public void setHeadColor()
    {
        Color HColor;
        if (ColorUtility.TryParseHtmlString(headColors[ChosenHeadColor], out HColor))
        {
            Debug.Log(ChosenHeadColor);
            head.color = HColor;
        }
    }

    public void NextSneakers()
    {
        int SneakerCount = spriteArray_left.Length;
        if (ChosenSneaker < SneakerCount)
        {
            ChosenSneaker++;
        }
        else
        {
            ChosenSneaker = 1;
        }
        left_sneaker.sprite = spriteArray_left[ChosenSneaker - 1];
        right_sneaker.sprite = spriteArray_right[ChosenSneaker - 1];
        Debug.Log(ChosenSneaker);
    }

    public void PrevSneakers()
    {
        int SneakerCount = spriteArray_left.Length;
        if (ChosenSneaker > 1)
        {
            ChosenSneaker--;
        }
        else
        {
            ChosenSneaker = SneakerCount;
        }
        left_sneaker.sprite = spriteArray_left[ChosenSneaker - 1];
        right_sneaker.sprite = spriteArray_right[ChosenSneaker - 1];
        Debug.Log(ChosenSneaker);
    }

    public void nextHat()
    {
        int hatCount = hats.Length;
        if (ChosenHat < hatCount - 1)
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
        if (ChosenHat > 0)
        {
            ChosenHat--;
        }
        else
        {
            ChosenHat = capCount - 1;
        }
        caprenderer.sprite = hats[ChosenHat];
    }

    public void nextJacket()
    {
        if (ChosenJacket < tops.Length - 1)
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
        if (ChosenJacket > 0)
        {
            ChosenJacket--;
        }
        else
        {
            ChosenJacket = tops.Length - 1;
        }
        LeftArm.sprite = larms[ChosenJacket];
        RightArm.sprite = rarms[ChosenJacket];
        Top.sprite = tops[ChosenJacket];
    }

    public void nextPants()
    {
        if (ChosenPants < Bottoms.Length - 1)
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
        if (ChosenPants > 0)
        {
            ChosenPants--;
        }
        else
        {
            ChosenPants = Bottoms.Length - 1;
        }
        LeftLeg.sprite = LeftLegs[ChosenPants];
        RightLeg.sprite = RightLegs[ChosenPants];
        Bottom.sprite = Bottoms[ChosenPants];
    }
}
