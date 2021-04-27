using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WebrequestJSON : MonoBehaviour
{
    public InputField connectCode;
    public Text InfoText;
    string code;
    string device;
    void Start()
    {
        device = SystemInfo.deviceUniqueIdentifier.ToString();
    }

    IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

       // JSONNode jsonData = JSON.Parse(Encoding.UTF8.GetString(request.downloadHandler.data));

        Debug.Log("Status Code: " + request.responseCode);
        Dictionary<string, string> asd = request.GetResponseHeaders();
        foreach(KeyValuePair<string, string> header in asd)
        {
            Debug.Log(header);
        }
    }
    public void PostJSON()
    {
        StartCoroutine(Post("http://game-management-api.herokuapp.com/api/children/link", "{'code':'55175357', 'device':'asdasd'}"));
    }
}