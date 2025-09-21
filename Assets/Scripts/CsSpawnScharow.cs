using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

//������� ��������������� CTRL+K CTRL+/
/*
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


}*/

public class CsSpawnScharow : MonoBehaviour
{
    [SerializeField] bool start_spawner;
    //������������ �� ���� ������� ��� ��������������� ������� ������ ����� ��� ��� ������� �������� ���������
    [SerializeField] float skorost;

    [Header("���� �����")]
    [SerializeField] public GameObject[] prefab;
    [SerializeField] public int[] kolichestvo_start;
    [SerializeField] public int[] kolichestvo_ostalos;
    private int[] kolichestvo_niz;//����� ��� �������, ����� ����� ������� ����� �������� ���� �� ����� ����
    private int[] kolichestvo_verh;

    [Header("��� �����������")]
    [SerializeField] RectTransform _levo_UI;
    [SerializeField] RectTransform _pravo_UI;
    Vector3 _levo_position_UI;
    Vector3 _pravo_position_UI;
    Vector3 levo;
    Vector3 pravo;
    float size_spawner;

    //��� ������ �����

    Vector3 ScharPosition = Vector3.zero;
    private int size_array_scharu = 0;
    private int typ_spawn = 0;
    private int sum_scharov_ostalos = 0;
    private int sum_scharov_max = 0;

    private void Awake()
    {
        size_array_scharu = prefab.Length; //�������� ���������� ����� ������ �����

        //��������� ������� ������� �������. ����� ������� �����, �.�. �������� ������ ������� ������
        kolichestvo_ostalos = new int[size_array_scharu];
        kolichestvo_niz = new int[size_array_scharu];
        kolichestvo_verh = new int[size_array_scharu];

    }
    void Start()
    {
        if (start_spawner) Start_spawna(kolichestvo_start, skorost);
    }

    public void Start_spawna(int[] kolichestvo_start, float new_skorost)
    {
        //������� � c# - ��� ������
        //���� �������� ������������ ������, ����� �� ����� �������� �������� ������ � ���������
        kolichestvo_ostalos = (int[])kolichestvo_start.Clone();
        
        skorost = new_skorost;


        //������� ������� ����� ����� ���������� �� ������� �����
        //����� ��� ���������� ������ ����
        //������� ������ ����� 9 ����� ����� ������ ������������, ���� �� �����, ����� ���� ���������� � �������� ���������
        //�������� ������� ����� ���� ������ � 10 ���.
        //������� ����� ��� ��������� ��������� �������������� �� �� ���������� ����� ����� ����� �� ����
        //� �� �� ����� ����� � ������ ����
        sum_scharov_max = 0;
        int i = 0;
        while (i < size_array_scharu)
        {
            kolichestvo_niz[i] = sum_scharov_max;
            sum_scharov_max = sum_scharov_max + kolichestvo_start[i];
            kolichestvo_verh[i] = sum_scharov_max;
            i++;
        }
        sum_scharov_ostalos = sum_scharov_max;
        StartCoroutine(SpawnScharow());
    }

    IEnumerator SpawnScharow()
    {
        bool spawn_okonchen = true;
        while (spawn_okonchen)
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

            //---------------����� �������----------------------

            ScharPosition = RandomPosition();
            //���� ���� ��� ����, �� ��������
            if (sum_scharov_ostalos > 0)
            {
                typ_spawn = Random_Typ_Schara();
                Instantiate(prefab[typ_spawn], ScharPosition, Quaternion.identity); //��� Quaternion.identity - ��������� �������
                sum_scharov_ostalos--;
                kolichestvo_ostalos[typ_spawn]--;
            }
            else
            { spawn_okonchen = false;
            if(start_spawner)
                { Start_spawna(kolichestvo_start, skorost); }//����������, ���� ���� ���������
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
            if ((a >= kolichestvo_niz[i]) && (a < kolichestvo_verh[i]))
            {
                typ = i;
                break;//���� ������ ��� ������ - ������ �� ����������
            }
            i++;
        }

        //�� ���� ���������� ���������� �������������� ����� == 0, �� ������� �� ���� ���, � ���������
        if (kolichestvo_ostalos[typ] == 0)
        { sdvig_typ = true; }

        if (sdvig_typ)
        {
            int nomer_oborota = 0;
            while (nomer_oborota < size_array_scharu)
            {
                nomer_oborota++;
                typ++;
                //���� ���� ��������� �� ����� - ����� ������ �������
                if (typ >= size_array_scharu) typ = 0;
                if (kolichestvo_ostalos[typ] > 0) break;
            }
        }

        return typ;
    }

}
