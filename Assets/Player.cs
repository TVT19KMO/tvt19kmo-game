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
    public Sprite[] LeftSneakers;
    public Sprite[] RightSneakers;

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
            sneakerRight.sprite = LeftSneakers[SneakersColor-1];
            sneakerLeft.sprite = RightSneakers[SneakersColor-1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
