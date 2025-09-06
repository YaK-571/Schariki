using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Cs_Nastrojki_defolt : MonoBehaviour
{
    [SerializeField] Slider _slider_gyro_max;
    [SerializeField] Slider _slider_gyro_base;
    [SerializeField] csNastrojki _nastrojki_gyro_max;
    [SerializeField] csNastrojki _nastrojki_gyro_base;
    [SerializeField] csNastrojki _nastrojki_inversija;

    private float chuvstvitelnost_gyro_base = 0.5f;
    private float chuvstvitelnost_gyro_max=2.0f;

    Progress GameInstance;

    private void Start()
    {
        GameInstance = Progress.GameInstance;
        if (_slider_gyro_max)
        {
            _slider_gyro_max.value = GameInstance.chuvstvitelnost_gyro_max;
      //      _nastrojki_gyro_max.chuvstvitelnost_gyro_max();
        }
        if (_slider_gyro_base)
        {
            _slider_gyro_base.value = GameInstance.chuvstvitelnost_gyro_base;
       //     _nastrojki_gyro_base.chuvstvitelnost_gyro_base();
        }
    }

    //сбрасывание настроек управления на по умолчанию
    public void Defolt()
    {
        _slider_gyro_base.value = chuvstvitelnost_gyro_base;
        _slider_gyro_max.value = chuvstvitelnost_gyro_max;

        _nastrojki_gyro_base.chuvstvitelnost_gyro_base();
        _nastrojki_gyro_max.chuvstvitelnost_gyro_max();

        _nastrojki_inversija.inversija_x_vukl();
        _nastrojki_inversija.inversija_y_vukl();

    }
}
