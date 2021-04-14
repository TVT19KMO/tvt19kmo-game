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

    public void ChangePanelState(bool state)
    {
        panel.SetActive(state);
    }

    public void SaveCharacter()
    {
        PrefabUtility.SaveAsPrefabAsset(character, "Assets/SpritesPlayer/Character/Character.prefab");
    }

    // Start is called before the first frame update
    void Start()
    {

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
    }
    public void ChangeHeadColor2()
    {
        whatColor = 2;
    }
    public void ChangeHeadColor3()
    {
        whatColor = 3;
    }
    public void ChangeHeadColor4()
    {
        whatColor = 4;
    }
    public void ChangeHeadColor5()
    {
        whatColor = 5;
    }
}
