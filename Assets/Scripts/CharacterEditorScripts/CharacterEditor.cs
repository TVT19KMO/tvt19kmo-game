using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    public static CharacterEditor instance;

    public GameObject panel;
    public GameObject character;

    public SpriteRenderer head;
    public Image squareHeadDisplay;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public int whatColor;

    public GameObject LSneaker;
    public GameObject RSneaker;
    public SpriteRenderer left_sneaker;
    public SpriteRenderer right_sneaker;
    public Sprite[] spriteArray_left;
    public Sprite[] spriteArray_right;

    public int ChosenSneaker;

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
        string headColor = "#" + PlayerPrefs.GetString("headColor", null);
        Color headclr;
        if (ColorUtility.TryParseHtmlString(headColor, out headclr))
        {
            head.color = headclr;
        }

        ChosenSneaker = PlayerPrefs.GetInt("SneakersColor", 1);
        if (ChosenSneaker != 1)
        {
            LSneaker.SetActive(true);
            RSneaker.SetActive(true);
            left_sneaker.sprite = spriteArray_left[ChosenSneaker - 1];
            right_sneaker.sprite = spriteArray_right[ChosenSneaker - 1];
        }
    }

    public void ChangePanelState(bool state)
    {
        panel.SetActive(state);
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetString("headColor", ColorUtility.ToHtmlStringRGBA(head.color));
        PlayerPrefs.SetInt("SneakersColor", ChosenSneaker);
        Debug.Log(ColorUtility.ToHtmlStringRGBA(head.color));
    }

    public void ChangeHeadColor(int color)
    {
        whatColor = color;

        if (whatColor == 1)
        {
            head.color = color1;
        }
        else if (whatColor == 2)
        {
            head.color = color2;
        }
        else if (whatColor == 3)
        {
            head.color = color3;
        }
        else if (whatColor == 4)
        {
            head.color = color4;
        }
        else if (whatColor == 5)
        {
            head.color = color5;
        }

        squareHeadDisplay.color = head.color;
    }

    public void NextSneakers()
    {
        int SneakerCount = spriteArray_left.Length;
        if (ChosenSneaker < SneakerCount)
        {
            ChosenSneaker++;
            LSneaker.SetActive(true);
            RSneaker.SetActive(true);
        }
        else
        {
            ChosenSneaker = 1;
            LSneaker.SetActive(false);
            RSneaker.SetActive(false);
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
            if (ChosenSneaker == 1)
            {
                LSneaker.SetActive(false);
                RSneaker.SetActive(false);
            }
        }
        else
        {
            ChosenSneaker = SneakerCount;
            LSneaker.SetActive(true);
            RSneaker.SetActive(true);
        }
        left_sneaker.sprite = spriteArray_left[ChosenSneaker - 1];
        right_sneaker.sprite = spriteArray_right[ChosenSneaker - 1];
        Debug.Log(ChosenSneaker);
    }
}
