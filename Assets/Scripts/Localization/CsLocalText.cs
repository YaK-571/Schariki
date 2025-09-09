using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Editor;
using UnityEngine;

public class CsLocalText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_self;
    [SerializeField] string _Language;
    [SerializeField] string _id_fraza;

    void Start()
    {
        if(_Language==null)
        {
            _Language = "RU";
        }
        _text_self=gameObject.GetComponent<TextMeshProUGUI>();
        _text_self.text= CsLocalization.Local.GetText(_id_fraza, _Language);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
