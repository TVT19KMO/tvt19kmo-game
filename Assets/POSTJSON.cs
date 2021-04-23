using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class POSTJSON : MonoBehaviour
{
    string DeviceID;
    public InputField codefield;
    public Text inputhelptext;
    public Text InfoText;
    void Start()
    {
        DeviceID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log(DeviceID);
    }
    public void Validate()
    {
        if(String.IsNullOrWhiteSpace(codefield.text))
        {
            Debug.Log("tyhjää");
            InfoText.text = "Kirjoita koodisi alle";
            //Tähän ehkä parempi validointi vielä?
        }
        else
        {
            POST();
        }
    }
    public void POST()
    {
        string sendcode = codefield.text;
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://game-management-api.herokuapp.com/api/children/link");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                    string json = "{\"code\":\"" + sendcode + "\",\"device\":\"" + DeviceID + "\"}";

                    streamWriter.Write(json);
            }
        Debug.Log("streamwriter done");
        try
            {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Debug.Log(result);
                InfoText.text = result;
            }
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                Debug.Log(new StreamReader(e.Response.GetResponseStream()).ReadToEnd());
                Debug.Log(e.Message);
                InfoText.text = e.Message;
            }
        }
    Debug.Log("httpresponse done");
    }
    public void BackToHouse()
    {
        SceneManager.LoadScene("HouseScene");
    }
}
//{"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb2RlIjo2MjQ0NTc0OCwiaWQiOiI2MDgzMDM2ODYzNzM5MDAwMDQwM2FmN2IiLCJpYXQiOjE2MTkxOTg4MzN9.lC3mJNkjdYHYo9J4nba54REc3hx65eTlQxyQh2flYKI"}