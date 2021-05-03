using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Net;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ShopShoes : MonoBehaviour
{
    [System.Serializable]
    class ShoeImage
    {
        public Sprite Image;        
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
    public class Shoe
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
        public bool IsPurchased = false;
    }

    [SerializeField] List<Shoe> Shoes = new List<Shoe>();
    [SerializeField] List<ShoeImage> ShoeImageList;
    

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    public int coins = 1500;
    
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;
    public string itemType;      

    void Start()
    {        
        GetShoes();        
    }
        

    void OnShopItemBtnClicked(int itemIndex)
    {
        string url = "http://game-management-api.herokuapp.com/api/store/purchase";
        string token = PlayerPrefs.GetString("ParentToken", "");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        request.ContentType = "application/json";
        request.Method = "POST";
        Debug.Log("buyItem called");
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{ \"item\": \"" + Shoes[itemIndex].id + "\"}";
            streamWriter.Write(json);
            Debug.Log("postataan " + json);
        }
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            Debug.Log(msg);
            Debug.Log("buyItem onnistui!");
            CoinManager.Instance.UseCoins(Shoes[itemIndex].price);
            Shoes[itemIndex].IsPurchased = true;
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            Debug.Log(Shoes[itemIndex].id);
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("buyItems ei onnistunut!");
                Debug.Log(response);
            }
        }

    }

    public void GetShoes()
    {
        StartCoroutine(GetShoesRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);

            int len = items.Length - 1;

            for (int i = 0; i < len; i++)
            {
                if (items[i].type == "shoe")
                {
                    Shoes.Add(new Shoe
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }
            }

            /*foreach (var s in Shoes)
            {
                Debug.Log("Item: " + s.type + " " + s.name + " " + s.color + " " + s.price + " " + s.id);
            }*/
            
            int x = ShoeImageList.Count;            

            for (int i = 0; i < x; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = ShoeImageList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Shoes[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !Shoes[i].IsPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            }

            Destroy(ItemTemplate);
        }));
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
            //Debug.Log(www.downloadHandler.text);
            if (result != null)
                result(www.downloadHandler.text);                        
        }
    }

    string fixJson(string value)
    {        
        value = "{\"Items\":" + value + "}";
        return value;
    }
   
}


