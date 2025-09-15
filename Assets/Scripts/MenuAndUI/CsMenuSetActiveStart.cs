using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsMenuSetActiveStart : MonoBehaviour
{
    [SerializeField] GameObject _ui;
    [SerializeField] GameObject _ui2;
    [SerializeField] GameObject _ui3;

    // Start is called before the first frame update
    void Start()
    {
        if(_ui)
        _ui.SetActive(false);
        if (_ui2)
            _ui2.SetActive(false);
        if (_ui3)
            _ui3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
