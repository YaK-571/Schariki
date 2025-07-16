using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSchar : MonoBehaviour
{
    [SerializeField] private float speed_default;
    [SerializeField] private float speed_default_min;
    [SerializeField] private float speed_default_max;
    [SerializeField] private float speed_uskorenie;
    Vector2 napravlenie = Vector2.zero;
    Rigidbody2D schar;
    bool uvelischenie_skorosti = true;
    [SerializeField] bool skorost_ustanovlena = false;
    [SerializeField] CsSkorostSvjaski _svjaska;


    private void Start()
    {
        schar = gameObject.GetComponent<Rigidbody2D>();
        napravlenie.y = 1;
        napravlenie.Normalize();

        //��������� ��������
        if (skorost_ustanovlena) //���� �������� ��� ��������� ����� ������ - �� ����� �
        { }
        else
        {
            speed_default = Random.Range(speed_default_min, speed_default_max);
        }
        
        //���� ��� ������ ��� � ������, �� �������� ������ ����� �� ��������
        if(_svjaska) 
        {
            _svjaska.odinakovaja_skorost(speed_default);
        }
    }
    void Update()
    {
        // transform.position += new Vector3(0, speed * Time.deltaTime);
        schar.velocity = speed_default * napravlenie;
    }

    public void skorost()
    {

        if (uvelischenie_skorosti)
        {
            speed_default = speed_default * speed_uskorenie;
            uvelischenie_skorosti = false;
        }
    }

    public void Zamerzanie()
    {
        speed_default = 0.3f;
        speed_uskorenie = 1.2f;
    }

    //���� ���� � ������, �� �� ���� ������ +- ���������� ��������
    public void set_speed_default(float new_speed_svjaska)
    {
        skorost_ustanovlena = true; // ���� ��� ������, �� ���������� ��������� �������� � ������
        speed_default = new_speed_svjaska;
    }
}
