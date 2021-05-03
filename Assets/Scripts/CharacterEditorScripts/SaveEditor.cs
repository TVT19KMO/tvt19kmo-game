using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditor : MonoBehaviour
{
    public SpriteRenderer head;
    public CharacterEditor characterEditor;
    void Start()
    {
        characterEditor = GameObject.Find("EditorManager").GetComponent<CharacterEditor>();
        }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("HeadColor", characterEditor.ChosenHeadColor);
        PlayerPrefs.SetInt("SneakersColor", characterEditor.ChosenSneaker);
        PlayerPrefs.SetInt("Hat", characterEditor.ChosenHat);
        PlayerPrefs.SetInt("Jacket", characterEditor.ChosenJacket);
        PlayerPrefs.SetInt("Pants", characterEditor.ChosenPants);
        Debug.Log("saved head color: " + characterEditor.ChosenHeadColor);
        Debug.Log("sneaker " + characterEditor.ChosenSneaker + "jacket " + characterEditor.ChosenJacket);
    }
}
