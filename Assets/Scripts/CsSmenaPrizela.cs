using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsSmenaPrizela : MonoBehaviour
{
    public static event System.Action Event_SmenaPrizela;

    [SerializeField] SpriteRenderer _contur;
    [SerializeField] SpriteRenderer _mushka;

    [SerializeField] Image _contur_menu;
    [SerializeField] Image _mushka_menu;

    int number_contur;
    int number_mushka;

    [SerializeField] Sprite[] _prizel_contur = new Sprite[20];
    [SerializeField] Sprite[] _prizel_mushka = new Sprite[20];

    void Start()
    {
        Event_SmenaPrizela += SmenaPrizela; //подписка на событие
        SmenaPrizela();

    }

    public void SmenaPrizela()
    {
        number_contur = Progress.GameInstance.Get_prizel_contur();
        number_mushka = Progress.GameInstance.Get_prizel_mushka();


        //если значение не отсутствует - то включаем дефолтный прицел
        if (number_contur <= _prizel_contur.Length)
        { }
        else { number_contur = 0; }

        if (number_mushka <= _prizel_mushka.Length)
        { }
        else { number_mushka = 0; }

        //переключение прицелов
        if (_contur != null)
        { _contur.sprite = _prizel_contur[number_contur]; }

        if (_mushka != null)
        { _mushka.sprite = _prizel_mushka[number_mushka]; }


        if (_contur_menu != null)
        { _contur_menu.sprite = _prizel_contur[number_contur]; }
        if (_mushka_menu != null)
        { _mushka_menu.sprite = _prizel_mushka[number_mushka]; }
    }

}
