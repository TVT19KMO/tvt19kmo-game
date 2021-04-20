using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
    public GameObject character;
    
    public string[] headColors;

    public SpriteRenderer head;
    public Image squareHeadDisplay;
    public int ChosenHeadColor;

    public void Start()
    {
        ChosenHeadColor = PlayerPrefs.GetInt("HeadColor", 1);
        Debug.Log(ChosenHeadColor);
        Color HColor;
        if(ColorUtility.TryParseHtmlString(headColors[ChosenHeadColor], out HColor))
        {
            Debug.Log("toimii");
            head.color = HColor;
        }
        Data.HeadColors = headColors;
    }

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

    void Update()
    {

    }
}
