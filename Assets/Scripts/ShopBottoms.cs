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

public class ShopBottoms : MonoBehaviour
{
    [System.Serializable]
    class BottomImage
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
    public class Bottom
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
        public bool IsPurchased = false;
    }

    [SerializeField] List<Bottom> Bottoms = new List<Bottom>();
    [SerializeField] List<BottomImage> BottomImageList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;



    void Start()
    {
        GetBottoms();
               
    }

    void GetBottoms()
    {
        StartCoroutine(GetBottomsRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);

            int q = items.Length - 1;

            for (int i = 0; i < q; i++)
            {
                if (items[i].type == "bottom")
                {
                    Bottoms.Add(new Bottom
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }
            }

            /*foreach (var b in Bottoms)
            {
                Debug.Log("Item: " + b.type + " " + b.name + " " + b.color + " " + b.price + " " + b.id);
            }*/

            int x = BottomImageList.Count;
            for (int i = 0; i < x; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = BottomImageList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Bottoms[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();                
                buyBtn.interactable = !Bottoms[i].IsPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClicked);                
            }
            Destroy(ItemTemplate);
        }));
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
            string json = "{ \"item\": \"" + Bottoms[itemIndex].id + "\"}";
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
            CoinManager.Instance.UseCoins(Bottoms[itemIndex].price);
            Bottoms[itemIndex].IsPurchased = true;
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            Debug.Log(Bottoms[itemIndex].id);
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

    IEnumerator GetBottomsRequest(System.Action<string> result)
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

