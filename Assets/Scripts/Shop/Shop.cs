using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Shop : MonoBehaviour
{
    public List<Sprite> larms = new List<Sprite>();
    public List<Sprite> rarms = new List<Sprite>();
    public List<Sprite> tops = new List<Sprite>();
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [System.Serializable]
    public class TopsClass
    {
        public int _id;
        public string name;
        public string color;
        public int price;
        public int __v;
    }
    [SerializeField] List<TopsClass> TopItemsList;

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField ]Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/tops";
    public string dataString;
    
    

    void Start ()
    {
        Data.LeftArms = larms.ToArray();
        Data.RightArms = rarms.ToArray();
        Data.Tops = tops.ToArray();
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

        StartCoroutine(GetTopsRequest( result =>{
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;            
            
            string jsonString = fixJson(dataString);
            TopsClass[] tops = JsonHelper.FromJson<TopsClass>(jsonString);
            
            /*for (int i = 0; i<9; i++)
            {
                Debug.Log(tops[i].name + " " + tops[i].color);                
            }*/

            for (int i = 0; i <= 9; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tops[i].price.ToString();
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

    IEnumerator GetTopsRequest( System.Action<string> result)
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
            /*TopsClass[] tops = JsonHelper.FromJson<TopsClass>(jsonString);
            
            for (int i = 0; i<5; i++)
            {
                Debug.Log(tops[i].name + " " + tops[i].color);                
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

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}