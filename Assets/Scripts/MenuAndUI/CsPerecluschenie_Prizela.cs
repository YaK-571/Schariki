using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsPerecluschenie_Prizela : MonoBehaviour
{
    [SerializeField] bool _muschka = false;
    [SerializeField] int tip_prizela;

    private void Start()
    {
        //Progress.GameInstance.date._prizel_mushka;
    }

    public void smena_prizela()
    {
        if (_muschka)
        {
            Progress.GameInstance.Set_prizel_mushka(tip_prizela);
        }
        else
        {
            Progress.GameInstance.Set_prizel_contur(tip_prizela);
        }




    }
}
