using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class checkTasks : MonoBehaviour
{
    List<Root> TaskList = new List<Root>();
    public GameObject taskbutton;
    void Start()
    {
        GET();
    }

    public void GET()
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
            Debug.Log(root);
            foreach(Root r in root)
            {
                TaskList.Add(r);
            }
            if(TaskList.Count > 0){
                taskbutton.SetActive(true);
            }
            else{
                taskbutton.SetActive(false);
            }
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log(response);
            }
        }
    }
    public void goTasks()
    {
        SceneManager.LoadScene("TaskScene");
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