using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
  
    public static Yandex YandexInstance;
    //Awake - эвент, который вызывается при запуске, но раньше, чем эвент Старт
    private void Awake()
    {
        if (YandexInstance == null)
        {
            YandexInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] TextMeshProUGUI _text_nik;
    [SerializeField] RawImage _image;

    [DllImport("__Internal")]
    private static extern void PlayerData();
    //Экстерн - внешний. Запрос функции из другого джаваскипт файла
    
    [DllImport("__Internal")]
    private static extern void OzenkaJS(); //предложение поставить оценку

    [DllImport("__Internal")]
    private static extern void SaveJS(string date);//для сохранения данных
    [DllImport("__Internal")]
    private static extern void LoadJS();//для загрузки данных

    public void TestHelloWorld()
    {
        PlayerData();
    }

    //подгрузка имени игрока в игру из яндекса
      public void SetNik(string text)
    {
         _text_nik.text = text;
        
       
    }
    //подгрузка иконки игрока из яндекса в игру
    //из джаваскрипта происходит подгрузка
    public void Set_Image(string url)
    {
        StartCoroutine(Zagruzka_image(url));
    }

    IEnumerator Zagruzka_image(string media_url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(media_url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
           
        }
        else
        {
                           _image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            
            
           
        }
    }


    public void Ozenka()
    {
        OzenkaJS();
    }

    public void Save(string date)
    {
        SaveJS(date);
    }
    public void Load()
    {
        LoadJS();
    }

    public void LoadCoin(string value)
    {
        Progress.GameInstance.LoadCoin(value);
    }
}
