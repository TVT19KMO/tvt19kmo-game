using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSneakers : MonoBehaviour
{
    public GameObject LSneaker;
    public GameObject RSneaker;
    public SpriteRenderer left_sneaker;
    public SpriteRenderer right_sneaker;
    public Sprite[] spriteArray_left;
    public Sprite[] spriteArray_right;

    public int ChosenSneaker;

    // Start is called before the first frame update
    void Start()
    {
        ChosenSneaker = PlayerPrefs.GetInt("SneakersColor", 1);
        if(ChosenSneaker != 1)
        {
            left_sneaker.sprite = spriteArray_left[ChosenSneaker-1];
            right_sneaker.sprite = spriteArray_right[ChosenSneaker-1];
        }
        Data.LeftSneakers = spriteArray_left;
        Data.RightSneakers = spriteArray_right;
    }
    public void NextSneakers()
    {
        int SneakerCount = spriteArray_left.Length;
        if(ChosenSneaker < SneakerCount)
        {
            ChosenSneaker++;
        }
        else
        {
            ChosenSneaker = 1;
        }
        left_sneaker.sprite = spriteArray_left[ChosenSneaker-1];
        right_sneaker.sprite = spriteArray_right[ChosenSneaker-1];
        Debug.Log(ChosenSneaker);
    }

    public void PrevSneakers()
    {
        int SneakerCount = spriteArray_left.Length;
        if(ChosenSneaker > 1)
        {
            ChosenSneaker--;
        }
        else
        {
            ChosenSneaker = SneakerCount;
        }
        left_sneaker.sprite = spriteArray_left[ChosenSneaker-1];
        right_sneaker.sprite = spriteArray_right[ChosenSneaker-1];
        Debug.Log(ChosenSneaker);
    }
}
