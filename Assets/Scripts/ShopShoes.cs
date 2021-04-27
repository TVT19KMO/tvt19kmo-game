using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShopShoes : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [System.Serializable]
    public class ShoesClass
    {
        public int _id;
        public string name;
        public string color;
        public int price;
        public int __v;
    }
    [SerializeField] List<ShoesClass> ShoeItemsList;

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/shoes";
    public string dataString;

    void Start()
    {
        StartCoroutine(GetShoesRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            ShoesClass[] shoes = JsonHelper.FromJson<ShoesClass>(jsonString);

            for (int i = 0; i<9; i++)
            {
                Debug.Log(shoes[i].name + " " + shoes[i].color);                
            }

            for (int i = 0; i <= 3; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);                
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shoes[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !ShopItemsList[i].IsPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClicked);
            }
            Destroy(ItemTemplate);
        }));        
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

    IEnumerator GetShoesRequest(System.Action<string> result)
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
            Debug.Log(www.downloadHandler.text);
            if (result != null)
                result(www.downloadHandler.text);                        
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
