using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopPanels : MonoBehaviour
{
    public GameObject TopsPanel;
    public GameObject BottomsPanel;
    
    public Button NextButton;
    public Button PreviousButton;

    
    void Start()
    {
        MainPanel();
        //Button NextBtn = NextButton.GetComponent<Button>();
        //NextBtn.onClick.AddListener(NextButtonClicked);
        //Button PrevBtn = PreviousButton.GetComponent<Button>();
        //PrevBtn.onClick.AddListener(PreviousButtonClicked);
    }


    public void MainPanel()
    {
        TopsPanel.SetActive(true);
        BottomsPanel.SetActive(false);
    }

    public void NextButtonClicked()
    {
        Debug.Log("You pushed next button");
        BottomsPanel.SetActive(true);
        TopsPanel.SetActive(false);
        
    }

    public void PreviousButtonClicked()
    {
        Debug.Log("You pushed previous button");
        BottomsPanel.SetActive(false);
        TopsPanel.SetActive(true);
    }
    public void toEditor()
    {
        SceneManager.LoadScene("CharacterEditor");
    }
}
