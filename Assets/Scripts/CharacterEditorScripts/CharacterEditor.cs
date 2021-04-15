using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.UI;

public class CharacterEditor : MonoBehaviour
{
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
    string headColorToPref;

    public void Start()
    {
        string headColor = "#" + PlayerPrefs.GetString("headColor", null);
        Color headclr;
        if(ColorUtility.TryParseHtmlString(headColor, out headclr))
        {
            head.color = headclr;
        }
    }
    public void ChangePanelState(bool state)
    {
        panel.SetActive(state);
    }

    public void SaveCharacter()
    {
        Debug.Log("Add save here");
        PlayerPrefs.SetString("headColor", ColorUtility.ToHtmlStringRGBA(head.color));
        Debug.Log(ColorUtility.ToHtmlStringRGBA(head.color));
    }

    // Update is called once per frame
    void Update()
    {
        squareHeadDisplay.color = head.color;

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
    }

    public void ChangeHeadColor1()
    {
        whatColor = 1;
        string headColorToPref = ColorUtility.ToHtmlStringRGBA(color1);
    }
    public void ChangeHeadColor2()
    {
        whatColor = 2;
        string headColorToPref = ColorUtility.ToHtmlStringRGBA(color2);
    }
    public void ChangeHeadColor3()
    {
        whatColor = 3;
        string headColorToPref = ColorUtility.ToHtmlStringRGBA(color3);
    }
    public void ChangeHeadColor4()
    {
        whatColor = 4;
        string headColorToPref = ColorUtility.ToHtmlStringRGBA(color4);
    }
    public void ChangeHeadColor5()
    {
        whatColor = 5;
        string headColorToPref = ColorUtility.ToHtmlStringRGBA(color5);
    }
}
