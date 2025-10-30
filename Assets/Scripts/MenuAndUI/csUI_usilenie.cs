using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class csUI_usilenie : MonoBehaviour
{
    [SerializeField] GameObject _usilenie1;
    [SerializeField] GameObject _usilenie2;
    [SerializeField] GameObject _usilenie3;
    [SerializeField] GameObject _usilenie4;
  //  [SerializeField] GameObject _usilenie5;

    //дл€ обновлени€ числа усилений
    [SerializeField] TextMeshProUGUI _text1;
    [SerializeField] TextMeshProUGUI _text2;
    [SerializeField] TextMeshProUGUI _text3;
    [SerializeField] TextMeshProUGUI _text4;
    [SerializeField] TextMeshProUGUI _text5;

    private int _tschislo_1;
    private int _tschislo_2;
    private int _tschislo_3;
    private int _tschislo_4;
 //   private int _tschislo_5;

    bool actyve = false;
    [SerializeField] GameManager _gameManager;

    [SerializeField] GameObject _minigan;

    [SerializeField] GameObject _artobstrel;
    [SerializeField] csSpawn_Vspuschek _spawn_vspuschek;
    [SerializeField] csGraniza_nischnjaja _graniza_niz;

    //[SerializeField] csGraniza_nischnjaja _zamerzanie_graniza;


    private void Start()
    {


        //ситуаци€ в том, что Game Instance создаЄтс€ при открытии игры в меню. далее он не удал€етс€
        //но при разработке мы открываем сразу уровень без меню, поэтому гейм инстанс спавнитс€ там
        //мы обращаемс€ к нему тут дл€ загрузки данных, но т.к. он спавнитс€ после загрузки уровн€
        //то тут он не успевает создастьс€ до того, как мы к нему обратились
        //поэтому надо вызвать короутину, котора€ будет провер€ть каждый кадр не создалс€ ли game instance

        //OnEnable вместо start использовать не получилось, т.к. оказалось,
        //что он вызываетс€ не после всех start, а после start  внутри этого конкретного обьекта
        //получаетс€ тут может пройти OnEnable, а в другом обьекте ещЄ не быть выполненым
        if (Progress.GameInstance != null)
        {
            vuvod_tschislo_usilenij();
        }
        else
        {
            StartCoroutine(WaitForProgressInstance());
        }
    }

    //короутина провер€ет не создалс€ ли game instance каждый кадр, а потом вызывает загрузку данных с него
    private IEnumerator WaitForProgressInstance()
    {
        while (Progress.GameInstance == null)
            yield return null;

        vuvod_tschislo_usilenij();
    }

    public void Pokaz_usileniy()
    {
        if (actyve)
        {
            actyve = false;
        }
        else
        {
            actyve = true;
        }
        _usilenie1.SetActive(actyve);
        _usilenie2.SetActive(actyve);
        _usilenie3.SetActive(actyve);
        _usilenie4.SetActive(actyve);
     //   _usilenie5.SetActive(actyve);
    }

    private void vuvod_tschislo_usilenij()
    {

        var date = Progress.GameInstance.date;
        _tschislo_1 = date.usilenie1_minigun;
        _tschislo_2 = date.usilenie2_arta;
        _tschislo_3 = date.usilenie3_zamarozka;
        _tschislo_4 = date.usilenie4_schit;
      //  _tschislo_5 = date.aptetschka;

        //отображение числа доступных усилений на кнопках
        _text1.text = _tschislo_1.ToString();
        _text2.text = _tschislo_2.ToString();
        _text3.text = _tschislo_3.ToString();
        _text4.text = _tschislo_4.ToString();
      //  _text5.text = _tschislo_5.ToString();
    }


    //»спользование усилений
    public void Minigan()
    {
        if (_tschislo_1 >= 1)
        {
            Progress.GameInstance.Usilenie(1);
            _minigan.SetActive(true);
            vuvod_tschislo_usilenij();
            Pokaz_usileniy();
        }

    }
    public void Artobstrel()
    {
        if (_tschislo_2 >= 1)
        {
            Progress.GameInstance.Usilenie(2);
            _artobstrel.SetActive(true);
            _spawn_vspuschek.Spawn_vspuschki();
            _graniza_niz.artillerija_vzruv_scharov();
            vuvod_tschislo_usilenij();
            Pokaz_usileniy();
        }
    }
    public void Zamerzanie()
    {
        if (_tschislo_3 >= 1)
        {
            Progress.GameInstance.Usilenie(3);
            _graniza_niz.Zamerzanie();
            vuvod_tschislo_usilenij();
            Pokaz_usileniy();
        }
    }
    public void Schit()
    {
        if (_tschislo_4 >= 1)
        {
            Progress.GameInstance.Usilenie(4);
            _gameManager.activazija_schita();
            vuvod_tschislo_usilenij();
            Pokaz_usileniy();
        }
    }

}

