using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CsLocalText : MonoBehaviour
{
    TextMeshProUGUI _text_self;
    string _Language;
    [SerializeField] string _id_fraza;
    TMP_FontAsset atlas;

    void Start()
    {
        _Language = CsLocalization.Local.GetLanguage();

        if (_Language==null)
        {
            _Language = "RU";
        }
        _text_self=gameObject.GetComponent<TextMeshProUGUI>();

        atlas = CsLocalization.Local.GetAtlas();
        if (atlas) { _text_self.font = atlas; }
        
        _text_self.text= CsLocalization.Local.GetText(_id_fraza);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
