using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSkrut : MonoBehaviour
{
    [SerializeField] GameObject[] _skrut;

    private void Start()
    {
        foreach (GameObject obj in _skrut)
        {
            obj.SetActive(false);
        }
    }
}
