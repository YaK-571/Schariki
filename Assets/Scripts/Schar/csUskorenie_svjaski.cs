using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csUskorenie_svjaski : MonoBehaviour
{
    [SerializeField] csSchar[] _schar;
    int tschislo_scharov;

    void Start()
    {
        tschislo_scharov = _schar.Length;
    }

    public void Uskorenie()
    {
        int i = 0;
        while(i< tschislo_scharov)
        {
            _schar[i].skorost();
            i++;
        }    
    }
}
