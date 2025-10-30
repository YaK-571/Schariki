using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsNastrojkiSposobUpravlenija : MonoBehaviour
{
    [SerializeField] GameObject _obj0;
    [SerializeField] GameObject _obj1;
    [SerializeField] GameObject _obj2;
    int tip_upravlenija;
    void Start()
    {
        tip_upravlenija=Progress.GameInstance.get_tip_upravlenija();
        Vubor_Upravlenija(tip_upravlenija);
    }

    public void Vubor_Upravlenija(int a=0)
    {
        tip_upravlenija = a;
        if (tip_upravlenija == 0)
        {
            _obj0.SetActive(true);
            _obj1.SetActive(false);
            _obj2.SetActive(false);
        }
        else if (tip_upravlenija == 1)
        {
            _obj0.SetActive(false);
            _obj1.SetActive(true);
            _obj2.SetActive(false);
        }
        else if (tip_upravlenija == 2)
        {
            _obj0.SetActive(false);
            _obj1.SetActive(false);
            _obj2.SetActive(true);
        }

        Progress.GameInstance.set_tip_upravlenija(tip_upravlenija);
    }
}
