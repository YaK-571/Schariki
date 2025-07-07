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

    [SerializeField] csBomba _bomba;
    [SerializeField] csVsruv _vsruv;
    [SerializeField] csMischen _mischen;

    // [SerializeField] GameObject _canvas_game_over;
    [SerializeField] GameManager _gameManager;
    [SerializeField] csVspuschka _vspuschka;
    [SerializeField] GameObject _vspuschka_prefab;

    private Vector3 lastMousePosition; //������� ���� � ������� �����
    private Vector3 mousePos; //� ���� �����



    int coef_coin = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WASD();

        _telo.velocity = napravlenie * _speed; //time.DeltaTime �� �����! �.�. �������� ����� ������ ����������
        napravlenie.x = 0;
        napravlenie.y = 0;

        prizel_mouse();

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            vustrel();
        }


    }

    public void vustrel()
    {
        //�������� ������� � ������ �����
        //_vspuschka.teleport(transform.position);
        GameObject vspuschka_spawn = Instantiate(_vspuschka_prefab);
        vspuschka_spawn.transform.position = transform.position;

        //������� �����, ���������� ����� ��������� ��������
        RaycastHit2D[] hit = Physics2D.RaycastAll(gameObject.transform.position, Vector2.zero);
        //�������� �������� �� � ��� ��� �����
        for (int i = 0; i < hit.Length; i++)
        {


            //��� ��������� � ����� ������ ����� �������������
            if (hit[i].collider.gameObject.GetComponent<csMischen_zentr>())
            {
                coef_coin = coef_coin * 2;
            }
            //��� ��������� � ������ ���������� ����� ������
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


