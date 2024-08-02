using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_nik;
    [SerializeField] RawImage _image;

    [DllImport("__Internal")]
    private static extern void PlayerData();
    //������� - �������. ������ ������� �� ������� ���������� �����

    private void Start()
    {
      
    }
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
            Debug.Log("������ �������� �������� 123");
        }
        else
        {
            _image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("�������� �����������");
        }
    }
}
