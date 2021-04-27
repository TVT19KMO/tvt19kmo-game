using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebrequestPost : MonoBehaviour
{
    public InputField connectCode;
    public Text InfoText;
    string code;
    string device;
    
    void Start()
    {
        device = SystemInfo.deviceUniqueIdentifier.ToString();
        Debug.Log(device);
    }

    public void PostCode()
    {
        TestJSON();
        if(string.IsNullOrWhiteSpace(connectCode.text))
        {
            Debug.Log("kenttä tyhjä!");
        }
        else
        {
            //SendJson("http://game-management-api.herokuapp.com/api/children/link", "{'code':'75912349', 'device':'asdasd'}");
            //Debug.Log("tried to post: " + code + " " + device);
        }
    }


    private void SendJson(string url, string json)
    {
        Debug.Log("Trying to post json:" + json);
    }
        
    private void TestJSON()
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://game-management-api.herokuapp.com/api/children/link");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{'code':'54200815', 'device':'asdasd'}";
            streamWriter.Write(json);
            Debug.Log("streamwriter");
        }
        Debug.Log("Streamwriter done");
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        Debug.Log("httpresponse");
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            Debug.Log("result: " + result);
        }
    }
}
