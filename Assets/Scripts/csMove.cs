using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _telo;
    Vector2 napravlenie_WASD = Vector2.zero;
    Vector2 napravlenie_stik = Vector2.zero;
    Vector2 napravlenie_gyro = Vector2.zero;

   
    void Update()
    {
        _telo.velocity = (napravlenie_WASD + napravlenie_stik + napravlenie_gyro) * _speed;
        //time.DeltaTime НЕ НУЖНО! Т.к. движение через физику РиджидБади

        //чтобы на следующем кадре перемещение останавливалось, а не длилось бесконечно
        napravlenie_WASD = Vector2.zero;
        napravlenie_stik = Vector2.zero;
        napravlenie_gyro = Vector2.zero;
    }

    public void Set_Napravlenie_WASD(Vector2 newNapravlenie)
    {
        //сюда передаётся уже нормализованное
        napravlenie_WASD = newNapravlenie;
    }
    public void Set_Napravlenie_stik(Vector2 newNapravlenie)
    {
        //сюда передаётся уже нормализованное
        napravlenie_stik = newNapravlenie;
    }
    public void Set_Napravlenie_gyro(Vector2 newNapravlenie)
    {
        //сюда передаётся уже нормализованное
        napravlenie_gyro = newNapravlenie;
    }

    //прицеливание мышью, только если она в экране и если не используются другие способы ввода
   

}


