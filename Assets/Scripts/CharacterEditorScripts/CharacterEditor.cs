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

    public GameObject cap;
    public SpriteRenderer caprenderer;
    public Sprite[] caps;
    public int ChosenCap;

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

        ChosenCap = PlayerPrefs.GetInt("Cap", 0);
        if (ChosenCap != 0)
        {
            caprenderer.sprite = caps[ChosenCap];
        }
        Data.Caps = caps;
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("HeadColor", ChosenHeadColor);
        PlayerPrefs.SetInt("SneakersColor", ChosenSneaker);
        PlayerPrefs.SetInt("Cap", ChosenCap);
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

    public void nextCap()
    {
        int capCount = caps.Length;
        if (ChosenCap < capCount - 1)
        {
            ChosenCap++;
        }
        else
        {
            ChosenCap = 0;
        }
        caprenderer.sprite = caps[ChosenCap];
    }

    public void prevCap()
    {
        int capCount = caps.Length;
        if (ChosenCap > 0)
        {
            ChosenCap--;
        }
        else
        {
            ChosenCap = capCount - 1;
        }
        caprenderer.sprite = caps[ChosenCap];
    }
}
