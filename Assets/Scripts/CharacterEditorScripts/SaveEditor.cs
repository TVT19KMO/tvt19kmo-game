using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditor : MonoBehaviour
{
    public SpriteRenderer head;
    public ChangeSneakers changeSneakers;
    void Start()
    {
        changeSneakers = GameObject.Find("EditorManager").GetComponent<ChangeSneakers>();
    }

    public void SaveCharacter()
    {
        Debug.Log("Add save here");
        PlayerPrefs.SetString("headColor", ColorUtility.ToHtmlStringRGBA(head.color));
        PlayerPrefs.SetInt("SneakersColor", changeSneakers.ChosenSneaker);
        Debug.Log(ColorUtility.ToHtmlStringRGBA(head.color));
    }
    void Update()
    {
        
    }
}
