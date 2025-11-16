using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsNitka : MonoBehaviour
{
    [SerializeField] GameObject _gruz;

    public bool sbiv_gruza()
    {
        if (_gruz)
        {
            return true;
        }
        else 
        { return false; }
    }
}
