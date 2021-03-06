using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Newtonsoft.Json;

public class ControllerTitle : MonoBehaviour
{
    public static ControllerTitle instance;

    string DeviceID;
    public GameObject inputPanel;
    public InputField codefield;
    public Text inputhelptext;
    public Text InfoText;
    public GameObject saveButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GET();
        DeviceID = SystemInfo.deviceUniqueIdentifier;
        Debug.Log(DeviceID);
    }

    public void Validate()
    {
        if (String.IsNullOrWhiteSpace(codefield.text))
        {
            Debug.Log("tyhj??");
            InfoText.text = "Kirjoita koodisi alle";
            //T?h?n ehk? parempi validointi viel??
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
                string result = streamReader.ReadToEnd();
                Debug.Log(result);
                int resultLen = result.Length;
                int tokenLen = resultLen - 12;
                string token = result.Remove(0, 10).Remove(tokenLen, 2);
                Debug.Log("Tallennettu token: " + token);
                PlayerPrefs.SetString("ParentToken", token);
                InfoText.text = "Hallintasovellukseen yhdist?minen onnistui!";
            }
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                InfoText.text = "Virheellinen koodi, yrit? uudestaan!";
                Debug.Log(response);
            }
        }
        Debug.Log("httpresponse done");
    }

    public void GET()
    {
        string url = "http://game-management-api.herokuapp.com/api/assigned-tasks";
        string token = PlayerPrefs.GetString("ParentToken", "");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        Debug.Log(request.Headers.ToString());
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            Debug.Log(msg);
            InfoText.text = "Laite yhdistetty onnistuneesti vanhemman tiliin!";
            inputPanel.SetActive(false);
            saveButton.SetActive(false);
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log(response);
                InfoText.text = "Yhdist? laite vanhemman hallintasovellukseen.\nLis?tietoa osoitteesta https://tvt19kmo.github.io/tvt19kmo-app/";
            }
        }

    }

    public void LoadCharacterEditor()
    {
        SceneManager.LoadScene("CharacterEditor", LoadSceneMode.Single);
    }
}
