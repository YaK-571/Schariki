using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csUI_HP : MonoBehaviour
{
    [SerializeField] Transform[] hp;
    int i = 0;

    public void Awake() //эта хуита в Start вызывалась после Update_HP, пришлось перенести в Awake
    {

        hp = new Transform[10];
        foreach (Transform Serdechko in gameObject.transform)
        {
            hp[i] = Serdechko;
            i++;
        }
    }

    public void Update_HP(int HP)
    {
        //foreach - тоже самое, что цикл. Сделай это для каждого дочернего обьекта в этом обьекте
        foreach (Transform Serdechko in hp) //
        {
            Serdechko.gameObject.SetActive(false);
        }
        int a = 0;
        while (a < HP)
        {
            hp[a].gameObject.SetActive(true);
            a++;
        }

    }

}
