using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//[System.Serializable] - �����, ����� ���� ����� ����������� ������ � ����������, ��� ��������� ����������
//����� ��� ���������� ���������� �� ������� �������
[System.Serializable]
public class Date
{
    public int Coin;
    public int usilenie1_minigun;
    public int usilenie2_arta;
    public int usilenie3_zamarozka;
    public int usilenie4_schit;
    public int aptetschka;
    public int[] progress_lvl = new int[10];
    public bool razblokirovan_lvl2;
    public bool razblokirovan_lvl3;
    // public bool _PERVUI_ZAPUSK; //--------------�������������-------------------
}
public class Progress : MonoBehaviour
{
    [SerializeField] GameObject _yandex;
    [SerializeField] Yandex _csYandex;
    private bool _save_yandex;
    public Date date;
    [SerializeField] TextMeshProUGUI _text_monetu;

    [SerializeField] int nomer_lvl = 1;
    bool web_telefon = false;

    [SerializeField] bool sohranenyja_v_editore = false;


    //������ ���, ����� ������ �� ����������� ��� ����� �������
    //���������� ��� ��� ���������� ���������

    //����� �� �������� ��������, ����� ��� ����������� ������
    //���������� ��� ���� ����������� ������
    //�� ������ ��������� ����� ������ ���������
    //������� ��� � ������ ����������
    //� ��� ��������� ����� �������, ���� ��� ���������� ��� ��������
    //����� ���������� ��������
    public static Progress GameInstance;
    //Awake - �����, ������� ���������� ��� �������, �� ������, ��� ����� �����
    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(gameObject); //������ ������ �����������

        }
        else
        {
            Destroy(gameObject);
        }

#if UNITY_WEBGL
        if (_yandex)
        { _yandex.SetActive(true); }
#else
        if(_yandex)
        { _yandex.SetActive(false); }
#endif
#if UNITY_EDITOR
        if (_yandex)
        { _yandex.SetActive(false); }
#endif
    }

    void Start()
    {
#if UNITY_EDITOR
        if (sohranenyja_v_editore)
        {

        }
        else
        {
            PlayerPrefs.DeleteKey("PERVUI_ZAPUSK");
        }

#endif
        Debug.Log(JsonUtility.ToJson(date));


        //���� ��� ���� ��� �������, �� ��������� �� �� ������
        //���� ���, �� ����� ����� ���� �� ����������

        //����� ������� ������ ����� �� ����, ���� �� ������ ���� �� � �������� ����
        //�������������� �������� �� ������� ����� ������� ����� �������� ��� ����,
        //����� �� ���� ������� ������ �� ����� ����������, ����� �������� ������ ������ ����� ��� ����
        if (_yandex && _yandex.activeSelf)
        {
            Debug.Log("1 �������� �������� �� ������� ��������");
            _save_yandex = true;
            _csYandex.Load_Start();
        }
        else
        {
            //���� ��� ������ ������ - ������� ��������� ��������, ���� ��� - ������� ����������
            if (PERVUI_ZAPUSK())
            {
                PERVOE_SOHRANENIE();
            }
            else
            {
                Load_PlayerPrefs();
                _save_yandex = false;
            }
        }
    }




    bool igrok_avtorizirovan = true;
    public void Set_igrok_avtorizirovan(bool value)
    {
        igrok_avtorizirovan = value;
    }

    public void SaveCoin(int zarabotok)
    {
        Debug.Log("9 �������� ����������");
        date.Coin = zarabotok + date.Coin;
        if (_save_yandex && igrok_avtorizirovan)
        {
            Save_Yandex();
        }
        else
        {
            Save_PlayerPrefs();
        }
    }

    public void Save_Yandex()
    {
        Debug.Log("9-1 �������� ���������� � ������� �����");
        //������ � ������ ���������� ������ ��� �������
        //����������� ����� � ������� � ������
        string dateJS = JsonUtility.ToJson(date);
        //� �������� ��� � ����� ������
        _csYandex.Save(dateJS);
    }

        public void Save_PlayerPrefs()
    {
        Debug.Log("9-2 �������� ���������� � ���������� �����");
        PlayerPrefs.SetInt("Coin", date.Coin); //���������� ����� �� ��
        PlayerPrefs.SetInt("usilenie1_minigun", date.usilenie1_minigun);
        PlayerPrefs.SetInt("usilenie2_arta", date.usilenie2_arta);
        PlayerPrefs.SetInt("usilenie3_zamarozka", date.usilenie3_zamarozka);
        PlayerPrefs.SetInt("usilenie4_schit", date.usilenie4_schit);
        PlayerPrefs.SetInt("aptetschka", date.aptetschka);
        PlayerPrefs.SetInt("progress_lvl_0", date.progress_lvl[0]);
        PlayerPrefs.SetInt("progress_lvl_1", date.progress_lvl[1]);
        PlayerPrefs.SetInt("progress_lvl_2", date.progress_lvl[2]);

        if (date.razblokirovan_lvl2 == false)
        {
            PlayerPrefs.SetInt("razblokirovan_lvl2", 0);
        }
        else
        {
            PlayerPrefs.SetInt("razblokirovan_lvl2", 1);
        }
        if (date.razblokirovan_lvl3 == false)
        {
            PlayerPrefs.SetInt("razblokirovan_lvl3", 0);
        }
        else
        {
            PlayerPrefs.SetInt("razblokirovan_lvl3", 1);

        }
        PlayerPrefs.Save();
    }

    //��� �������� �� ������ �������� ������� � �������
    //��� �������� ������� � js
    //�� �������� ������� � ������ c#
    //� ��� �� ����� �������� ���� �����
    public void Load_Yandex(string value)
    {
        Debug.Log("6 �������� ������ �� ������� �������� ����������");
        Debug.Log(value);

        if (value.Length<5) //������ ���������� �� null, � {}, ������� ������� �������� �� ����� ������
        {
            Debug.Log("7-1 �������� ������ ���������� ���. �������� ������� ����������");
            // ���� ��� ���������� ������
            // ����� ������������� ������� ������ ����������, ����� ������� ������ �� �������
            PERVOE_SOHRANENIE();
        }
        else
        {
            date = JsonUtility.FromJson<Date>(value); Debug.Log("7-2 �������� ���������� ����. �������� ����������. �����");
            Debug.Log("����� ��������");
            Debug.Log(JsonUtility.ToJson(date));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//���������� ������ ��� ������� ������������ ��������
        }
        

    }

    public void Load_PlayerPrefs()
    {
        date.Coin = PlayerPrefs.GetInt("Coin"); //�������� ����� � ��
        date.usilenie1_minigun = PlayerPrefs.GetInt("usilenie1_minigun");
        date.usilenie2_arta = PlayerPrefs.GetInt("usilenie2_arta");
        date.usilenie3_zamarozka = PlayerPrefs.GetInt("usilenie3_zamarozka");
        date.usilenie4_schit = PlayerPrefs.GetInt("usilenie4_schit");
        date.aptetschka = PlayerPrefs.GetInt("aptetschka");
        date.progress_lvl[0] = PlayerPrefs.GetInt("progress_lvl_0");
        date.progress_lvl[1] = PlayerPrefs.GetInt("progress_lvl_1");
        date.progress_lvl[2] = PlayerPrefs.GetInt("progress_lvl_2");
        if (PlayerPrefs.GetInt("razblokirovan_lvl2") == 0)
        {
            date.razblokirovan_lvl2 = false;
        }
        else date.razblokirovan_lvl2 = true;

        if (PlayerPrefs.GetInt("razblokirovan_lvl3") == 0)
        {
            date.razblokirovan_lvl3 = false;
        }
        else date.razblokirovan_lvl3 = true;
    }


    private bool PERVUI_ZAPUSK()
    {
        return !PlayerPrefs.HasKey("PERVUI_ZAPUSK");
        //��� ���� ��� ������ ������� �� ���������� ������. ���������
        //� ���� �� ��� �������� � ��, � ��� �� ���� � ��������.
        //������� � ��������� if ��� ������ ������� ��� �� ��������� �����
        //� �� �������� ������, ������� � �� � ����������
    }

    private void PERVOE_SOHRANENIE()
    {
        Debug.Log("8 �������� �������� ����� ��� ������� ����������");
        PlayerPrefs.SetInt("PERVUI_ZAPUSK", 0);
        Save_PlayerPrefs();
        SaveCoin(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//���������� ������ ��� ������� ������������ ��������
    }






    //--------------------------��������--------------------------------------
    //������������� ��������
    public void Usilenie(int nomer)
    {
        if (nomer == 1)
        {
            date.usilenie1_minigun--;
        }
        else if (nomer == 2)
        {
            date.usilenie2_arta--;
        }
        else if (nomer == 3)
        {
            date.usilenie3_zamarozka--;
        }
        else if (nomer == 4)
        {
            date.usilenie4_schit--;
        }
        else if (nomer == 5)
        {
            date.aptetschka--;
        }

        SaveCoin(0);
    }
    public void aptetschka()
    {
        date.aptetschka--;
        SaveCoin(0);
    }

    //������������� ������
    public bool Razblokirovka_urovnja(int nomer_cartu, int nomer_missii, int zena_razblokirovki)
    {
        //���� ����� �������, �� ����������� �������
        if ((date.Coin - zena_razblokirovki) >= 0)
        {
            date.Coin = date.Coin - zena_razblokirovki;
            date.progress_lvl[nomer_cartu - 1]++;
            SaveCoin(0);
            return true;
        }
        return false;
    }


   

    public void set_nomer_lvl(int a)
    {
        nomer_lvl = a;
    }
    public int get_nomer_lvl()
    {
        return nomer_lvl;
    }


    public bool get_web_telefon()
    {
        return web_telefon;
    }

}
