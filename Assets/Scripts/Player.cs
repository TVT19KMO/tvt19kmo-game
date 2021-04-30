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
        headColor = PlayerPrefs.GetInt("HeadColor", 1);
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
            sneakerLeft.sprite = Data.OwnedLeftSneakers[SneakersColor-1];
            sneakerRight.sprite = Data.OwnedRightSneakers[SneakersColor-1];
        }
        if(ChosenHat != 0)
        {
            hairRenderer.sprite = Data.OwnedHats[ChosenHat];
        }
        if(ChosenJacket != 0)
        {
            topRenderer.sprite = Data.OwnedTops[ChosenJacket];
            leftArmRenderer.sprite = Data.OwnedLeftArms[ChosenJacket];
            rightArmRenderer.sprite = Data.OwnedRightArms[ChosenJacket];
        }
        if(ChosenPants != 0)
        {
            bottomRenderer.sprite = Data.OwnedBottoms[ChosenPants];
            leftLegRenderer.sprite = Data.OwnedLeftLegs[ChosenPants];
            RightLegRenderer.sprite = Data.OwnedRightLegs[ChosenPants];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
