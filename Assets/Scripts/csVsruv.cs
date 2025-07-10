using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class csVsruv : MonoBehaviour
{

    [SerializeField] csSchar _schar;
    [SerializeField] GameObject _Square;
    [SerializeField] public bool _popadanie_v_schar;
    [SerializeField] GameObject component_vsruv;


    [SerializeField] int _ballu;
    [SerializeField] HingeJoint2D _szepka;
    [SerializeField] csUskorenie_svjaski _svjaska;
    public void Vsruv()
    {

        //если у нас был груз и мы отстрелили верёвочку, то отвязываем его и ускоряем шар

        if (_szepka && _schar)
        {
            //ускорение шарика без груза
            _schar.skorost();
            _szepka.enabled = false;
        }

        if (_popadanie_v_schar) //деактивируем подьём шара, чтобы оставшаяся его часть могла упасть

        {
            _schar.enabled = false;
        }
        //если мы сбили груз в связке, то её надо ускорить
        if (_svjaska)
        {
            _svjaska.Uskorenie();
        }

       // Destroy(_Square);//удаляем верёвочку тоже
        //удаление обьекта по которому мы попали
        Destroy(component_vsruv);

    }

    public int get_ballu()
    {
        return _ballu;
    }
}