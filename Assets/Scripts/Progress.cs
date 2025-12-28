using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


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
    public int[] Coin_record = new int[20];
    public int[] progress_lvl = new int[20];
    public bool razblokirovan_lvl2;
    public bool razblokirovan_lvl3;
    public string language;

    //настройки
    public int _prizel_contur;
    public int _prizel_mushka;
    public int tip_upravlenija=1;

    public int inversija_x = 1;
    public int inversija_y = 1;

    public float chuvstvitelnost_gyro_base = 0.5f;
    public float chuvstvitelnost_gyro_max = 2.0f;

    //исключительно для достижений
    public int ispolzovano_usilenij_minigun;
    public int ispolzovano_usilenij_art;
    public int ispolzovano_usilenij_schit;
    public int ispolzovano_usilenij_samoroska;
    public int sbito_gruza_na_nitke;
    public int sbito_scharikov_za_5_sek;
    public int pokupok;
    public int popadanij_v_zentr;


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
        copy.language = this.language;

        copy._prizel_contur = this._prizel_contur;
        copy._prizel_mushka = this._prizel_mushka;
        copy.tip_upravlenija = this.tip_upravlenija;
        copy.inversija_y = this.inversija_y;
        copy.inversija_x = this.inversija_x;
        copy.chuvstvitelnost_gyro_base = this.chuvstvitelnost_gyro_base;
        copy.chuvstvitelnost_gyro_max = this.chuvstvitelnost_gyro_max;

        copy.ispolzovano_usilenij_minigun = this.ispolzovano_usilenij_minigun;
        copy.ispolzovano_usilenij_art = this.ispolzovano_usilenij_art;
        copy.ispolzovano_usilenij_schit = this.ispolzovano_usilenij_schit;
        copy.ispolzovano_usilenij_samoroska = this.ispolzovano_usilenij_samoroska;
        copy.sbito_gruza_na_nitke = this.sbito_gruza_na_nitke;
        copy.sbito_scharikov_za_5_sek = this.sbito_scharikov_za_5_sek;
        copy.pokupok = this.pokupok;
        copy.popadanij_v_zentr = this.popadanij_v_zentr;

        // Копируем массив отдельно, чтобы не делить ссылку
        if (this.progress_lvl != null)
        {
            copy.progress_lvl = new int[this.progress_lvl.Length];
            Array.Copy(this.progress_lvl, copy.progress_lvl, this.progress_lvl.Length);
        }
        else
        {copy.progress_lvl = null;}

        // Копируем массив отдельно, чтобы не делить ссылку
        if (this.Coin_record != null)
        {
            copy.Coin_record = new int[this.Coin_record.Length];
            Array.Copy(this.Coin_record, copy.Coin_record, this.Coin_record.Length);
        }
        else
        { copy.Coin_record = null; }


        return copy;
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public int Coin;
        public int usilenie1_minigun;
        public int usilenie2_arta;
        public int usilenie3_zamarozka;
        public int usilenie4_schit;
        public int aptetschka;
        public int[] Coin_record = new int[20];
        public int[] progress_lvl = new int[20];
        public bool razblokirovan_lvl2;
        public bool razblokirovan_lvl3;
        public string language;

        public int _prizel_contur;
        public int _prizel_mushka;
        public int tip_upravlenija;
        public int inversija_y;
        public int inversija_x;
        public float chuvstvitelnost_gyro_base;
        public float chuvstvitelnost_gyro_max;

        public int ispolzovano_usilenij_minigun;
        public int ispolzovano_usilenij_art;
        public int ispolzovano_usilenij_schit;
        public int ispolzovano_usilenij_samoroska;
        public int sbito_gruza_na_nitke;
        public int sbito_scharikov_za_5_sek;
        public int pokupok;
        public int popadanij_v_zentr;

    }
}

[System.Serializable]
public class Purch
{
    public int[] purch_aptetschka = new int[3];
    public int[] purch_usilenie1_minigun = new int[3];
    public int[] purch_usilenie2_arta = new int[3];
    public int[] purch_usilenie3_zamarozka = new int[3];
    public int[] purch_usilenie4_schit = new int[3];
  //  public int[] purch_bundl = new int[3];
}


public class Progress : MonoBehaviour
{
    [SerializeField] GameObject _yandex;
    [SerializeField] Yandex _csYandex;
    private bool _save_yandex;

    //   [SerializeField] TextMeshProUGUI _text_monetu;

    [SerializeField] int nomer_lvl = 1;
    [SerializeField] int nomer_missii = 1;
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
    [SerializeField] public Purch _purch;

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
        if (date_default_dlja_sbrosa.Coin == 0)
        { date_default_dlja_sbrosa.Coin = 1; }
        //если data.coin == 0, то включается загрузка первого сохранения
        //если тут случайно поставить 0, то она была бы бесконечно


#if UNITY_EDITOR
        if (sohranenyja_v_editore)
        {

        }
        else
        {
            PERVOE_SOHRANENIE();
        }

#endif
        //  Debug.Log(JsonUtility.ToJson(date));


        //если это билд для яндекса, то сохраняем всё на сервер
        //если нет, то через плеер преф на устройстве

        //также обьекта яндекс может не быть, если мы начали игру не с главного меню
        //дополнительная проверка на наличие этого обьекта здесь включена для того,
        //чтобы не было бесящих ошибок во время разработки, когда включаем разные уровни сразу без меню



        if (_yandex && _yandex.activeSelf)
        {
            Debug.Log("1 ПРОГРЕСС загрузка из яндекса запущена");
            _save_yandex = true;
            /*_csYandex.Load_Start();*/

            Load_Yandex();
        }
        else
        {
            //если это первый запуск - сохрани дефолтные значения, если нет - загрузи сохранение
            Load_PlayerPrefs();
            _save_yandex = false;
        }

#if UNITY_WEBGL
        YG2.ConsumePurchases(); //метод консумирования
        //то есть он прогружает покупки которые были оплачены, но которые не прошли сразу по какой-то причине
        //например пользователь оплатил и закрыл игру, не вернувшись в неё
#endif
        if(CsLocalization.Local)
        CsLocalization.Local.SetLanguage(date.language);
        // Sbros_progressa();
    }





    public void Save(int zarabotok = 0)
    {
        Debug.Log("9 ПРОГРЕСС сохранение");
        date.Coin = zarabotok + date.Coin;

        YG2.saves.Coin = date.Coin;

        if (nomer_lvl > 0)
        {
            if (date.Coin_record[nomer_lvl - 1] < zarabotok)
            {
                date.Coin_record[nomer_lvl - 1] = zarabotok;
                YG2.saves.Coin_record[nomer_lvl - 1] = date.Coin_record[nomer_lvl - 1];
            }
        }


        if (_save_yandex)
        {
            YandexPluginSaveDate();
        }
        else
        {
            Save_PlayerPrefs();
        }
    }

    public int get_coin_record()
    {
        if (nomer_lvl > 0) return date.Coin_record[nomer_lvl - 1];
        else return 0;
    }

    public void Save_Yandex()
    {
        YG2.SaveProgress();
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
        //рекорд
        PlayerPrefs.SetInt("Coin_record_0", date.Coin_record[0]);
        PlayerPrefs.SetInt("Coin_record_1", date.Coin_record[1]);
        PlayerPrefs.SetInt("Coin_record_2", date.Coin_record[2]);
        PlayerPrefs.SetString("language", date.language);
        //прицел
        PlayerPrefs.SetInt("_prizel_contur", date._prizel_contur);
        PlayerPrefs.SetInt("_prizel_mushka", date._prizel_mushka);
        //настройки
        PlayerPrefs.SetInt("tip_upravlenija", date.tip_upravlenija);
        PlayerPrefs.SetInt("inversija_y", date.inversija_y);
        PlayerPrefs.SetInt("inversija_x", date.inversija_x);
        PlayerPrefs.SetFloat("chuvstvitelnost_gyro_max", date.chuvstvitelnost_gyro_max);
        PlayerPrefs.SetFloat("chuvstvitelnost_gyro_base", date.chuvstvitelnost_gyro_base);

        PlayerPrefs.SetInt("ispolzovano_usilenij_minigun", date.ispolzovano_usilenij_minigun);
        PlayerPrefs.SetInt("ispolzovano_usilenij_art", date.ispolzovano_usilenij_art);
        PlayerPrefs.SetInt("ispolzovano_usilenij_schit", date.ispolzovano_usilenij_schit);
        PlayerPrefs.SetInt("ispolzovano_usilenij_samoroska", date.ispolzovano_usilenij_samoroska);
        PlayerPrefs.SetInt("sbito_gruza_na_nitke", date.sbito_gruza_na_nitke);
        PlayerPrefs.SetInt("sbito_scharikov_za_5_sek", date.sbito_scharikov_za_5_sek);
        PlayerPrefs.SetInt("pokupok", date.pokupok);
        PlayerPrefs.SetInt("popadanij_v_zentr", date.popadanij_v_zentr);


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


    public void Load_Yandex()
    {
        if (YG2.saves.Coin <= 0)
        { PERVOE_SOHRANENIE(); }
        else
        {
            date.Coin = YG2.saves.Coin;
            date.usilenie1_minigun = YG2.GetState("usilenie1_minigun");
            date.usilenie2_arta = YG2.GetState("usilenie2_arta");
            date.usilenie3_zamarozka = YG2.GetState("usilenie3_zamarozka");
            date.usilenie4_schit = YG2.GetState("usilenie4_schit");
            date.aptetschka = YG2.GetState("aptetschka");
            date.progress_lvl[0] = YG2.saves.progress_lvl[0];
            date.progress_lvl[1] = YG2.saves.progress_lvl[1];
            date.progress_lvl[2] = YG2.saves.progress_lvl[2];

            date.Coin_record[0] = YG2.saves.Coin_record[0];
            date.Coin_record[1] = YG2.saves.Coin_record[1];
            date.Coin_record[2] = YG2.saves.Coin_record[2];

            date.language = YG2.saves.language;
            if (YG2.saves.language == "" || YG2.saves.language == null)
            {
                date.language = YG2.envir.language;
            }
            date.razblokirovan_lvl2 = YG2.saves.razblokirovan_lvl2;
            date.razblokirovan_lvl3 = YG2.saves.razblokirovan_lvl3;

            date._prizel_contur = YG2.saves._prizel_contur;
            date._prizel_mushka = YG2.saves._prizel_mushka;

            date.tip_upravlenija = YG2.saves.tip_upravlenija;
            date.inversija_y = YG2.saves.inversija_y;
            date.inversija_x = YG2.saves.inversija_x;
            date.chuvstvitelnost_gyro_base = YG2.saves.chuvstvitelnost_gyro_base;
            date.chuvstvitelnost_gyro_max = YG2.saves.chuvstvitelnost_gyro_max;

            date.ispolzovano_usilenij_minigun = YG2.saves.ispolzovano_usilenij_minigun;
            date.ispolzovano_usilenij_art = YG2.saves.ispolzovano_usilenij_art;
            date.ispolzovano_usilenij_schit = YG2.saves.ispolzovano_usilenij_schit;
            date.ispolzovano_usilenij_samoroska = YG2.saves.ispolzovano_usilenij_samoroska;
            date.sbito_gruza_na_nitke = YG2.saves.sbito_gruza_na_nitke;
            date.sbito_scharikov_za_5_sek = YG2.saves.sbito_scharikov_za_5_sek;
            date.pokupok = YG2.saves.pokupok;
            date.popadanij_v_zentr = YG2.saves.popadanij_v_zentr;



    int pojavilas_novaja_peremennaja = 0;
            if (date.tip_upravlenija == 0)
            { date.tip_upravlenija = date_default_dlja_sbrosa.tip_upravlenija; pojavilas_novaja_peremennaja++; }
            if (date.inversija_y == 0)
            { date.inversija_y = date_default_dlja_sbrosa.inversija_y; pojavilas_novaja_peremennaja++; }
            if (date.inversija_x == 0)
            { date.inversija_x = date_default_dlja_sbrosa.inversija_x; pojavilas_novaja_peremennaja++; }
            if (date.chuvstvitelnost_gyro_base == 0)
            { date.chuvstvitelnost_gyro_base = date_default_dlja_sbrosa.chuvstvitelnost_gyro_base; pojavilas_novaja_peremennaja++; }
            if (date.chuvstvitelnost_gyro_max == 0)
            { date.chuvstvitelnost_gyro_max = date_default_dlja_sbrosa.chuvstvitelnost_gyro_max; pojavilas_novaja_peremennaja++; }

            if (pojavilas_novaja_peremennaja > 0)
                YandexPluginSaveDate();
        }
    }

    public void Load_PlayerPrefs()
    {
        if (PlayerPrefs.GetInt("Coin") == 0)
        { PERVOE_SOHRANENIE(); }
        else
        {
            date.Coin = PlayerPrefs.GetInt("Coin"); //загрузка денег с пк
            date.usilenie1_minigun = PlayerPrefs.GetInt("usilenie1_minigun", date_default_dlja_sbrosa.usilenie1_minigun);
            date.usilenie2_arta = PlayerPrefs.GetInt("usilenie2_arta", date_default_dlja_sbrosa.usilenie2_arta);
            date.usilenie3_zamarozka = PlayerPrefs.GetInt("usilenie3_zamarozka", date_default_dlja_sbrosa.usilenie3_zamarozka);
            date.usilenie4_schit = PlayerPrefs.GetInt("usilenie4_schit", date_default_dlja_sbrosa.usilenie4_schit);
            date.aptetschka = PlayerPrefs.GetInt("aptetschka", date_default_dlja_sbrosa.aptetschka);
            date.progress_lvl[0] = PlayerPrefs.GetInt("progress_lvl_0", date_default_dlja_sbrosa.progress_lvl[0]);
            date.progress_lvl[1] = PlayerPrefs.GetInt("progress_lvl_1", date_default_dlja_sbrosa.progress_lvl[1]);
            date.progress_lvl[2] = PlayerPrefs.GetInt("progress_lvl_2", date_default_dlja_sbrosa.progress_lvl[2]);

            date.Coin_record[0] = PlayerPrefs.GetInt("Coin_record_0", date_default_dlja_sbrosa.Coin_record[0]);
            date.Coin_record[1] = PlayerPrefs.GetInt("Coin_record_1", date_default_dlja_sbrosa.Coin_record[1]);
            date.Coin_record[2] = PlayerPrefs.GetInt("Coin_record_2", date_default_dlja_sbrosa.Coin_record[2]);
            date._prizel_contur = PlayerPrefs.GetInt("_prizel_contur", date_default_dlja_sbrosa._prizel_contur);
            date._prizel_mushka = PlayerPrefs.GetInt("_prizel_mushka", date_default_dlja_sbrosa._prizel_mushka);

            date.tip_upravlenija = PlayerPrefs.GetInt("tip_upravlenija", date_default_dlja_sbrosa.tip_upravlenija);
            date.inversija_y = PlayerPrefs.GetInt("inversija_y", date_default_dlja_sbrosa.inversija_y);
            date.inversija_x = PlayerPrefs.GetInt("inversija_x", date_default_dlja_sbrosa.inversija_x);
            date.chuvstvitelnost_gyro_base = PlayerPrefs.GetFloat("chuvstvitelnost_gyro_base", date_default_dlja_sbrosa.chuvstvitelnost_gyro_base);
            date.chuvstvitelnost_gyro_max = PlayerPrefs.GetFloat("chuvstvitelnost_gyro_max", date_default_dlja_sbrosa.chuvstvitelnost_gyro_max);


            date.ispolzovano_usilenij_minigun = PlayerPrefs.GetInt("ispolzovano_usilenij_minigun", date_default_dlja_sbrosa.ispolzovano_usilenij_minigun);
            date.ispolzovano_usilenij_art = PlayerPrefs.GetInt("ispolzovano_usilenij_art", date_default_dlja_sbrosa.ispolzovano_usilenij_art);
            date.ispolzovano_usilenij_schit = PlayerPrefs.GetInt("ispolzovano_usilenij_schit", date_default_dlja_sbrosa.ispolzovano_usilenij_schit);
            date.ispolzovano_usilenij_samoroska = PlayerPrefs.GetInt("ispolzovano_usilenij_samoroska", date_default_dlja_sbrosa.ispolzovano_usilenij_samoroska);
            date.sbito_gruza_na_nitke = PlayerPrefs.GetInt("sbito_gruza_na_nitke", date_default_dlja_sbrosa.sbito_gruza_na_nitke);
            date.sbito_scharikov_za_5_sek = PlayerPrefs.GetInt("sbito_scharikov_za_5_sek", date_default_dlja_sbrosa.sbito_scharikov_za_5_sek);
            date.pokupok = PlayerPrefs.GetInt("pokupok", date_default_dlja_sbrosa.pokupok);
            date.popadanij_v_zentr = PlayerPrefs.GetInt("popadanij_v_zentr", date_default_dlja_sbrosa.popadanij_v_zentr);




    date.language = PlayerPrefs.GetString("language");

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
    }


    private void PERVOE_SOHRANENIE()
    {
        date = date_default_dlja_sbrosa.Clone();

        Debug.Log("8 ПРОГРЕСС создание метки для первого сохранения");
        //  PlayerPrefs.SetInt("PERVUI_ZAPUSK", 0);
        Save_PlayerPrefs();
        Debug.Log("СБРОС ПРОГРЕССА ПЛЕЕР ПРЕФС");
        YandexPluginSaveDate();

        CsLocalization.Local.SetLanguage(date.language);

        //перезапуск будет в локализации
        //   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//перезапуск уровня для простой перезагрузки значений
    }

    private void YandexPluginSaveDate()
    {
        if (_yandex && _yandex.activeSelf)
        {
            YG2.saves.Coin = date.Coin;

            YG2.SetState("usilenie1_minigun", date.usilenie1_minigun);
            YG2.SetState("usilenie2_arta", date.usilenie2_arta);
            YG2.SetState("usilenie3_zamarozka", date.usilenie3_zamarozka);
            YG2.SetState("usilenie4_schit", date.usilenie4_schit);
            YG2.SetState("aptetschka", date.aptetschka);

            /*YG2.saves.usilenie1_minigun = date.usilenie1_minigun;
            YG2.saves.usilenie2_arta = date.usilenie2_arta;
            YG2.saves.usilenie3_zamarozka = date.usilenie3_zamarozka;
            YG2.saves.usilenie4_schit = date.usilenie4_schit;
            YG2.saves.aptetschka = date.aptetschka;*/

            YG2.saves.Coin_record[0] = date.Coin_record[0];
            YG2.saves.progress_lvl[0] = date.progress_lvl[0];
            YG2.saves.Coin_record[1] = date.Coin_record[1];
            YG2.saves.progress_lvl[1] = date.progress_lvl[1];
            YG2.saves.Coin_record[2] = date.Coin_record[2];
            YG2.saves.progress_lvl[2] = date.progress_lvl[2];


            YG2.saves.razblokirovan_lvl2 = date.razblokirovan_lvl2;
            YG2.saves.razblokirovan_lvl3 = date.razblokirovan_lvl3;
            //   YG2.saves.language = date_default_dlja_sbrosa.language


            YG2.saves._prizel_contur = date._prizel_contur;
            YG2.saves._prizel_mushka = date._prizel_mushka;
            YG2.saves.tip_upravlenija = date.tip_upravlenija;
            YG2.saves.inversija_y = date.inversija_y;
            YG2.saves.inversija_x = date.inversija_x;
            YG2.saves.chuvstvitelnost_gyro_base = date.chuvstvitelnost_gyro_base;
            YG2.saves.chuvstvitelnost_gyro_max = date.chuvstvitelnost_gyro_max;

            YG2.saves.ispolzovano_usilenij_minigun = date.ispolzovano_usilenij_minigun;
            YG2.saves.ispolzovano_usilenij_art = date.ispolzovano_usilenij_art;
            YG2.saves.ispolzovano_usilenij_schit = date.ispolzovano_usilenij_schit;
            YG2.saves.ispolzovano_usilenij_samoroska = date.ispolzovano_usilenij_samoroska;
            YG2.saves.sbito_gruza_na_nitke = date.sbito_gruza_na_nitke;
            YG2.saves.sbito_scharikov_za_5_sek = date.sbito_scharikov_za_5_sek;
            YG2.saves.pokupok = date.pokupok;
            YG2.saves.popadanij_v_zentr = date.popadanij_v_zentr;





    YG2.saves.language = YG2.envir.language;

            Debug.Log("Язык запрошен " + YG2.saves.language);
            //Я сделал язык заглавным в своей локализации
            //потом узнал, что яндекс возвращает его маленькими
            //переписывать его во всех местах - влом
            if (YG2.saves.language == "ru") YG2.saves.language = "RU";
            else if (YG2.saves.language == "es") YG2.saves.language = "ES";
            else if (YG2.saves.language == "it") YG2.saves.language = "IT";
            else if (YG2.saves.language == "ja") YG2.saves.language = "JP";
            else if (YG2.saves.language == "pl") YG2.saves.language = "PL";
            else if (YG2.saves.language == "tr") YG2.saves.language = "TR";
            else if (YG2.saves.language == "zh") YG2.saves.language = "CN";
            else if (YG2.saves.language == "de") YG2.saves.language = "DE";
            else if (YG2.saves.language == "fr") YG2.saves.language = "FR";
            else if (YG2.saves.language == "en") YG2.saves.language = "EN";

            //резервные языки
            else if (YG2.saves.language == "be" || YG2.saves.language == "kk" || YG2.saves.language == "uk" || YG2.saves.language == "uz") YG2.saves.language = "RU";
            else YG2.saves.language = "EN";

            date.language = YG2.saves.language;
            Save_Yandex();
            Debug.Log("СОХРАНЕНИЕ ПРОГРЕССА НА ЯНДЕКСЕ");
        }
    }

    //--------------------------ПОКУПКИ МИКРОТРАНЗАКЦИИ--------------------------------------
    //
    private void OnEnable()
    {
        //подписка на событие
        YG2.onPurchaseSuccess += OnPurchaseSuccess;
        //т.е. если где-то сработает YG2.onPurchaseSuccess
        //то сработает и My OnPurchaseSuccess
        //Он запустится, если произошла покупка
        //также при старте должен быть метод консюмирования - выдачи покупок, если оплата прошла, но произошёл сбой
        //см его в старт
        //он вызывает метод успешной покупки
    }

    private void OnDisable()
    {
        //отписка от события
        YG2.onPurchaseSuccess -= OnPurchaseSuccess;
    }

    private void OnPurchaseSuccess(string id)
    {
        for (int razmer = 0; razmer <=2; razmer++)
        {
            if (id == "a"+(razmer+1)) { date.aptetschka += _purch.purch_aptetschka[razmer]; YG2.SetState("aptetschka", date.aptetschka); }
            else if (id == "m" + (razmer + 1)) { date.usilenie1_minigun += _purch.purch_usilenie1_minigun[razmer]; YG2.SetState("usilenie1_minigun", date.usilenie1_minigun); }
            else if (id == "art" + (razmer + 1)) { date.usilenie2_arta += _purch.purch_usilenie2_arta[razmer]; YG2.SetState("usilenie2_arta", date.usilenie2_arta); }
            else if (id == "z" + (razmer + 1)) { date.usilenie3_zamarozka += _purch.purch_usilenie3_zamarozka[razmer]; YG2.SetState("usilenie3_zamarozka", date.usilenie3_zamarozka); }
            else if (id == "s" + (razmer + 1)) { date.usilenie4_schit += _purch.purch_usilenie4_schit[razmer]; YG2.SetState("usilenie4_schit", date.usilenie4_schit); }
            else if (id == "b" + (razmer + 1))
            {
                date.aptetschka += _purch.purch_aptetschka[razmer]; YG2.SetState("aptetschka", date.aptetschka);
                date.usilenie1_minigun += _purch.purch_usilenie1_minigun[razmer]; YG2.SetState("usilenie1_minigun", date.usilenie1_minigun);
                date.usilenie2_arta += _purch.purch_usilenie2_arta[razmer]; YG2.SetState("usilenie2_arta", date.usilenie2_arta);
                date.usilenie3_zamarozka += _purch.purch_usilenie3_zamarozka[razmer]; YG2.SetState("usilenie3_zamarozka", date.usilenie3_zamarozka);
                date.usilenie4_schit += _purch.purch_usilenie4_schit[razmer]; YG2.SetState("usilenie4_schit", date.usilenie4_schit);
            }
        }
        date.pokupok++;
        Save();
        Debug.Log("Покупка №" + date.pokupok);
    }


    //--------------------------УСИЛЕНИЯ--------------------------------------
    //использование усиления
    public void Usilenie(int nomer)
    {
        if (nomer == 1)
        {
            date.usilenie1_minigun--;
            date.ispolzovano_usilenij_minigun++;
    YG2.SetState("usilenie1_minigun", date.usilenie1_minigun);
        }
        else if (nomer == 2)
        {
            date.usilenie2_arta--;
            date.ispolzovano_usilenij_art++;
    YG2.SetState("usilenie2_arta", date.usilenie2_arta);
        }
        else if (nomer == 3)
        {
            date.usilenie3_zamarozka--;
    date.ispolzovano_usilenij_samoroska++;
    YG2.SetState("usilenie3_zamarozka", date.usilenie3_zamarozka);
        }
        else if (nomer == 4)
        {
            date.usilenie4_schit--;
    date.ispolzovano_usilenij_schit++;
    YG2.SetState("usilenie4_schit", date.usilenie4_schit);
        }
        else if (nomer == 5)
        {
            aptetschka();
        }

        Save();
    }
    public void aptetschka()
    {
        date.aptetschka--;
        YG2.SetState("aptetschka", date.aptetschka);
        Save();
    }

    //разблокировка уровня
    public void Razblokirovka_urovnja()
    {
        if (nomer_missii == date.progress_lvl[nomer_lvl - 1])
        {
            nomer_missii++;
            date.progress_lvl[nomer_lvl - 1] = nomer_missii;
        }
        Save(0);

        return;

    }
    public void Next_LVL()
    {
        SceneManager.LoadScene("LVL_" + nomer_lvl + " " + nomer_missii);
    }

    public void set_nomer_lvl(int a)
    {
        nomer_lvl = a;
    }
    public int get_nomer_lvl()
    {
        return nomer_lvl;
    }

    public void set_nomer_missii(int a)
    {
        nomer_missii = a;
    }
    public int get_nomer_missii()
    {
        return nomer_missii;
    }

    public bool get_web_telefon()
    {
        return web_telefon;
    }

    public void Sbros_progressa()
    {
        Debug.Log("СБРОС ПРОГРЕССА ЗАПУЩЕН");
        PERVOE_SOHRANENIE();
    }

    public void SetLanguage(string _language)
    {
        date.language = _language;
        YG2.saves.language = _language;
        Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }

    public void Set_prizel_contur(int a)
    {
        date._prizel_contur = a;
        Save();
    }
    public void Set_prizel_mushka(int a)
    {
        date._prizel_mushka = a;
        Save();
    }
    public int Get_prizel_contur()
    {
       return date._prizel_contur;
    }
    public int Get_prizel_mushka()
    {
       return date._prizel_mushka;
    }

    public int get_tip_upravlenija()
    {
        return date.tip_upravlenija;
    }
    public void set_tip_upravlenija(int a)
    {
        date.tip_upravlenija=a;
    }
    public void set_chuvstvitelnost_gyro_base(float a)
    {
        date.chuvstvitelnost_gyro_base = a;
    }
    public void set_chuvstvitelnost_gyro_max(float a)
    {
        date.chuvstvitelnost_gyro_max = a;
    }
    public void set_inversija_x(int a)
    {
        date.inversija_x = a;
    }
    public void set_inversija_y(int a)
    {
        date.inversija_y = a;
    }
    
    public void popadanij_v_zentr()
    {
        date.popadanij_v_zentr++;
        Debug.Log("Попаданий в яблочко: " + date.popadanij_v_zentr);
    }
    public void sbito_gruza_na_nitke()
    {
        date.sbito_gruza_na_nitke++;
        Debug.Log("Сбито груза на нитке: " + date.sbito_gruza_na_nitke);
    }

}

//---------------ЛЕГАСИ----------------------

/*
private bool PERVUI_ZAPUSK()
{
    return !PlayerPrefs.HasKey("PERVUI_ZAPUSK");
    //Это поле при первом запуске не существует вообще. Впринципе
    //и речь не про значение в нём, а про всё поле в принципе.
    //поэтому в стартовом if при первом запуске его не получится найти
    //и мы сохраним данные, которые я вёл в инспекторе
}*/



/*
bool igrok_avtorizirovan = true;
public void Set_igrok_avtorizirovan(bool value)
{
    igrok_avtorizirovan = value;
}
*/

/*
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
   }*/




//для загрузки на старте вызываем функцию в яндексе
//она вызывает функцию в js
//он вызывает функцию в яндекс c#
//и уже он потом вызывает этот метод
/*
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


}*/
/* //СОХРАНЕНИЯ В ЯНДЕКСЕ
public void Save_Yandex()
{
    if (_yandex)
    { if (_yandex.activeSelf)
        {
            Debug.Log("9-1 ПРОГРЕСС сохранение в яндексе КОНЕЦ");
            //данные в яндекс передаются только как строчка
            //преобразуем класс с данными в строку
            string dateJS = JsonUtility.ToJson(date);
            //и вызываем код в джава скрипт
            _csYandex.Save(dateJS);
        }
}
}*/