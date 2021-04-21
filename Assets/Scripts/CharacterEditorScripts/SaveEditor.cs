using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEditor : MonoBehaviour
{
    public SpriteRenderer head;
    public ChangeSneakers changeSneakers;
    public ChangeHat changeHat;
    public ChangeJacket changeJacket;
    public ChangePants changePants;
    public CharacterEditor characterEditor;
    void Start()
    {
        changeSneakers = GameObject.Find("EditorManager").GetComponent<ChangeSneakers>();
        characterEditor = GameObject.Find("EditorManager").GetComponent<CharacterEditor>();
        changeHat = GameObject.Find("EditorManager").GetComponent<ChangeHat>();
        changeJacket = GameObject.Find("EditorManager").GetComponent<ChangeJacket>();
        changePants = GameObject.Find("EditorManager").GetComponent<ChangePants>();
        }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("HeadColor", characterEditor.ChosenHeadColor);
        PlayerPrefs.SetInt("SneakersColor", changeSneakers.ChosenSneaker);
        PlayerPrefs.SetInt("Hat", changeHat.ChosenHat);
        PlayerPrefs.SetInt("Jacket", changeJacket.ChosenJacket);
        PlayerPrefs.SetInt("Pants", changePants.ChosenPants);
        Debug.Log("saved head color: " + characterEditor.ChosenHeadColor);
    }
}
