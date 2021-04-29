using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ShopBottoms : MonoBehaviour
{
    [System.Serializable]
    class BottomImage
    {
        public Sprite Image;
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

    [System.Serializable]
    public class Bottom
    {
        public Sprite Image;
        public string type;
        public string name;
        public string color;
        public int price;
        public string id;
        public bool IsPurchased = false;
    }

    [SerializeField] List<Bottom> Bottoms = new List<Bottom>();
    [SerializeField] List<BottomImage> BottomImageList;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;
    readonly string getURL = "https://game-management-api.herokuapp.com/api/store/items";
    public string dataString;



    void Start()
    {
        GetBottoms();
               
    }

    void GetBottoms()
    {
        StartCoroutine(GetBottomsRequest(result => {
            ItemTemplate = ShopScrollView.GetChild(0).gameObject;
            dataString = result;

            string jsonString = fixJson(dataString);
            ItemsClass[] items = JsonHelper.FromJson<ItemsClass>(jsonString);

            int q = items.Length - 1;

            for (int i = 0; i < q; i++)
            {
                if (items[i].type == "bottom")
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

            foreach (var b in Bottoms)
            {
                //Debug.Log("Item: " + b.type + " " + b.name + " " + b.color + " " + b.price + " " + b.id);
            }

            int x = BottomImageList.Count;
            for (int i = 0; i < x; i++)
            {
                g = Instantiate(ItemTemplate, ShopScrollView);
                g.transform.GetChild(0).GetComponent<Image>().sprite = BottomImageList[i].Image;
                g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Bottoms[i].price.ToString();
                buyBtn = g.transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !Bottoms[i].IsPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClicked);
            }
            Destroy(ItemTemplate);
        }));
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (CoinManager.Instance.HasEnoughCoins(Bottoms[itemIndex].price))
        {
            CoinManager.Instance.UseCoins(Bottoms[itemIndex].price);
            Bottoms[itemIndex].IsPurchased = true;
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
            Debug.Log(Bottoms[itemIndex].id);
        }
        else
        {
            Debug.Log("Ei tarpeeksi kolikoita");
        };

    }

    IEnumerator GetBottomsRequest(System.Action<string> result)
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