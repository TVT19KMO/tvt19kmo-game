using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int headColor;
    public GameObject LSneaker;
    public GameObject RSneaker;
    public GameObject hair;
    public SpriteRenderer head;
    public SpriteRenderer sneakerRight;
    public SpriteRenderer sneakerLeft;
    public SpriteRenderer hairRenderer;


    void Start()
    {
        headColor = PlayerPrefs.GetInt("HeadColor", 0);
        string HColor = Data.HeadColors[headColor];
        Debug.Log("head color: " + headColor);
        int SneakersColor = PlayerPrefs.GetInt("SneakersColor", 1);
        int HairColor = PlayerPrefs.GetInt("Cap", 0);

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
        if(HairColor != 0)
        {
            hairRenderer.sprite = Data.Caps[HairColor];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
