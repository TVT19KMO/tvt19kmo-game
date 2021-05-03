using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanels : MonoBehaviour
{
    public GameObject TopsPanel;
    public GameObject BottomsPanel;
    public GameObject HatsPanel;
    public GameObject ShoesPanel;
    
    public Button NextButton;
    public Button PreviousButton;

    public GameObject[] PanelsList;   
        
    [SerializeField] public Text CoinsText;

    void Start()
    {
        
        Button NextBtn = NextButton.GetComponent<Button>();
        NextBtn.onClick.AddListener(NextButtonClicked);
        Button PrevBtn = PreviousButton.GetComponent<Button>();
        PrevBtn.onClick.AddListener(PreviousButtonClicked);
        CoinManager.Instance.GetCoins();
        SetCoinsUI();
        MainPanel();
        
    }

    void Update()
    {
        SetCoinsUI();
    }


    public void MainPanel()
    {
        PanelsList[0].SetActive(true);
        PanelsList[1].SetActive(false);
        PanelsList[2].SetActive(false);
        PanelsList[3].SetActive(false);        
    }

    public void NextButtonClicked()
    {
        Debug.Log("You pushed next button");        
        if (PanelsList[0].activeSelf == true)
        {
            PanelsList[0].SetActive(false);
            PanelsList[1].SetActive(true);
        }
        else if (PanelsList[1].activeSelf == true)
        {
            PanelsList[1].SetActive(false);
            PanelsList[2].SetActive(true);
        }
        else if (PanelsList[2].activeSelf == true)
        {
            PanelsList[2].SetActive(false);
            PanelsList[3].SetActive(true);
        }
        else if (PanelsList[3].activeSelf == true)
        {
            PanelsList[3].SetActive(false);
            PanelsList[0].SetActive(true);
        }

    }

    public void PreviousButtonClicked()
    {
        Debug.Log("You pushed previous button");        
        if (PanelsList[0].activeSelf == true)
        {
            PanelsList[0].SetActive(false);
            PanelsList[3].SetActive(true);
        }
        else if (PanelsList[3].activeSelf == true)
        {
            PanelsList[3].SetActive(false);
            PanelsList[2].SetActive(true);
        }
        else if (PanelsList[2].activeSelf == true)
        {
            PanelsList[2].SetActive(false);
            PanelsList[1].SetActive(true);
        }
        else if (PanelsList[1].activeSelf == true)
        {
            PanelsList[1].SetActive(false);
            PanelsList[0].SetActive(true);
        }
    }

    public void SetCoinsUI()
    {
        CoinsText.text = CoinManager.Instance.Coins.ToString();
    }
}
