using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csUI_HP : MonoBehaviour
{
    [SerializeField] Transform[] hp;
     int i = 0;

    public void Start()
    {
        hp = new Transform[10];
        foreach (Transform Serdechko in gameObject.transform) //
        {
            hp[i]= Serdechko;
            i++;
        }
        
    }
    public void Update_HP(int HP)
    {
        //foreach - ���� �����, ��� ����. ������ ��� ��� ������� ��������� ������� � ���� �������
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
