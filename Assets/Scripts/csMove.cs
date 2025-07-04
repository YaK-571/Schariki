using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _telo;
    Vector2 napravlenie = Vector2.zero;
    bool WASD = false;

    [SerializeField] csBomba _bomba;
    [SerializeField] csVsruv _vsruv;
    [SerializeField] csMischen _mischen;

    // [SerializeField] GameObject _canvas_game_over;
    [SerializeField] GameManager _gameManager;
    [SerializeField] csVspuschka _vspuschka;
    [SerializeField] GameObject _vspuschka_prefab;





    int coef_coin = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //движение
        if (Input.GetKey(KeyCode.W))
        {
            napravlenie.y = 1;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            napravlenie.y = -1;

        }
        else
        {
            // napravlenie.y = 0;

        }
        if (Input.GetKey(KeyCode.D))
        {
            napravlenie.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            napravlenie.x = -1;
        }
        else
        {
            // napravlenie.x = 0;
        }
        //napravlenie.Normalize(); 
        //нормализация нужна, чтобы если я нажму на две кнопки сразу, то скорость по диагонале не получилась больше задуманого
        _telo.velocity = napravlenie * _speed;
        napravlenie.x = 0;
        napravlenie.y = 0;


        //отрисовка луча
        //Debug.DrawRay(transform.position, Vector2.zero, Color.red, 1f);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            vustrel();
        }

    }

    public void vustrel()
    {
        //телепорт вспышки в нужное место
        //_vspuschka.teleport(transform.position);
        GameObject vspuschka_spawn = Instantiate(_vspuschka_prefab);
        vspuschka_spawn.transform.position = transform.position;

        //выстрел лучём, проходящим через множество обьектов
        RaycastHit2D[] hit = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero);
        //проверка попалили мы в шар или бомбу
        for (int i = 0; i < hit.Length; i++)
        {


            //При попадании в центр мишени коэфф увеличивается
            if (hit[i].collider.gameObject.GetComponent<csMischen_zentr>())
            {
                coef_coin = coef_coin * 2;
            }
            //При попадании в мишень увеличивай коэфф баллов
            else if (hit[i].collider.gameObject.GetComponent<csMischen>())
            {
                _mischen = hit[i].collider.gameObject.GetComponent<csMischen>();
                coef_coin = coef_coin + 1;
            }

            if (hit[i].collider.gameObject.GetComponent<csBomba>())
            {
                _bomba = hit[i].collider.gameObject.GetComponent<csBomba>();

                _gameManager.HP(-1);

            }
            if (hit[i].collider.gameObject.GetComponent<csVsruv>())
            {
                _vsruv = hit[i].collider.gameObject.GetComponent<csVsruv>();
                _gameManager.Coin(_vsruv.get_ballu() * coef_coin);
                _vsruv.Vsruv();
            }
        }
    }


    public void Set_Napravlenie_stik(Vector2 a)
    {
        napravlenie = a;
    }

}
