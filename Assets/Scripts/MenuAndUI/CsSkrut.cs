using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSkrut : MonoBehaviour
{
    [SerializeField] GameObject[] _skrut;
    [SerializeField] GameObject[] _otobrazit;

    private void OnEnable()
    {
        foreach (GameObject obj in _skrut)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in _otobrazit)
        {
            obj.SetActive(true);
        }
    }
}
