using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsNastrojkiSetActive : MonoBehaviour
{
    [SerializeField]  GameObject _ui_actyve;
    [SerializeField] GameObject _ui_self;

    public void vubor()
    {
        _ui_actyve.SetActive(true);
        if(_ui_self)
        _ui_self.SetActive(false);

    }
}
