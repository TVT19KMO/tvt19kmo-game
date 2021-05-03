using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else 
        {
            Destroy(gameObject); 
        }
    }
    
    public int Coins;

    public void UseCoins (int amount)
    {
        Coins -= amount;
    }
    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

    /*public void GetCoins()
    {
        int coinzz;
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
            //var Balance = JsonConvert.DeserializeObject<List<CoinBalance>>(msg);
            //Debug.Log(Balance[0].a);
            //Debug.Log("Laite on yhdistetty vanhemman tiliin!");
            
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log("Laitetta ei ole yhdistetty vanhemman tiliin!");
                Debug.Log(response);
            }
        }

        CoinBalance balance = new CoinBalance() { coins = 5000};
        Debug.Log("Coin amount: " + balance.coins);
        Coins = balance.coins;

        
    }*/

    public void GetCoins()
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
            var root = JsonConvert.DeserializeObject<List<Root>>(msg);
            Coins = root[0].assignee.balance;
            Debug.Log("Lapsen tilin saldo : " + root[0].assignee.balance);
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log(response);                
            }
        }
    }
    

    public class Assignee
    {
        public int balance { get; set; }
        public int code { get; set; }
        public object device { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Task
    {
        public object deleted { get; set; }
        public object creator { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string room { get; set; }
        public string difficulty { get; set; }
        public DateTime created { get; set; }
        public string id { get; set; }
    }

    public class Root
    {
        public object finished { get; set; }
        public object deleted { get; set; }
        public Assignee assignee { get; set; }
        public Task task { get; set; }
        public DateTime assigned { get; set; }
        public string id { get; set; }
    }
    public class RoomCodes
    {
        public string code { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
    public class DifficultyCodes
    {
        public int level { get; set; }
        public int reward { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

}
