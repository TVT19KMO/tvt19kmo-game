using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShopConnection : MonoBehaviour
{
    public static ShopConnection Instance;
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

    [System.Serializable]
    public class ItemsClass
    {
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    public class Shoe
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    public class Hat
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    public class Top
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    public class Bottom
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
    }

    List<Shoe> Shoes = new List<Shoe>();
    List<Hat> Hats = new List<Hat>();
    List<Top> Tops = new List<Top>();
    List<Bottom> Bottoms = new List<Bottom>();

    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;

    void Start()
    {
        Debug.Log("GetItems called");
        GetItems();        
    }

    public void GetItems()
    {    
        StartCoroutine(GetItemsRequest(result => {
            
            dataString = result;
            string jsonString = fixJson(dataString);
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);           

            int q = items.Length - 1;

            for (int i=0; i<q; i++)
            {
                if (items[i].type == "shoe")
                {                    
                    Shoes.Add(new Shoe
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }

                else if (items[i].type == "hat")
                {
                    Hats.Add(new Hat
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }

                else if (items[i].type == "top")
                {
                    Tops.Add(new Top
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }

                else if (items[i].type == "bottom")
                {
                    Bottoms.Add(new Bottom
                    {
                        type = items[i].type,
                        name = items[i].name,
                        color = items[i].color,
                        price = items[i].price,
                        id = items[i].id
                    });
                }
            }            

            foreach (var s in Shoes)
            {
                Debug.Log("Item: " + s.type + " " + s.name + " " + s.color + " " + s.price + " " + s.id);
            }

            foreach (var h in Hats)
            {
                Debug.Log("Item: " + h.type + " " + h.name + " " + h.color + " " + h.price + " " + h.id);
            }

            foreach (var t in Tops)
            {
                Debug.Log("Item: " + t.type + " " + t.name + " " + t.color + " " + t.price + " " + t.id);
            }

            foreach (var b in Bottoms)
            {
                Debug.Log("Item: " + b.type + " " + b.name + " " + b.color + " " + b.price + " " + b.id);
            }

        }));

    }    

    IEnumerator GetItemsRequest(System.Action<string> result)
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            if (result != null)
                result(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            if (result != null)
                result(www.downloadHandler.text);
        }
    }

    string fixJson(string value)
    {        
        value = "{\"Items\":" + value + "}";        
        return value;
    }
}

/*public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}*/