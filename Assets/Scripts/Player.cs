using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int headColor;
    public SpriteRenderer head;
    public SpriteRenderer sneakerRight;
    public SpriteRenderer sneakerLeft;
    public SpriteRenderer hairRenderer;
    public SpriteRenderer topRenderer;
    public SpriteRenderer leftArmRenderer;
    public SpriteRenderer rightArmRenderer;
    public SpriteRenderer bottomRenderer;
    public SpriteRenderer leftLegRenderer;
    public SpriteRenderer RightLegRenderer;


    void Start()
    {
        headColor = PlayerPrefs.GetInt("HeadColor", 0);
        string HColor = Data.HeadColors[headColor];
        Debug.Log("head color: " + headColor);
        int SneakersColor = PlayerPrefs.GetInt("SneakersColor", 1);
        int ChosenHat = PlayerPrefs.GetInt("Hat", 0);
        int ChosenJacket = PlayerPrefs.GetInt("Jacket", 0);
        int ChosenPants = PlayerPrefs.GetInt("Pants", 0);

        Color headclr;
        if(ColorUtility.TryParseHtmlString(HColor, out headclr))
        {
            head.color = headclr;
        }

        if(SneakersColor != 1)
        {
            sneakerLeft.sprite = Data.LeftSneakers[SneakersColor-1];
            sneakerRight.sprite = Data.RightSneakers[SneakersColor-1];
        }
        if(ChosenHat != 0)
        {
            hairRenderer.sprite = Data.Hats[ChosenHat];
        }
        if(ChosenJacket != 0)
        {
            topRenderer.sprite = Data.Tops[ChosenJacket];
            leftArmRenderer.sprite = Data.LeftArms[ChosenJacket];
            rightArmRenderer.sprite = Data.RightArms[ChosenJacket];
        }
        if(ChosenPants != 0)
        {
            bottomRenderer.sprite = Data.Bottoms[ChosenPants];
            leftLegRenderer.sprite = Data.LeftLegs[ChosenPants];
            RightLegRenderer.sprite = Data.RightLegs[ChosenPants];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
