using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csNastrojki : MonoBehaviour
{
    [SerializeField] GameObject _Canvas_Nastrojki;
    [SerializeField] Slider _slider;


    //Progress GameInstance;
    // Start is called before the first frame update
    void Start()
    {
     //   GameInstance = Progress.GameInstance;
      
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Vkluchi_Nastrojki()
    {
        _Canvas_Nastrojki.SetActive(true);
    }

    public void Zakroj_Nastrojki()
    {
        _Canvas_Nastrojki.SetActive(false);
    }



    //инверсия гироскопа
    public void inversija_x_vkl()
    {
        Progress.GameInstance.inversija_x=-1;
        Vkl_Vukl();
    }
    public void inversija_x_vukl()
    {
        Progress.GameInstance.inversija_x = 1;
        Vkl_Vukl();
    }
    public void inversija_y_vkl()
    {
        Progress.GameInstance.inversija_y = -1;
        Vkl_Vukl();

    }
    public void inversija_y_vukl()
    {
        Progress.GameInstance.inversija_y = 1;
        Vkl_Vukl();
    }

    public void Vkl_Vukl()
    {
        gameObject.SetActive(false);
        _Canvas_Nastrojki.SetActive(true);
    }

    //Чувствительность гироскопа
    public void chuvstvitelnost_gyro_base()
    {
        Progress.GameInstance.chuvstvitelnost_gyro_base = _slider.value;
    }
    public void chuvstvitelnost_gyro_max()
    {
        Progress.GameInstance.chuvstvitelnost_gyro_max = _slider.value;
    }
}
