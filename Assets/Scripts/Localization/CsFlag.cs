using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFlag : MonoBehaviour
{
    [SerializeField] string _language;

    public void perevod()
    {
        CsLocalization.Local.SetLanguage( _language );
    }
}
