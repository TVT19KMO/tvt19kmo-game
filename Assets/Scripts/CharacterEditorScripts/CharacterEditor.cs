using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    // Hahmon määritys, jotta voidaan vaihdella editorissa näkyvän hahmon vaatteita
    public GameObject character;
    public SpriteRenderer head;
    public SpriteRenderer caprenderer;
    public SpriteRenderer LeftArm;
    public SpriteRenderer RightArm;
    public SpriteRenderer Top;
    public SpriteRenderer LeftLeg;
    public SpriteRenderer RightLeg;
    public SpriteRenderer Bottom;
    public SpriteRenderer left_sneaker;
    public SpriteRenderer right_sneaker;
    // Arrayt, joihin haetaan datasta spritet. Hakee nyt kaikki, myöhemmin dataan lisättävä arrayt joissa vain omistetut itemit
    public string[] headColors;
    Sprite[] hats;
    Sprite[] larms;
    Sprite[] rarms;
    Sprite[] tops;
    Sprite[] LeftLegs;
    Sprite[] RightLegs;
    Sprite[] Bottoms;
    Sprite[] leftSneakers;
    Sprite[] rightSneakers;
    public Image squareHeadDisplay;
    // Playerpreffeihin tallennettavat arvot, jotta muokattu hahmo säilyy samanlaisena pelisessioiden välillä
    public int ChosenHeadColor;
    public int ChosenHat;
    public int ChosenJacket;
    public int ChosenPants;
    public int ChosenSneaker;

    public void Start()
    {
        // Haetaan ensin data.cs tiedostosta spritet
        hats = Data.OwnedHats;
        tops = Data.OwnedTops;
        larms = Data.OwnedLeftArms;
        rarms = Data.OwnedRightArms;
        Bottoms = Data.OwnedBottoms;
        RightLegs = Data.OwnedRightLegs;
        LeftLegs = Data.OwnedLeftLegs;
        leftSneakers = Data.OwnedLeftSneakers;
        rightSneakers = Data.OwnedRightSneakers;
        // Tarkistetaan onko laitteella tallennettua hahmoa, ellei ole niin käytetään oletusarvoja
        ChosenHeadColor = PlayerPrefs.GetInt("HeadColor", 1);
        ChosenHat = PlayerPrefs.GetInt("Hat", 0);
        ChosenJacket = PlayerPrefs.GetInt("Jacket", 0);
        ChosenPants = PlayerPrefs.GetInt("Pants", 0);
        ChosenSneaker = PlayerPrefs.GetInt("SneakersColor", 1);
        Color HColor;
        // Rakennetaan hahmoeditoriin hahmo tallennetuilla / oletusarvoilla
        if(ColorUtility.TryParseHtmlString(headColors[ChosenHeadColor], out HColor))
        {
            Debug.Log("toimii");
            head.color = HColor;
        }
        if(ChosenHat != 0)
        {
            caprenderer.sprite = hats[ChosenHat];
        }
        if(ChosenJacket != 0)
        {
            LeftArm.sprite = larms[ChosenJacket];
            RightArm.sprite = rarms[ChosenJacket];
            Top.sprite = tops[ChosenJacket];
        }
        if(ChosenPants != 0)
        {
            LeftLeg.sprite = LeftLegs[ChosenPants];
            RightLeg.sprite = RightLegs[ChosenPants];
            Bottom.sprite = Bottoms[ChosenPants];
        }
        if(ChosenSneaker != 1)
        {
            left_sneaker.sprite = leftSneakers[ChosenSneaker-1];
            right_sneaker.sprite = rightSneakers[ChosenSneaker-1];
        }
        Data.HeadColors = headColors;
    }
    /* Hahmoeditorien napeille omat metodit, nappien painelu siis looppaa datasta haettua arraytä läpi ja vaihtaa
    scenessä näkyvän hahmon vaatteita*/ 
    public void prevHeadColor()
    {
        if(ChosenHeadColor > 0)
        {
            ChosenHeadColor--;
            setHeadColor();
        }
        else
        {
            ChosenHeadColor = headColors.Length-1;
            setHeadColor();
        }
    }
    public void nextHeadColor()
    {
        if(ChosenHeadColor < headColors.Length-1)
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
        if(ColorUtility.TryParseHtmlString(headColors[ChosenHeadColor], out HColor))
        {
            Debug.Log(ChosenHeadColor);
            head.color = HColor;
        }
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
    public void NextSneakers()
    {
        int SneakerCount = leftSneakers.Length;
        if(ChosenSneaker < SneakerCount)
        {
            ChosenSneaker++;
        }
        else
        {
            ChosenSneaker = 1;
        }
        left_sneaker.sprite = leftSneakers[ChosenSneaker-1];
        right_sneaker.sprite = rightSneakers[ChosenSneaker-1];
        Debug.Log(ChosenSneaker);
    }

    public void PrevSneakers()
    {
        int SneakerCount = leftSneakers.Length;
        if(ChosenSneaker > 1)
        {
            ChosenSneaker--;
        }
        else
        {
            ChosenSneaker = SneakerCount;
        }
        left_sneaker.sprite = leftSneakers[ChosenSneaker-1];
        right_sneaker.sprite = rightSneakers[ChosenSneaker-1];
        Debug.Log(ChosenSneaker);
    }
}
