using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGraniza_nischnjaja : MonoBehaviour
{
    [SerializeField] bool spawn_vspuschek = false;
    [SerializeField] List<csVsruv> _scharu_artobstrel = new List<csVsruv>();//������ ��� �������� �����
    [SerializeField] List<csSchar> _scharu_zamerzanie = new List<csSchar>();//������ ��� ���������� �����
    int dlina_list1 = 0;
    int dlina_list2 = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<csVsruv>())
        {
            if (collision.gameObject.GetComponent<csVsruv>()._popadanie_v_schar)
            {
                //���� �������� ����������, ������ ����������� ����� �����
                if (spawn_vspuschek)
                {
                    collision.gameObject.GetComponent<csVsruv>().Vsruv();
                }
                else
                {
                    //������� ����������� ������
                    _scharu_artobstrel.Add(collision.gameObject.GetComponent<csVsruv>());
                   
                    //�������� � �������� � ����� ������ ������ �� ��� � ��� ����������
                    if(collision.transform.parent.GetComponent<csSchar>())
                    {
                        _scharu_zamerzanie.Add(collision.transform.parent.GetComponent<csSchar>());
                    }

                }
            }
        }
        

    }

    //����� �������
    public void artillerija_vzruv_scharov()
    {
        //������ ����� ������
        dlina_list1 = _scharu_artobstrel.Count;

        //������ ��� ������, ������� ��� �� ��������
        for (int i = 0; i < dlina_list1; i++)
        {
            if (_scharu_artobstrel[i])
            {
                _scharu_artobstrel[i].Vsruv();
            }
        }
        //������� �������
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
