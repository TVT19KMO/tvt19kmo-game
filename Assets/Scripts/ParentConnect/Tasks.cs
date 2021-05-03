using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Tasks : MonoBehaviour
{
    public Text title;
    public Text nametext;
    public Text notetext;
    public Text roomtext;
    public Text difficultytext;
    List<RoomCodes> ListOfRooms = new List<RoomCodes>();
    public Transform ShopScrollView;
    GameObject ItemTemplate;
    GameObject g;
    List<Root> TaskList = new List<Root>();
    List<DifficultyCodes> ListOfDifficulties = new List<DifficultyCodes>();
    void Start()
    {
        SetRooms();
        setDifficulties();
        GET();
    }

    public void GET()
    {
        string url = "http://game-management-api.herokuapp.com/api/assigned-tasks";
        string token = PlayerPrefs.GetString("ParentToken", "");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("Authorization", "bearer " + token);
        Debug.Log("get called");
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            Debug.Log(msg);
            var root = JsonConvert.DeserializeObject<List<Root>>(msg);
            foreach(Root r in root)
            {
                TaskList.Add(r);
            }
            for(int i = 0; i < TaskList.Count; i++){
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Text>().text = TaskList[i].task.name;
                g.transform.GetChild(1).GetComponent<Text>().text = "Lisähuomio: " + TaskList[i].task.note;
                Debug.Log("name: " + TaskList[i].task.name);
                Debug.Log("note: " + TaskList[i].task.note);
                string roomid = TaskList[i].task.room;
                string difficultyid = TaskList[i].task.difficulty;
                string roomname ="";
                string difficultyname = "";
                foreach(RoomCodes rc in ListOfRooms)
                {
                    if(rc.id == roomid)
                    {
                        roomname = rc.name;
                    }
                }
                Debug.Log("room: " + roomname);
                g.transform.GetChild(2).GetComponent<Text>().text = "Tehtävän sijainti: " + roomname;
                //roomtext.text = "Tehtävän sijainti: " + roomname;
                foreach(DifficultyCodes dc in ListOfDifficulties)
                {
                    if(dc.id == difficultyid)
                    {
                        difficultyname = dc.name;
                    }
                }
                Debug.Log("difficulty: "+ difficultyname);
                g.transform.GetChild(3).GetComponent<Text>().text = "Tehtävän vaikeustaso: " + difficultyname;
            }
            Destroy(ItemTemplate);
        }
        catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                string response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                Debug.Log(response);
                title.text = "Yhdistä laite vanhemman hallintasovellukseen.\nLisätietoa osoitteesta https://tvt19kmo.github.io/tvt19kmo-app/";
            }
        }
    }
    public void SetRooms()
    {
        string url = "http://game-management-api.herokuapp.com/api/task-rooms";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            var rooms = JsonConvert.DeserializeObject<List<RoomCodes>>(msg);
            foreach(RoomCodes r in rooms)
            {
                ListOfRooms.Add(r);
            }
        }
        catch(WebException e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void setDifficulties()
    {
        string url = "http://game-management-api.herokuapp.com/api/task-difficulties";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(datastream);
            string msg = streamReader.ReadToEnd();
            var difficulties = JsonConvert.DeserializeObject<List<DifficultyCodes>>(msg);
            foreach(DifficultyCodes d in difficulties)
            {
                ListOfDifficulties.Add(d);
            }
        }
        catch(WebException e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void goBack()
    {
        SceneManager.LoadScene("TitleScene");
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
