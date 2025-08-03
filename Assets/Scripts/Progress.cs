using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int[] progress_lvl = new int[20];
    public bool razblokirovan_lvl2;
    public bool razblokirovan_lvl3;
    // public bool _PERVUI_ZAPUSK; //--------------ПЕРЕПРОВЕРИТЬ-------------------

    //просто присвоить его нельзя, т.к. классы - ссылочный тип.
    //поэтому для копирования делаем отдельный метод
    public Date Clone()
    {
        Date copy = new Date();
        copy.Coin = this.Coin;
        copy.usilenie1_minigun = this.usilenie1_minigun;
        copy.usilenie2_arta = this.usilenie2_arta;
        copy.usilenie3_zamarozka = this.usilenie3_zamarozka;
        copy.usilenie4_schit = this.usilenie4_schit;
        copy.aptetschka = this.aptetschka;
        copy.razblokirovan_lvl2 = this.razblokirovan_lvl2;
        copy.razblokirovan_lvl3 = this.razblokirovan_lvl3;

        // Копируем массив отдельно, чтобы не делить ссылку
        if (this.progress_lvl != null)
        {
            copy.progress_lvl = new int[this.progress_lvl.Length];
            Array.Copy(this.progress_lvl, copy.progress_lvl, this.progress_lvl.Length);
        }
        else
        {
            copy.progress_lvl = null;
        }

        return copy;
    }
}
public class Progress : MonoBehaviour
{
    [SerializeField] GameObject _yandex;
    [SerializeField] Yandex _csYandex;
    private bool _save_yandex;

    [SerializeField] TextMeshProUGUI _text_monetu;

    [SerializeField] int nomer_lvl = 1;
    bool web_telefon = false;

    [SerializeField] bool sohranenyja_v_editore = false;


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

    [SerializeField] public Date date;
    [SerializeField] Date date_default_dlja_sbrosa;

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


        //если это билд для яндекса, то сохраняем всё на сервер
        //если нет, то через плеер преф на устройстве

        //также обьекта яндекс может не быть, если мы начали игру не с главного меню
        //дополнительная проверка на наличие этого обьекта здесь включена для того,
        //чтобы не было бесящих ошибок во время разработки, когда включаем разные уровни сразу без меню
        if (_yandex&& _yandex.activeSelf)
        {
                Debug.Log("1 ПРОГРЕСС загрузка из яндекса запущена");
                _save_yandex = true;
                _csYandex.Load_Start();
            
        }
        else
        {
            //если это первый запуск - сохрани дефолтные значения, если нет - загрузи сохранение
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
        // Sbros_progressa();
    }




    bool igrok_avtorizirovan = true;
    public void Set_igrok_avtorizirovan(bool value)
    {
        igrok_avtorizirovan = value;
    }

    public void SaveCoin(int zarabotok)
    {
        Debug.Log("9 ПРОГРЕСС сохранение");
        date.Coin = zarabotok + date.Coin;
        if (_save_yandex)
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
        if (_yandex)
        {
            if (_yandex.activeSelf)
            {
                Debug.Log("9-1 ПРОГРЕСС сохранение в яндексе КОНЕЦ");
                //данные в яндекс передаются только как строчка
                //преобразуем класс с данными в строку
                string dateJS = JsonUtility.ToJson(date);
                //и вызываем код в джава скрипт
                _csYandex.Save(dateJS);
            }
        }
    }

    public void Save_PlayerPrefs()
    {
        Debug.Log("9-2 ПРОГРЕСС сохранение в компьютере КОНЕЦ");
        PlayerPrefs.SetInt("Coin", date.Coin); //сохранение денег на пк
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

    //для загрузки на старте вызываем функцию в яндексе
    //она вызывает функцию в js
    //он вызывает функцию в яндекс c#
    //и уже он потом вызывает этот метод
    public void Load_Yandex(string value)
    {
        Debug.Log("6 ПРОГРЕСС данные из яндекса получены прогрессом");
        Debug.Log(value);

        if (value.Length < 5) //яндекс возвращает не null, а {}, поэтому сделаем проверку на длину строки
        {
            Debug.Log("7-1 ПРОГРЕСС старых сохранений нет. Создание первого сохранения");
            // если нет сохранённых данных
            // Можно дополнительно вызвать первое сохранение, чтобы создать запись на сервере
            PERVOE_SOHRANENIE();
        }
        else
        {
            date = JsonUtility.FromJson<Date>(value); Debug.Log("7-2 ПРОГРЕСС сохранения были. Загрузка сохранения. КОНЕЦ");
            Debug.Log("После загрузки");
            Debug.Log(JsonUtility.ToJson(date));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//перезапуск уровня для простой перезагрузки значений
        }


    }

    public void Load_PlayerPrefs()
    {
        date.Coin = PlayerPrefs.GetInt("Coin"); //загрузка денег с пк
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
        //Это поле при первом запуске не существует вообще. Впринципе
        //и речь не про значение в нём, а про всё поле в принципе.
        //поэтому в стартовом if при первом запуске его не получится найти
        //и мы сохраним данные, которые я вёл в инспекторе
    }

    private void PERVOE_SOHRANENIE()
    {
        Debug.Log("8 ПРОГРЕСС создание метки для первого сохранения");
        PlayerPrefs.SetInt("PERVUI_ZAPUSK", 0);
        Save_PlayerPrefs();
        Debug.Log("СБРОС ПРОГРЕССА ПЛЕЕР ПРЕФС");
        Save_Yandex();
        Debug.Log("СБРОС ПРОГРЕССА НА ЯНДЕКСЕ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//перезапуск уровня для простой перезагрузки значений
    }






    //--------------------------УСИЛЕНИЯ--------------------------------------
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

    public void Sbros_progressa()
    {
        Debug.Log("СБРОС ПРОГРЕССА ЗАПУЩЕН");
        date = date_default_dlja_sbrosa.Clone();
        PERVOE_SOHRANENIE();
    }

}
