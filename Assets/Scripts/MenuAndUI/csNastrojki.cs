using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csNastrojki : MonoBehaviour
{
    [SerializeField] GameObject _Canvas_Nastrojki;
    [SerializeField] Slider _slider;
    [SerializeField] bool _gyro_max;
    [SerializeField] bool _gyro_base;

    [SerializeField] bool _x;
    [SerializeField] bool _y;
    [SerializeField] bool _x_vkl;
    [SerializeField] bool _y_vkl;
    [SerializeField] bool _x_vukl;
    [SerializeField] bool _y_vukl;

    Progress MyGameInstance;

    //Progress GameInstance;
    // Start is called before the first frame update
    void Start()
    {
        MyGameInstance = Progress.GameInstance;
     //   GameInstance = Progress.GameInstance;
      if (_gyro_base)
        { _slider.value = MyGameInstance.date.chuvstvitelnost_gyro_base; }
      else if (_gyro_max)
        { _slider.value = MyGameInstance.date.chuvstvitelnost_gyro_max; }


        if (_x)
        {
            if ((MyGameInstance.date.inversija_x == -1 && _x_vkl)|| (MyGameInstance.date.inversija_x == 1 && _x_vukl))
            { }
            else { gameObject.SetActive(false); }
        }
        else if (_y)
        {
            if ((MyGameInstance.date.inversija_y == -1 && _y_vkl) || (MyGameInstance.date.inversija_y == 1 && _y_vukl))
            { }
            else { gameObject.SetActive(false); }
        }

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
        Progress.GameInstance.set_inversija_x(1);
        Vkl_Vukl();
    }
    public void inversija_x_vukl()
    {
        Progress.GameInstance.set_inversija_x(-1);
        Vkl_Vukl();
    }
    public void inversija_y_vkl()
    {
        Progress.GameInstance.set_inversija_y (1);
        Vkl_Vukl();

    }
    public void inversija_y_vukl()
    {
        Progress.GameInstance.set_inversija_y (-1);
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
        Progress.GameInstance.set_chuvstvitelnost_gyro_base (_slider.value);
    }
    public void chuvstvitelnost_gyro_max()
    {
        Progress.GameInstance.set_chuvstvitelnost_gyro_max(_slider.value);
    }

    public void Save()
    {
        Progress.GameInstance.Save();
    }
}
