using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGlavnoeMenu : MonoBehaviour
{
    [SerializeField] GameObject _canvas_parent;
    [SerializeField] GameObject _LVL;
    [SerializeField] GameObject _zamok;
    [SerializeField] GameObject _zamok_text;
    [SerializeField] int nomer_lvl;
    [SerializeField] bool activnost_knopki;
    public void skrut_menu()
    {
        _canvas_parent.SetActive(false);

    }

    public void LVL()
    {
        if (activnost_knopki == true)
        {
            _LVL.SetActive(true);
            skrut_menu();
        }

    }

    private void Start()
    {
        if (_zamok)
        {

            if (nomer_lvl == 2 && Progress.GameInstance.date.progress_lvl2)
            {
                _zamok.SetActive(false);
                _zamok_text.SetActive(false);
                activnost_knopki = true;
            }
            else if (nomer_lvl == 3 && Progress.GameInstance.date.progress_lvl3)
            {
                _zamok.SetActive(false);
                _zamok_text.SetActive(false);
                activnost_knopki = true;
            }
        }

    }
}
