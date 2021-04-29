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
    List<string> ownedItems = new List<string>();
    void Start()
    {
        // alustetaan kaikki spritet data.cs scriptiin
        Data.LeftArms = larms.ToArray();
        Data.RightArms = rarms.ToArray();
        Data.Tops = tops.ToArray();
        Data.LeftSneakers = leftSneakers.ToArray();
        Data.RightSneakers = rightSneakers.ToArray();
        Data.Hats = hats.ToArray();
        Data.LeftLegs = leftLegs.ToArray();
        Data.RightLegs = rightLegs.ToArray();
        Data.Bottoms = bottoms.ToArray();
        // Asetetaan oletusspritet omistettujen vaatteiden nollakohdaksi
        Ownedlarms.Add(larms[0]);
        Ownedrarms.Add(rarms[0]);
        Ownedtops.Add(tops[0]);
        OwnedleftSneakers.Add(leftSneakers[0]);
        OwnedrightSneakers.Add(rightSneakers[0]);
        Ownedhats.Add(hats[0]);
        OwnedleftLegs.Add(leftLegs[0]);
        OwnedrightLegs.Add(rightLegs[0]);
        Ownedbottoms.Add(bottoms[0]);
        ParentConnection();
        //esimerkki itemin ostamiseen id:n avulla, pitää olla yhdistettynä vanhempaan + kolikoita tilillä
        //buyItem("6088379fbfcad20c0e8cec8a");
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
    void buyItem(string itemID)
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
                string json = "{ \"item\": \"" + itemID + "\"}";
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
                // Tässä voi järjestellä itemeitä esim. nimen tai tyypin mukaan, esim. s.name tai s.type
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
                // Hats
                case "6088379fbfcad20c0e8cec6b":
                {
                    Ownedhats.Add(hats[6]);
                    Debug.Log("blue cap");
                    break;
                }
                case "6088379fbfcad20c0e8cec6c":
                {
                    Ownedhats.Add(hats[8]);
                    Debug.Log("red cap");
                    break;
                }
                case "6088379fbfcad20c0e8cec6d":
                {
                    Ownedhats.Add(hats[5]);
                    Debug.Log("yellow cap");
                    break;
                }
                case "6088379fbfcad20c0e8cec6e":
                {
                    Ownedhats.Add(hats[7]);
                    Debug.Log("black cap");
                    break;
                }
                case "6088379fbfcad20c0e8cec6f":
                {
                    Ownedhats.Add(hats[1]);
                    Debug.Log("blue beanie");
                    break;
                }
                case "6088379fbfcad20c0e8cec70":
                {
                    Ownedhats.Add(hats[2]);
                    Debug.Log("red beanie");
                    break;
                }
                case "6088379fbfcad20c0e8cec71":
                {
                    Ownedhats.Add(hats[4]);
                    Debug.Log("yellow beanie");
                    break;
                }
                case "6088379fbfcad20c0e8cec72":
                {
                    Ownedhats.Add(hats[3]);
                    Debug.Log("black beanie");
                    break;
                }
                case "6088379fbfcad20c0e8cec73":
                {
                    Ownedhats.Add(hats[10]);
                    Debug.Log("blue bandana");
                    break;
                }
                case "6088379fbfcad20c0e8cec74":
                {
                    Ownedhats.Add(hats[12]);
                    Debug.Log("red bandana");
                    break;
                }
                case "6088379fbfcad20c0e8cec75":
                {
                    Ownedhats.Add(hats[9]);
                    Debug.Log("yellow bandana");
                    break;
                }
                case "6088379fbfcad20c0e8cec76":
                {
                    Ownedhats.Add(hats[11]);
                    Debug.Log("black bandana");
                    break;
                }
                //shoes
                case "6088379fbfcad20c0e8cec7e":
                {
                    OwnedleftSneakers.Add(leftSneakers[4]);
                    OwnedrightSneakers.Add(rightSneakers[4]);
                    Debug.Log("blue sneakers");
                    break;
                }
                case "6088379fbfcad20c0e8cec7f":
                {
                    OwnedleftSneakers.Add(leftSneakers[3]);
                    OwnedrightSneakers.Add(rightSneakers[3]);
                    Debug.Log("red sneakers");
                    break;
                }
                case "6088379fbfcad20c0e8cec80":
                {
                    OwnedleftSneakers.Add(leftSneakers[2]);
                    OwnedrightSneakers.Add(rightSneakers[2]);
                    Debug.Log("yellow sneakers");
                    break;
                }
                case "6088379fbfcad20c0e8cec81":
                {
                    OwnedleftSneakers.Add(leftSneakers[1]);
                    OwnedrightSneakers.Add(rightSneakers[1]);
                    Debug.Log("black sneakers");
                    break;
                }
                // Tops
                case "6088379fbfcad20c0e8cec93":
                {
                    Ownedtops.Add(tops[2]);
                    Ownedlarms.Add(larms[2]);
                    Ownedrarms.Add(rarms[2]);
                    Debug.Log("blue hoodie");
                    break;
                }
                case "6088379fbfcad20c0e8cec94":
                {
                    Ownedtops.Add(tops[3]);
                    Ownedlarms.Add(larms[3]);
                    Ownedrarms.Add(rarms[3]);
                    Debug.Log("red hoodie");
                    break;
                }
                case "6088379fbfcad20c0e8cec95":
                {
                    Ownedtops.Add(tops[4]);
                    Ownedlarms.Add(larms[4]);
                    Ownedrarms.Add(rarms[4]);
                    Debug.Log("yellow hoodie");
                    break;
                }
                case "6088379fbfcad20c0e8cec96":
                {
                    Ownedtops.Add(tops[1]);
                    Ownedlarms.Add(larms[1]);
                    Ownedrarms.Add(rarms[1]);
                    Debug.Log("black hoodie");
                    break;
                }
                case "6088379fbfcad20c0e8cec97":
                {
                    Ownedtops.Add(tops[7]);
                    Ownedlarms.Add(larms[7]);
                    Ownedrarms.Add(rarms[7]);
                    Debug.Log("blue jacket");
                    break;
                }
                case "6088379fbfcad20c0e8cec98":
                {
                    Ownedtops.Add(tops[8]);
                    Ownedlarms.Add(larms[8]);
                    Ownedrarms.Add(rarms[8]);
                    Debug.Log("red jacket");
                    break;
                }
                case "6088379fbfcad20c0e8cec99":
                {
                    Ownedtops.Add(tops[5]);
                    Ownedlarms.Add(larms[5]);
                    Ownedrarms.Add(rarms[5]);
                    Debug.Log("yellow jacket");
                    break;
                }
                case "6088379fbfcad20c0e8cec9a":
                {
                    Ownedtops.Add(tops[6]);
                    Ownedlarms.Add(larms[6]);
                    Ownedrarms.Add(rarms[6]);
                    Debug.Log("black jacket");
                    break;
                }
                // Bottoms
                case "6088379fbfcad20c0e8cec83":
                {
                    Ownedbottoms.Add(bottoms[7]);
                    OwnedleftLegs.Add(leftLegs[7]);
                    OwnedrightLegs.Add(rightLegs[7]);
                    Debug.Log("blue pants");
                    break;
                }
                case "6088379fbfcad20c0e8cec84":
                {
                    Ownedbottoms.Add(bottoms[8]);
                    OwnedleftLegs.Add(leftLegs[8]);
                    OwnedrightLegs.Add(rightLegs[8]);
                    Debug.Log("red pants");
                    break;
                }
                case "6088379fbfcad20c0e8cec85":
                {
                    Ownedbottoms.Add(bottoms[5]);
                    OwnedleftLegs.Add(leftLegs[5]);
                    OwnedrightLegs.Add(rightLegs[5]);
                    Debug.Log("yellow pants");
                    break;
                }
                case "6088379fbfcad20c0e8cec86":
                {
                    Ownedbottoms.Add(bottoms[6]);
                    OwnedleftLegs.Add(leftLegs[6]);
                    OwnedrightLegs.Add(rightLegs[6]);
                    Debug.Log("black pants");
                    break;
                }
                case "6088379fbfcad20c0e8cec87":
                {
                    Ownedbottoms.Add(bottoms[4]);
                    OwnedleftLegs.Add(leftLegs[4]);
                    OwnedrightLegs.Add(rightLegs[4]);
                    Debug.Log("blue jeans");
                    break;
                }
                case "6088379fbfcad20c0e8cec88":
                {
                    Ownedbottoms.Add(bottoms[2]);
                    OwnedleftLegs.Add(leftLegs[2]);
                    OwnedrightLegs.Add(rightLegs[2]);
                    Debug.Log("red jeans");
                    break;
                }
                case "6088379fbfcad20c0e8cec89":
                {
                    Ownedbottoms.Add(bottoms[1]);
                    OwnedleftLegs.Add(leftLegs[1]);
                    OwnedrightLegs.Add(rightLegs[1]);
                    Debug.Log("yellow jeans");
                    break;
                }
                case "6088379fbfcad20c0e8cec8a":
                {
                    Ownedbottoms.Add(bottoms[3]);
                    OwnedleftLegs.Add(leftLegs[3]);
                    OwnedrightLegs.Add(rightLegs[3]);
                    Debug.Log("black jeans");
                    break;
                }
                default:
                    Debug.Log("error");
                    break;
            }
        }
        Data.OwnedHats = Ownedhats.ToArray();
        Data.OwnedTops = Ownedtops.ToArray();
        Data.OwnedLeftArms = Ownedlarms.ToArray();
        Data.OwnedRightArms = Ownedrarms.ToArray();
        Data.OwnedBottoms = Ownedbottoms.ToArray();
        Data.OwnedLeftLegs = OwnedleftLegs.ToArray();
        Data.OwnedRightLegs = OwnedrightLegs.ToArray();
        Data.OwnedLeftSneakers = OwnedleftSneakers.ToArray();
        Data.OwnedRightSneakers = OwnedrightSneakers.ToArray();
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

