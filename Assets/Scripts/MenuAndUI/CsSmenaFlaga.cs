using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static Unity.VisualScripting.Icons;

public class CsSmenaFlaga : MonoBehaviour
{
    [SerializeField] string language;
    [SerializeField] Image _button;
    [SerializeField] Sprite [] _new_flag;

    // Start is called before the first frame update
    void Start()
    {
        if(CsLocalization.Local)
        language=CsLocalization.Local.GetLanguage();
        if(language=="")
        {
            language = "RU";
        }
        int i=0;
        if (language == "RU") i = 0;
        if (language == "EN") i = 1;
        if (language == "JP") i = 2;
        if (language == "CN") i = 3;
        if (language == "KR") i = 4;
        if (language == "DE") i = 5;
        if (language == "FR") i = 6;
        if (language == "IT") i = 7;
        if (language == "ES") i = 8;
        if (language == "PL") i = 9;
        if (language == "TR") i = 10;
        if (language == "SE") i = 11;

        _button.sprite = _new_flag[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
