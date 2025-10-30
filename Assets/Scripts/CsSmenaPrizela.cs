using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CsSmenaPrizela : MonoBehaviour
{
    [SerializeField] SpriteRenderer _contur;
    [SerializeField] SpriteRenderer _mushka;

    int number_contur;
    int number_mushka;

    [SerializeField] Sprite[] _prizel_contur = new Sprite[20];
    [SerializeField] Sprite[] _prizel_mushka = new Sprite[20];

    void Start()
    {
        number_contur = Progress.GameInstance.Get_prizel_contur();
        number_mushka = Progress.GameInstance.Get_prizel_mushka();
        if(number_contur<= _prizel_contur.Length)
        {} else {  number_contur = 0; }
        _contur.sprite = _prizel_contur[number_contur];

        if (number_mushka <= _prizel_mushka.Length)
        { } else { number_mushka = 0; }
        _mushka.sprite = _prizel_mushka[number_mushka];
    }

}
