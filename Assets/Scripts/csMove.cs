using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMove : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _telo;
    Vector2 napravlenie = Vector2.zero;
    bool WASD_X = false;
    bool WASD_Y = false;

  

    private Vector3 lastMousePosition; //������� ���� � ������� �����
    private Vector3 mousePos; //� ���� �����



    void Start()
    {

    }

    void Update()
    {
        WASD();

        _telo.velocity = napravlenie * _speed; //time.DeltaTime �� �����! �.�. �������� ����� ������ ����������
        napravlenie.x = 0;
        napravlenie.y = 0;

        prizel_mouse();
      }

    

    public void WASD()
    {
        //�������� WASD
        if (Input.GetKey(KeyCode.W))
        {
            napravlenie.y = 1;
            WASD_X = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            napravlenie.y = -1;
            WASD_X = true;
        }
        else
        {
            WASD_X = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            napravlenie.x = 1;
            WASD_Y = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            napravlenie.x = -1;
            WASD_Y = true;
        }
        else
        {
            WASD_Y = false;
        }
        if ((WASD_Y || WASD_X) && napravlenie.magnitude > 0.01f)
        { //����������, ���� ������ ������. ��� �������� 0 ������������� ������
          //������������ �����, ����� ���� � ����� �� ��� ������ �����, �� �������� �� ��������� �� ���������� ������ ����������
            napravlenie.Normalize();
        }
    }
    public void Set_Napravlenie_stik(Vector2 a)
    {

        napravlenie = a;
    }

    //������������ �����, ������ ���� ��� � ������ � ���� �� ������������ ������ ������� �����
    public void prizel_mouse()
    {
        mousePos = Input.mousePosition;
        // ���������, ��� ���� ������ ���� ����
        bool isMouseInside = mousePos.x >= 0 && mousePos.x <= Screen.width &&
                             mousePos.y >= 0 && mousePos.y <= Screen.height;

        // ���������, ��������� �� ���� (���������� � ������� ��������)
        bool isMouseMoved = (mousePos != lastMousePosition);

        if (isMouseInside && isMouseMoved)
        {
            Vector2 mouseScreenPosition = Input.mousePosition;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            gameObject.transform.position = mouseWorldPosition;
        }
        lastMousePosition = mousePos;//��������� ��������� ������� ����
    }

}


