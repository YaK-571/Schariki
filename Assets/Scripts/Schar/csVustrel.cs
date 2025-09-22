using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class csVustrel : MonoBehaviour
{

    [SerializeField] csBomba _bomba;
    [SerializeField] csVsruv _vsruv;
    [SerializeField] csMischen _mischen;
    [SerializeField] CsDirigible dirigible;
    [SerializeField] CsBallu ballu;

    // [SerializeField] GameObject _canvas_game_over;
    [SerializeField] GameManager _gameManager;
    //[SerializeField] csVspuschka _vspuschka;
    [SerializeField] GameObject _vspuschka_prefab;

    int coef_coin = 1;
    private int uron = 1;

    bool web_mobile = false;

    void Start()
    {
#if UNITY_WEBGL
        if (Yandex.YandexInstance) { web_mobile = Yandex.YandexInstance.web_mobile; }
#endif
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
           
#if UNITY_WEBGL
            if (web_mobile) { }
            else { vustrel(); }
#else
    vustrel();
#endif


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
                coef_coin = coef_coin +2;
            }
            //При попадании в мишень увеличивай коэфф баллов
            else if (hit[i].collider.gameObject.GetComponent<csMischen>())
            {
                _mischen = hit[i].collider.gameObject.GetComponent<csMischen>();
                coef_coin ++;
            }

            if (hit[i].collider.gameObject.GetComponent<csBomba>())
            {
                _bomba = hit[i].collider.gameObject.GetComponent<csBomba>();

                _gameManager.HP(-1);

            }
            if (hit[i].collider.gameObject.GetComponent<CsBallu>())
            {
                ballu = hit[i].collider.gameObject.GetComponent<CsBallu>();
                _gameManager.Coin(ballu.get_ballu() * coef_coin);
            }

            if (hit[i].collider.gameObject.GetComponent<csVsruv>())
            {
                _vsruv = hit[i].collider.gameObject.GetComponent<csVsruv>();
                _vsruv.Vsruv();
            }
            if (hit[i].collider.gameObject.GetComponent<CsDirigible>())
            {
                dirigible = hit[i].collider.gameObject.GetComponent<CsDirigible>();
                _gameManager.Coin(dirigible.Damage(uron) * coef_coin);
            }
        }
    }
}
