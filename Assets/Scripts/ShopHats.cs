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

public class ShopHats : MonoBehaviour
{
    [System.Serializable]
    class HatImage
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
    public class Hat
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
        public bool IsPurchased = false;
    }

    [SerializeField] List<Hat> Hats = new List<Hat>();
    [SerializeField] List<HatImage> HatImageList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;

    void Start()
    {
        GetHats();
              
    }

    void GetHats()
    {
        StartCoroutine(GetHatsRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);

            int q = items.Length - 1;

            for (int i = 0; i < q; i++)
            {
                if (items[i].type == "hat")
                {
                    Hats.Add(new Hat
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }
            }

            /*foreach (var h in Hats)
            {
                Debug.Log("Item: " + h.type + " " + h.name + " " + h.color + " " + h.price + " " + h.id);
            }*/

            int x = HatImageList.Count;
            for (int i = 0; i < x; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = HatImageList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Hats[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !Hats[i].IsPurchased;
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
            string json = "{ \"item\": \"" + Hats[itemIndex].id + "\"}";
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
            CoinManager.Instance.UseCoins(Hats[itemIndex].price);
            Hats[itemIndex].IsPurchased = true;
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            Debug.Log(Hats[itemIndex].id);
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


