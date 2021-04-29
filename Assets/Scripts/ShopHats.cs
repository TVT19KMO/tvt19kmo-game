using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShopHats : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [System.Serializable]
    public class HatsClass
    {
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }
    [SerializeField] List<HatsClass> HatItemsList;

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;

    void Start()
    {
        //ItemTemplate = ShopScrollView.GetChild(0).gameObject;

        /*int len = ShopItemsList.Count;
        for (int i = 0; i <= 3; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();            
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].IsPurchased;
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }*/

        StartCoroutine(GetHatsRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            HatsClass[] hats = JsonHelper.FromJson<HatsClass>(jsonString);

            /*for (int i = 0; i<9; i++)
            {
                Debug.Log(hats[i].name + " " + hats[i].color);                
            }*/

            for (int i = 0; i <= 3; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                //g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = hats[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !ShopItemsList[i].IsPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClicked);
            }
            Destroy(ItemTemplate);
        }));
        //Debug.Log("dataString: " + dataString);        
        //Destroy(ItemTemplate);
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        //Debug.Log(itemIndex);
        ShopItemsList[itemIndex].IsPurchased = true;

        //Disable the buy button
        buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        buyBtn.interactable = false;
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";

    }

    IEnumerator GetHatsRequest(System.Action<string> result)
    {

        UnityWebRequest www = UnityWebRequest.Get(getURL);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            if (result != null)
                result(www.error);
        }
        else
        {
            //dataString = www.downloadHandler.text;
            //Debug.Log("From IEnumerator: " + dataString);
            Debug.Log(www.downloadHandler.text);
            if (result != null)
                result(www.downloadHandler.text);

            //string jsonString = fixJson(dataString);            
            /*BottomsClass[] bottoms = JsonHelper.FromJson<BottomsClass>(jsonString);
            
            for (int i = 0; i<5; i++)
            {
                Debug.Log(bottoms[i].name + " " + bottoms[i].color);                
            }*/
            //Debug.Log(top[0].color);
            //Debug.Log(top[1].color);            
        }

    }



    string fixJson(string value)
    {
        //Debug.Log(value);
        value = "{\"Items\":" + value + "}";
        //Debug.Log(value);
        return value;
    }
}


