using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csRazblokirovka_urovnja : MonoBehaviour
{
    [SerializeField] int _nomer_cartu;
    [SerializeField] int _nomer_lvl;
    [SerializeField] int _stoimost;

    [SerializeField] csVuvodMonet _csVuvodMonet;

  //  [SerializeField] GameObject _canvas_obnovljaemuj;
    [SerializeField] csScena[] _csScena;
    public void set_parametru_kartu(int a, int b, int c)
    {
        _nomer_cartu = a; 
        _nomer_lvl = b;
        _stoimost = c;

    }


    public void pokupka_lvl()
    {
        bool resultat_pokupki = Progress.GameInstance.Razblokirovka_urovnja(_nomer_cartu, _nomer_lvl, _stoimost);
        
        if(resultat_pokupki)
        {
            _csVuvodMonet.obnovlenie_monet();

            for (int i = 0; i < _csScena.Length; i++)
            {
                _csScena[i].obnovlenie_knopok();
            }
            gameObject.SetActive(false);
        }
    }
}
