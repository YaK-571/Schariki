using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[System.Serializable] - нужен, чтобы этот класс отображался справа в инспекторе, как публичная переменная
//класс для сохранения переменных на сервере яндекса
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
   // public bool _PERVUI_ZAPUSK; //--------------ПЕРЕПРОВЕРИТЬ-------------------
}
public class Progress : MonoBehaviour
{
    [SerializeField] GameObject _yandex;
    [SerializeField] Yandex _csYandex;
    private bool _save_yandex;
    public Date date;
    [SerializeField] TextMeshProUGUI _text_monetu;

    [SerializeField] int nomer_lvl = 1;

    //---------------------------НЕ ЗАБЫТЬ УБРАТЬ ПОСЛЕ БИЛДА----------------------
    [SerializeField] bool _DEV_BUILD=true; //СНЯЯЯЯЯЯЯТЬ ЭТУ ГАЛОЧКУ В ИНСПЕКТОРЕ!!!!!


    //делаем так, чтобы обьект не уничтожался при смене уровней
    //используем его для сохранения прогресса

    //чтобы не возникли ситуации, когда при перезапуске уровня
    //появляется ещё один неудаляемый обьект
    //Мы скажем неудалять самый первый созданный
    //запишем его в особую переменную
    //а все остальные будем удалять, если эта переменная уже записана
    //такое называется синглтон
    public static Progress GameInstance;
    //Awake - эвент, который вызывается при запуске, но раньше, чем эвент Старт
    private void Awake()
    {
        if (GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(gameObject); //сделай обьект неудаляемым

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (_DEV_BUILD)
        { 
        if(PERVUI_ZAPUSK())
            {
                PlayerPrefs.DeleteKey("PERVUI_ZAPUSK");
            }
        }
        else
        {

            //если это билд для яндекса, то сохраняем всё на сервер
            //если нет, то через плеер преф на устройстве

            //также обьекта яндекс может не быть, если мы начали игру не с главного меню
            //дополнительная проверка на наличие этого обьекта здесь включена для того,
            //чтобы не было бесящих ошибок во время разработки, когда включаем разные уровни сразу без меню
            if (_yandex && _yandex.activeSelf)
            {
                _save_yandex = true;
                _csYandex.Load();
            }
            else
            {//если это первый запуск - сохрани дефолтные значения, если нет - загрузи сохранение
                if (PERVUI_ZAPUSK())
                {
                    PERVOE_SOHRANENIE();
                }
                else
                {
                    LoadSave();
                }
            }
           
        }

    }


    public void LoadSave()
    {
        date.Coin = PlayerPrefs.GetInt("Coin"); //загрузка денег с пк
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

    public void SaveCoin(int zarabotok)
    {

        date.Coin = zarabotok + date.Coin;
        if (_save_yandex)
        {
            //данные в яндекс передаются только как строчка
            //преобразуем класс с данными в строку
            string dateJS = JsonUtility.ToJson(date);
            //и вызываем код в джава скрипт
            _csYandex.Save(dateJS);
        }
        else
        {
            PlayerPrefs.SetInt("Coin", date.Coin); //сохранение денег на пк
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
            PlayerPrefs.Save();
        }
    }

    //для загрузки на старте вызываем функцию в яндексе
    //она вызывает функцию в js
    //он вызывает функцию в яндекс c#
    //и уже он потом вызывает этот метод
    public void LoadCoin(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            // если нет сохранённых данных
            // Можно дополнительно вызвать первое сохранение, чтобы создать запись на сервере
            PERVOE_SOHRANENIE();
        }
        else
        {
            date = JsonUtility.FromJson<Date>(value);
            /*  if (_text_save_load)
              {
                  _text_save_load.text = date.Coin + "\n" +
              date.usilenie1_minigun + "\n" +
              date.usilenie2_arta + "\n" +
              date.usilenie3_zamarozka + "\n" +
              date.usilenie4_schit;
                  //заполнение всех полей
              }*/
        }

    }

    //использование усиления
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

    //разблокировка уровня
    public bool Razblokirovka_urovnja(int nomer_cartu, int nomer_missii, int zena_razblokirovki)
    {
        //если денег хватает, то разблокируй уровень
        if ((date.Coin - zena_razblokirovki) >= 0)
        {
            date.Coin = date.Coin - zena_razblokirovki;
            date.progress_lvl[nomer_cartu - 1]++;
            SaveCoin(0);
            return true;
        }
        return false;
    }


    private bool PERVUI_ZAPUSK()
    {
        return !PlayerPrefs.HasKey("PERVUI_ZAPUSK");
        //Это поле при первом запуске не существует вообще. Впринципе
        //и речь не про значение в нём, а про всё поле в принципе.
        //поэтому в стартовом if при первом запуске его не получится найти
        //и мы сохраним данные, которые я вёл в инспекторе
    }

    private void PERVOE_SOHRANENIE()
    {
        PlayerPrefs.SetInt("PERVUI_ZAPUSK", 0);
        SaveCoin(0);
    }

   

    public void set_nomer_lvl(int a)
    {
        nomer_lvl = a;
    }
    public int get_nomer_lvl()
    {
        return nomer_lvl;
    }
}
