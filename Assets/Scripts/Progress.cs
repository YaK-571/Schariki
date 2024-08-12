using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public int[] progress_lvl;
    public bool progress_lvl2;
    public bool progress_lvl3;
}
public class Progress : MonoBehaviour
{
    [SerializeField] GameObject _yandex;
    [SerializeField] Yandex _csYandex;
    private bool _save_yandex;
    public Date date;
    [SerializeField] TextMeshProUGUI _text_monetu;

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

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //���� ��� ���� ��� �������, �� ��������� �� �� ������
        //���� ���, �� ����� ����� ���� �� ����������
        if (_yandex.activeSelf)
        {
            _save_yandex = true;
            _csYandex.Load();
        }
        else
        {
            
            //date.Coin = PlayerPrefs.GetInt("Coin"); //�������� ����� � ��
            date.usilenie1_minigun = PlayerPrefs.GetInt("usilenie1_minigun");
            date.usilenie2_arta = PlayerPrefs.GetInt("usilenie2_arta");
            date.usilenie3_zamarozka = PlayerPrefs.GetInt("usilenie3_zamarozka");
            date.usilenie4_schit = PlayerPrefs.GetInt("usilenie4_schit");
            date.aptetschka = PlayerPrefs.GetInt("aptetschka");
          //  date.progress_lvl[0] = PlayerPrefs.GetInt("progress_lvl_0");
          //  date.progress_lvl[1] = PlayerPrefs.GetInt("progress_lvl_1");
          //  date.progress_lvl[2] = PlayerPrefs.GetInt("progress_lvl_2");
          /*
            if (PlayerPrefs.GetInt("progress_lvl2") == 0)
            {
                date.progress_lvl2 = false;
            }
            if (PlayerPrefs.GetInt("progress_lvl3") == 0)
            {
                date.progress_lvl3 = false;
            }
          */
            
            _save_yandex = false;

        }


    }

    public void SaveCoin(int zarabotok)
    {

        date.Coin = zarabotok + date.Coin;
        if (_save_yandex)
        {
            //������ � ������ ���������� ������ ��� �������
            //����������� ����� � ������� � ������
            string dateJS = JsonUtility.ToJson(date);
            //� �������� ��� � ����� ������
            _csYandex.Save(dateJS);
        }
        else
        {
            PlayerPrefs.SetInt("Coin", date.Coin); //���������� ����� �� ��
            PlayerPrefs.SetInt("usilenie1_minigun", date.usilenie1_minigun);
            PlayerPrefs.SetInt("usilenie2_arta", date.usilenie2_arta);
            PlayerPrefs.SetInt("usilenie3_zamarozka", date.usilenie3_zamarozka);
            PlayerPrefs.SetInt("usilenie4_schit", date.usilenie4_schit);
            PlayerPrefs.SetInt("aptetschka", date.aptetschka);
            PlayerPrefs.SetInt("progress_lvl_0", date.progress_lvl[0]);
            PlayerPrefs.SetInt("progress_lvl_1", date.progress_lvl[1]);
            PlayerPrefs.SetInt("progress_lvl_2", date.progress_lvl[2]);

            if (date.progress_lvl2 == false)
            {
                PlayerPrefs.SetInt("progress_lvl2", 0);
            }
            else
            {
                PlayerPrefs.SetInt("progress_lvl2", 1);
            }
            if (date.progress_lvl3 == false)
            {
                PlayerPrefs.SetInt("progress_lvl3", 0);
            }
            else
            {
                PlayerPrefs.SetInt("progress_lvl3", 1);

            }
        }
    }

        //��� �������� �� ������ �������� ������� � �������
        //��� �������� ������� � js
        //�� �������� ������� � ������ c#
        //� ��� �� ����� �������� ���� �����
        public void LoadCoin(string value)
        {

            date = JsonUtility.FromJson<Date>(value);
            /*  if (_text_save_load)
              {
                  _text_save_load.text = date.Coin + "\n" +
              date.usilenie1_minigun + "\n" +
              date.usilenie2_arta + "\n" +
              date.usilenie3_zamarozka + "\n" +
              date.usilenie4_schit;
                  //���������� ���� �����
              }*/


        }

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
    }
