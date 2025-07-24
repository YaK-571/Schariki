using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[System.Serializable] //�������� ��������, ��� ��� �� SerializeField
public struct Scharu
{
    [Tooltip("������ ���� ��� ������")]
    [SerializeField] public GameObject prefab;
    [Tooltip("���������� ����� ����� ����")]
    [SerializeField] public int kolichestvo;
    private int kolichestvo_niz;//����� ��� �������, ����� ����� ������� ����� �������� ���� �� ����� ����
    private int kolichestvo_verh;
    public int get_niz()
    { return kolichestvo_niz; }
    public int get_verh()
    { return kolichestvo_verh; }
    public void set_niz(int a)
    { kolichestvo_niz = a; }
    public void set_verh(int a)
    { kolichestvo_verh = a; }
}

public class CsSpawnScharow : MonoBehaviour
{

    [SerializeField] float skorost;
    //��� �������
    [SerializeField] RectTransform _levo_UI;
    [SerializeField] RectTransform _pravo_UI;
    Vector3 _levo_position_UI;
    Vector3 _pravo_position_UI;
    Vector3 levo;
    Vector3 pravo;
    float size_spawner;

    //��� ������ �����

    Vector3 ScharPosition = Vector3.zero;
    [SerializeField] public Scharu[] _scharu;
    private int size_array_scharu = 0;
    private int typ_spawn = 0;
    public int sum_scharov = 0;
    private int sum_scharov_max = 0;
    //[SerializeField] GameObject _debug_odin_schar;

    void Start()
    {
        size_array_scharu = _scharu.Length; //�������� ���������� ����� ������ �����

        //������� ������� ����� ����� ���������� �� ������� �����
        //����� ��� ���������� ������ ����
        //������� ������ ����� 9 ����� ����� ������ ������������, ���� �� �����, ����� ���� ���������� � �������� ���������
        //�������� ������� ����� ���� ������ � 10 ���.
        //������� ����� ��� ��������� ��������� �������������� �� �� ���������� ����� ����� ����� �� ����
        //� �� �� ����� ����� � ������ ����
        int i = 0;

        while (i < size_array_scharu)
        {
            _scharu[i].set_niz(sum_scharov);
            sum_scharov = sum_scharov + _scharu[i].kolichestvo;
            _scharu[i].set_verh(sum_scharov);
            i++;
        }
        sum_scharov_max = sum_scharov;
        StartCoroutine(SpawnScharow());
    }


    IEnumerator SpawnScharow()
    {
        while (true)
        {
            //-----------------����������� ������ ������� �������� ������---------------

            //�������� ���������� ������� �� �������
            _levo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _levo_UI.position);
            _pravo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _pravo_UI.position);
            //����������� ���������� ������� � ������� ����������
            levo = Camera.main.ScreenToWorldPoint(new Vector3(_levo_position_UI.x, _levo_position_UI.y, 0.0f));
            pravo = Camera.main.ScreenToWorldPoint(new Vector3(_pravo_position_UI.x, _pravo_position_UI.y, 0.0f));
            //��� z==0 ��� ������� ������
            //levo.z = 0.0f; //�����, ����� ������ �� ������ �� ������ ����

            size_spawner = (pravo.x - levo.x) * 0.9f; //�����, ����� �� �� ������� ������ �� ����� ������� ��� ������������ ������
                                                      // Debug.Log(pravo.y + " "+ levo.y);
            gameObject.transform.localScale = new Vector3(size_spawner, transform.localScale.y, 0);
            // gameObject.transform.position = pravo;


            /*
            //---------------������� ����� ������----------------------
            ScharPosition = RandomPosition();
            Instantiate(_debug_odin_schar, ScharPosition, Quaternion.identity); //��� Quaternion.identity - ��������� �������
            */


            //---------------����� �������----------------------

            ScharPosition = RandomPosition();
            //���� ���� ��� ����, �� ��������
            if (sum_scharov > 0)
            {
                typ_spawn = Random_Typ_Schara();
                Instantiate(_scharu[typ_spawn].prefab, ScharPosition, Quaternion.identity); //��� Quaternion.identity - ��������� �������
                sum_scharov--;
                _scharu[typ_spawn].kolichestvo--;
            }




            yield return new WaitForSeconds(skorost);//���������� ��������� ����� �������
        }
    }

    private Vector3 RandomPosition()
    {
        //��������� ��������� ����� � �������� ������ ��� ����� ������ ������
        return new Vector3(Random.Range(0, size_spawner) - size_spawner / 2, transform.position.y, 0f);
    }

    private int Random_Typ_Schara()
    {
        int typ = 0;
        bool sdvig_typ = false; //�������� �� ��� ���, ������� ������������ ������ ��� ���� ��������� ���� �� �������� �����
        int a = Random.Range(0, sum_scharov_max);

        int i = 0;
        
        //���������� ��� � ������� ����� �������� �����
        while (i < size_array_scharu)
        {
            if ((a >= _scharu[i].get_niz()) && (a < _scharu[i].get_verh()))
            {
                typ = i;
                break;//���� ������ ��� ������ - ������ �� ����������
            }
            i++;
        }

        //�� ���� ���������� ���������� �������������� ����� == 0, �� ������� �� ���� ���, � ���������
        if (_scharu[typ].kolichestvo == 0)
        { sdvig_typ = true; }

        if (sdvig_typ)
        {
            
            int nomer_oborota= 0;
            while (nomer_oborota < size_array_scharu)
            {
                nomer_oborota++;
                typ++;
                //���� ���� ��������� �� ����� - ����� ������ �������
                if (typ >= size_array_scharu)  typ = 0;
                if (_scharu[typ].kolichestvo > 0) break;

            }
        }

            return typ;
    }

}
