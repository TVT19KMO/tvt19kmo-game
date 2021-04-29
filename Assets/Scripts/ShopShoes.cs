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
    public class ItemsClass
    {
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    [System.Serializable]
    public class ShoesClass
    {        
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }
    

    [SerializeField] List<ShoesClass> ShoeItemsList;
    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] List<ItemsClass> ShoeItems;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    
    //readonly string getURL = "https://game-management-api.herokuapp.com/api/store/shoes";
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;
    public string itemType;
    int k = 0;
    //ItemsClass[] shoes = new ItemsClass[10];

    void Start()
    {        
        StartCoroutine(GetShoesRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);            
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);
            ItemsClass[] shoes = JsonHelper.FromJson<ItemsClass>(jsonString);
            int len = items.Length - 1;

            for (int i = 0; i < len; i++)
            {                
                //Debug.Log(i + " id: " + shoes[i].id + " " + shoes[i].price);
                if (items[i].type == "shoe")
                {                    
                    shoes[k].type = items[i].type;
                    shoes[k].name = items[i].name;
                    shoes[k].color = items[i].color;
                    shoes[k].price = items[i].price;
                    shoes[k].id = items[i].id;
                    Debug.Log("Shoe item: " + k + " " + shoes[k].type + " " + shoes[k].name + " " + shoes[i].color + " " + shoes[i].price + " " + shoes[i].id);
                    k += 1;
                                        
                }
                else if (items[i].type == "hat")
                {
                    //Debug.Log("This is a hat");
                    Debug.Log(items[i].type + " " + items[i].name + " " + items[i].color + " " + items[i].price + " " + items[i].id);
                }
                else if (items[i].type == "top")
                {
                    //Debug.Log("This is a top");
                    Debug.Log(items[i].type + " " + items[i].name + " " + items[i].color + " " + items[i].price + " " + items[i].id);
                }
                else if (items[i].type == "bottom")
                {
                    //Debug.Log("This is a bottom");
                    Debug.Log(items[i].type + " " + items[i].name + " " + items[i].color + " " + items[i].price + " " + items[i].id);
                }                   
            }

            for (int i = 0; i <= 8; i++)
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

        //ItemHandler();
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        //Debug.Log(itemIndex);
        ShopItemsList[itemIndex].IsPurchased = true;
        Debug.Log("Hello from outside scope: " + itemType);
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

    void ItemHandler(string result)
    {
        Debug.Log("Item handler called");
        Debug.Log("This is the result " + result);
    }
}


