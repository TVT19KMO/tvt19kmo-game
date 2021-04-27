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
    void Start()
    {
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
}
