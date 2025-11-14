using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOtschistkaMusora_2 : MonoBehaviour
{
    [SerializeField] int prioritet_udalenija;
    [SerializeField] GameObject _roditel;
    [SerializeField] CsOtschistkaMusora_2 _svjaska;
    [SerializeField] GameObject _deti1; //для родительского компонента связки
    [SerializeField] GameObject _deti2;
    [SerializeField] GameObject _deti3;
    [SerializeField] GameObject _zruz1;
    int detej;

    public void otschistka_misora(int a)
    {
        if (prioritet_udalenija >= a)
        {
            if(_svjaska)
            _svjaska.otschistka_misora_svjaska();
            if (_roditel)
            { Destroy(_roditel); }
            else
            { Destroy(gameObject); }
        }
    }
    
    public void otschistka_misora_svjaska()
    {//посчитай, остались ли у него шарики и груз. Если это последний шарик (или остался только груз) - дропни всю связку (родителя)
        detej = 0;
        if (_deti1) { detej++; }
        if (_deti2) { detej++; }
        if (_deti3) { detej++; }
        if (_zruz1) { detej++; }

        if (detej <= 1)
        { Destroy(gameObject); }
    }
}
