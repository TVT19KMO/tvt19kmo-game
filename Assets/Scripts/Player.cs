using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject LSneaker;
    public GameObject RSneaker;
    public SpriteRenderer head;
    public SpriteRenderer sneakerRight;
    public SpriteRenderer sneakerLeft;


    void Start()
    {
        string headColor = "#" + PlayerPrefs.GetString("headColor", null);
        int SneakersColor = PlayerPrefs.GetInt("SneakersColor", 1);

        Color headclr;
        if(ColorUtility.TryParseHtmlString(headColor, out headclr))
        {
            head.color = headclr;
        }

        if(SneakersColor != 1)
        {
            LSneaker.SetActive(true);
            RSneaker.SetActive(true);
            sneakerLeft.sprite = Data.LeftSneakers[SneakersColor-1];
            sneakerRight.sprite = Data.RightSneakers[SneakersColor-1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
