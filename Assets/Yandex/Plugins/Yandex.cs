using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;

//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Yandex : MonoBehaviour
{
    public static Yandex YandexInstance;
    //Awake - �����, ������� ���������� ��� �������, �� ������, ��� ����� �����
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

    public bool web_mobile;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //    ustroistvo = get_ustroistvo_string();
        web_mobile = get_ustroistvo_mobile();
    }

    [SerializeField] TextMeshProUGUI _text_nik;
    [SerializeField] RawImage _image;

    [DllImport("__Internal")]
    private static extern void PlayerData();
    //������� - �������. ������ ������� �� ������� ���������� �����

    [DllImport("__Internal")]
    private static extern void OzenkaJS(); //����������� ��������� ������

    [DllImport("__Internal")]
    private static extern void SaveJS(string date);//��� ���������� ������
    [DllImport("__Internal")]
    private static extern void LoadJS();//��� �������� ������

    //[DllImport("__Internal")]
    //private static extern string get_ustroistvo_string();//��� �������� ������
    [DllImport("__Internal")]
    private static extern bool get_ustroistvo_mobile();//��� �������� ������

    public void TestHelloWorld()
    {
        PlayerData();
    }

    //��������� ����� ������ � ���� �� �������
    public void SetNik(string text)
    {
        _text_nik.text = text;


    }
    //��������� ������ ������ �� ������� � ����
    //�� ������������ ���������� ���������
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
    public void Load_Start()
    {
        Debug.Log("2 ������ ����� ������������");
        LoadJS();
    }

    public void Load_Yandex(string value)
    {
        Debug.Log("5 ������ ������ �� ����� �������� ��������");
        Debug.Log("��������������� ��� � Yandex.cs");
        //  Progress.GameInstance.Load_Yandex(value);
    }

    bool igrok_avtorizirovan = true;
    public void Set_igrok_avtorizirovan(bool value)
    {
        Debug.Log("5 ������ ������ �� ����� �������� ��������");
        Debug.Log("��������������� ��� � Yandex.cs");
        igrok_avtorizirovan =value;
        //Progress.GameInstance.Set_igrok_avtorizirovan(igrok_avtorizirovan);
    }
    public void Save_PlayerPrefs()
    {
        Progress.GameInstance.Save_PlayerPrefs();
    }
}
