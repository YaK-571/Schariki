using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class csVustrel : MonoBehaviour
{
    [SerializeField] bool _glavnoe_menu;

    [SerializeField] csBomba _bomba;
    [SerializeField] CsVsruv_new _vsruv;
    [SerializeField] csMischen _mischen;
    [SerializeField] CsDirigible dirigible;
    [SerializeField] CsBallu ballu;

    // [SerializeField] GameObject _canvas_game_over;
    [SerializeField] GameManager _gameManager;
    //[SerializeField] csVspuschka _vspuschka;
    [SerializeField] GameObject _vspuschka_prefab;
    [SerializeField] GameObject _VFX_ballu;



    [SerializeField] AudioSource _zvuk_vustrela;



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


        //_vspuschka.teleport(transform.position);//телепорт вспышки в нужное место
        GameObject vspuschka_spawn = Instantiate(_vspuschka_prefab);
        vspuschka_spawn.transform.position = transform.position;

        //выстрел лучём, проходящим через множество обьектов
        RaycastHit2D[] hit = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero);


        // _zvuk_vustrela.Play();

        GameObject _VFX_ballu_spawn = null;
        //проверка попалили мы в шар или бомбу
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.isTrigger) { }
            else { continue; }
            //ЕСЛИ это коллизия триггер, ТО цикл продолжится, ИНАЧЕ - пропустит оборот. continue - тоже самое, что Next в Клик Сенс
            //Проблема: на шаре 2 коллизии:
            //1 - триггер
            //2 обычный коллайдер
            //из-за этого очки начисляются два раза, до уничтожения шара

            if (hit[i].collider.gameObject.GetComponent<CsNitka>())
            {
                if (hit[i].collider.gameObject.GetComponent<CsNitka>().sbiv_gruza())
                { Progress.GameInstance.sbito_gruza_na_nitke(); }
            }

            //При попадании в мишень увеличивай коэфф баллов (на центре мишени такая же)
            if (hit[i].collider.gameObject.GetComponent<csMischen>())
            {
                _mischen = hit[i].collider.gameObject.GetComponent<csMischen>();
                coef_coin++;

                if (_VFX_ballu_spawn) { Progress.GameInstance.popadanij_v_zentr(); }
                else
                {
                    _VFX_ballu_spawn = Instantiate(_VFX_ballu);
                }
                //если баллы уже вывелись, но игрок попал в центр мишени, и цифферки спавнятся второй раз
                //но не спавнь их, а измени значение в тех, которые уже заспавнились
                _VFX_ballu_spawn.GetComponent<Cs_VFX_ballu>().VFX_ballu("X" + coef_coin);
                _VFX_ballu_spawn.transform.position = transform.position;



            }

            if (hit[i].collider.gameObject.GetComponent<csBomba>())
            {
                _bomba = hit[i].collider.gameObject.GetComponent<csBomba>();
                _bomba._vzruv();
                if (_glavnoe_menu) { }
                else
                {
                    _gameManager.HP(-1);
                }

            }
            if (hit[i].collider.gameObject.GetComponent<CsBallu>())
            {
                ballu = hit[i].collider.gameObject.GetComponent<CsBallu>();
                if (_glavnoe_menu) { }
                else
                {
                    _gameManager.Coin(ballu.get_ballu() * coef_coin);

                    _VFX_ballu_spawn = Instantiate(_VFX_ballu);
                    _VFX_ballu_spawn.GetComponent<Cs_VFX_ballu>().VFX_ballu("+" + ballu.get_ballu() * coef_coin);
                    _VFX_ballu_spawn.transform.position = transform.position;

                }

            }

            if (hit[i].collider.gameObject.GetComponent<CsVsruv_new>())
            {
                // _vsruv = hit[i].collider.gameObject.GetComponent<csVsruv>();
                // _vsruv.Vsruv();
                _vsruv = hit[i].collider.gameObject.GetComponent<CsVsruv_new>();
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
