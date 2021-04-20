using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditor : MonoBehaviour
{
    public SpriteRenderer head;
    public ChangeSneakers changeSneakers;
    public CharacterEditor characterEditor;
    void Start()
    {
        changeSneakers = GameObject.Find("EditorManager").GetComponent<ChangeSneakers>();
        characterEditor = GameObject.Find("EditorManager").GetComponent<CharacterEditor>();
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("HeadColor", characterEditor.ChosenHeadColor);
        PlayerPrefs.SetInt("SneakersColor", changeSneakers.ChosenSneaker);
        Debug.Log("saved head color: " + characterEditor.ChosenHeadColor);
    }
    void Update()
    {
        
    }
}
