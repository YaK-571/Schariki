using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGraniza_nischnjaja : MonoBehaviour
{
    [SerializeField] bool spawn_vspuschek = false;
    [SerializeField] List<csVsruv> _scharu_artobstrel = new List<csVsruv>();//список для удаления шаров
    [SerializeField] List<csSchar> _scharu_zamerzanie = new List<csSchar>();//список для замедления шаров
    int dlina_list1 = 0;
    int dlina_list2 = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<csVsruv>())
        {
            if (collision.gameObject.GetComponent<csVsruv>()._popadanie_v_schar)
            {
                //если работает артобстрел, взорви подлетающий шарик сразу
                if (spawn_vspuschek)
                {
                    collision.gameObject.GetComponent<csVsruv>().Vsruv();
                }
                else
                {
                    //запомни пролетающие шарики
                    _scharu_artobstrel.Add(collision.gameObject.GetComponent<csVsruv>());
                   
                    //обратись к родителю и сразу запиши ссылку на шар и для замедления
                    if(collision.transform.parent.GetComponent<csSchar>())
                    {
                        _scharu_zamerzanie.Add(collision.transform.parent.GetComponent<csSchar>());
                    }

                }
            }
        }
        

    }

    //взрыв шариков
    public void artillerija_vzruv_scharov()
    {
        //получи длину списка
        dlina_list1 = _scharu_artobstrel.Count;

        //взорви все шарики, которые ещё не взорваны
        for (int i = 0; i < dlina_list1; i++)
        {
            if (_scharu_artobstrel[i])
            {
                _scharu_artobstrel[i].Vsruv();
            }
        }
        //очистка массива
        _scharu_artobstrel.Clear();
    }
    public void Zamerzanie()
    {
        dlina_list2 = _scharu_zamerzanie.Count;
        for (int i = 0; i < dlina_list2; i++)
        {
            if (_scharu_zamerzanie[i])
            {
                _scharu_zamerzanie[i].Zamerzanie();
            }
        }
        _scharu_zamerzanie.Clear();
    }

    public void Set_artobstrel(bool a)
    {
        spawn_vspuschek = a;
    }
}
