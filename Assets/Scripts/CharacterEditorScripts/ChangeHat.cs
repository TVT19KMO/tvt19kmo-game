using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    public GameObject cap;
    public SpriteRenderer caprenderer;
    public Sprite[] caps;
    public int ChosenCap;
    // Start is called before the first frame update
    void Start()
    {
        ChosenCap = PlayerPrefs.GetInt("Cap", 0);
        if(ChosenCap != 0)
        {
            caprenderer.sprite = caps[ChosenCap];
        }
        Data.Caps = caps;
    }
    public void nextCap()
    {
        int capCount = caps.Length;
        if(ChosenCap < capCount-1)
        {
            ChosenCap++;
        }
        else
        {
            ChosenCap = 0;
        }
        caprenderer.sprite = caps[ChosenCap];
    }
    public void prevCap()
    {
        int capCount = caps.Length;
        if(ChosenCap > 0)
        {
            ChosenCap--;
        }
        else
        {
            ChosenCap = capCount-1;
        }
        caprenderer.sprite = caps[ChosenCap];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
