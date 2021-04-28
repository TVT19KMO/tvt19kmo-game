using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
public class ConnectDB : MonoBehaviour
{
    public List<Sprite> larms = new List<Sprite>();
    public List<Sprite> rarms = new List<Sprite>();
    public List<Sprite> tops = new List<Sprite>();
    public List<Sprite> leftSneakers = new List<Sprite>();
    public List<Sprite> rightSneakers = new List<Sprite>();
    public List<Sprite> hats = new List<Sprite>();
    public List<Sprite> leftLegs = new List<Sprite>();
    public List<Sprite> rightLegs = new List<Sprite>();
    public List<Sprite> bottoms = new List<Sprite>();
    List<Sprite> Ownedlarms = new List<Sprite>();
    List<Sprite> Ownedrarms = new List<Sprite>();
    List<Sprite> Ownedtops = new List<Sprite>();
    List<Sprite> OwnedleftSneakers = new List<Sprite>();
    List<Sprite> OwnedrightSneakers = new List<Sprite>();
    List<Sprite> Ownedhats = new List<Sprite>();
    List<Sprite> OwnedleftLegs = new List<Sprite>();
    List<Sprite> OwnedrightLegs = new List<Sprite>();
    List<Sprite> Ownedbottoms = new List<Sprite>();
    List<string> shopItems = new List<string>();
    List<String> ownedItems = new List<string>();
    void Start()
    {
        // alustetaan spritet data.cs scriptiin
        Data.LeftArms = larms.ToArray();
        Data.RightArms = rarms.ToArray();
        Data.Tops = tops.ToArray();
        Data.LeftSneakers = leftSneakers.ToArray();
        Data.RightSneakers = rightSneakers.ToArray();
        Data.Hats = hats.ToArray();
        Data.LeftLegs = leftLegs.ToArray();
        Data.RightLegs = rightLegs.ToArray();
        Data.Bottoms = bottoms.ToArray();
        ParentConnection();
        Debug.Log(ownedItems);
        saveOwnedItems();
    }


    // Tarkistetaan onko vanhemman hallintasovellukseen yhdistetty
    public void ParentConnection()
    {
        string url = "http://game-management-api.herokuapp.com/api/assigned-tasks";
        string token = PlayerPrefs.GetString("ParentToken", "");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        Debug.Log("get called");
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            Debug.Log(msg);
            Debug.Log("Laite on yhdistetty vanhemman tiliin!");
            getItemIDs();
            getItems();
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("Laitetta ei ole yhdistetty vanhemman tiliin!");
                Debug.Log(response);
            }
        }
    }
    void getItems()
    {
        string url = "http://game-management-api.herokuapp.com/api/store/purchases";
        string token = PlayerPrefs.GetString("ParentToken", "");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        Debug.Log("getItems called");
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            var ownitems = JsonConvert.DeserializeObject<List<ShopItem>>(msg);
            foreach(ShopItem s in ownitems){
                ownedItems.Add(s.id);
            }
            Debug.Log(msg);
            Debug.Log("getItems onnistui!");
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("getItems ei onnistunut!");
                Debug.Log(response);
            }
        }
    }
    void buyItem()
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
                string json = "{ \"item\": \"6088379fbfcad20c0e8cec6c\"}";
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
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("buyItems ei onnistunut!");
                Debug.Log(response);
            }
        }
    }
    public void getItemIDs()
    // tarpeeton metodi titleen, lisätty vain testimielessä. Shopissa voi olla käyttöä
    {
        string url = "https://game-management-api.herokuapp.com/api/store/items";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        Debug.Log("getItemIDs called");
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            
            Debug.Log(msg);
            Debug.Log("getItemIDs onnistui!");
            var shopitem = JsonConvert.DeserializeObject<List<ShopItem>>(msg);
            foreach(ShopItem s in shopitem)
            {
                shopItems.Add(s.id);
            }
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("getItemIDs ei onnistunut!");
                Debug.Log(response);
            }
        }
    }
    public void saveOwnedItems()
    {
        foreach(string id in ownedItems){
            // Typerä tapa tallentaa omistetut spritet dataan, mutta en vaan keksi parempaa nyt :D
            // Eli tässä käydään läpi kaikki omistettujen itemeiden id:t, ja manuaalisesti laitettu tietty id vastaamaan tiettyä spriteä.
            switch(id)
            {
                case "6088379fbfcad20c0e8cec6b":
                {
                    Ownedhats.Add(hats[6]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec6c":
                {
                    Ownedhats.Add(hats[8]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec6d":
                {
                    Ownedhats.Add(hats[5]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec6e":
                {
                    Ownedhats.Add(hats[7]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec6f":
                {
                    Ownedhats.Add(hats[1]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec70":
                {
                    Ownedhats.Add(hats[2]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec71":
                {
                    Ownedhats.Add(hats[8]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec72":
                {
                    Ownedhats.Add(hats[7]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec73":
                {
                    Ownedhats.Add(hats[10]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec74":
                {
                    Ownedhats.Add(hats[12]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec75":
                {
                    Ownedhats.Add(hats[9]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                case "6088379fbfcad20c0e8cec76":
                {
                    Ownedhats.Add(hats[11]);
                    Debug.Log("tallennus toimi!");
                    break;
                }
                default:
                    Debug.Log("error");
                    break;
            }
        }
        Data.OwnedHats = Ownedhats.ToArray();
    }
}
public class ShopItem
{
    public string type { get; set; }
    public string name { get; set; }
    public string color { get; set; }
    public int price { get; set; }
    public string id { get; set; }
}

