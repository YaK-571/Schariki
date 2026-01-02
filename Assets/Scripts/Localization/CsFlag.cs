using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CsFlag : MonoBehaviour
{
    [SerializeField] string _language;

    public void perevod()
    {
        Progress.GameInstance.date.ruchnoj_vubor_jazuka = 1;
        CsLocalization.Local.SetLanguage( _language );
    }
}
