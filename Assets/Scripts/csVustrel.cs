using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csVustrel : MonoBehaviour
{
    [SerializeField] csBomba _bomba;
    [SerializeField] csVsruv _vsruv;
    [SerializeField] csMischen _mischen;

    // [SerializeField] GameObject _canvas_game_over;
    [SerializeField] GameManager _gameManager;
    //[SerializeField] csVspuschka _vspuschka;
    [SerializeField] GameObject _vspuschka_prefab;

    int coef_coin = 1;
  
    void Start()
    {
        
    }

    void Update()
    {
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
}
