using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csScena : MonoBehaviour
{
    [SerializeField] string name_lvl;
    [SerializeField] int nomer_cartu;
    [SerializeField] int nomer_missii;
    [SerializeField] int _stoimost;
    [SerializeField] GameObject _image_zamok;
    [SerializeField] GameObject _button_razblokirovka;
    [SerializeField] GameObject _razblokirovka_canvas;
    [SerializeField] csRazblokirovka_urovnja _csRazblokirovka_urovnja;

    bool actyvnost_knopki;
    public void start_lvl()
    {
        //запусти уровень, если кнопка разблокирована
        if (actyvnost_knopki == true)
        {
            SceneManager.LoadScene(name_lvl);
        }
        //если эта мисси€ следующа€ дл€ разблокировки, то предложи еЄ разблокировать
        else if(Progress.GameInstance.date.progress_lvl[nomer_cartu - 1] == nomer_missii - 1)
        {
            _razblokirovka_canvas.SetActive(true);
            _csRazblokirovka_urovnja.set_parametru_kartu(nomer_cartu, nomer_missii, _stoimost);
        }
    }

    private void Start()
    {
        obnovlenie_knopok();
    }

    public void obnovlenie_knopok()
    {
        _button_razblokirovka.SetActive(true);

        //разблокировать все открытые кнопки
        if (Progress.GameInstance.date.progress_lvl[nomer_cartu - 1] >= nomer_missii)
        {
            actyvnost_knopki = true;
            _image_zamok.SetActive(false);
        }
        else
        {
            actyvnost_knopki = false;
        }

        //отобрази кнопку дл€ покупки, если эта мисси€ следующа€ дл€ разблокировки
        if (Progress.GameInstance.date.progress_lvl[nomer_cartu - 1] != nomer_missii - 1)
        {
            _button_razblokirovka.SetActive(false);
        }
    }
}
