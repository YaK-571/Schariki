using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CsWASD : MonoBehaviour
{
    [SerializeField] csMove _csMove;
    Vector2 napravlenie = Vector2.zero;
    private Vector3 lastMousePosition; //������� ���� � ������� �����
    private Vector3 mousePos; //� ���� �����

    bool web_mobile = false;

    void Start()
    {
#if UNITY_WEBGL
        if (Yandex.YandexInstance) { web_mobile = Yandex.YandexInstance.web_mobile; }
#endif
    }

    void Update()
    {
        napravlenie.y = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) napravlenie.y = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) napravlenie.y = -1;

        napravlenie.x = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) napravlenie.x = 1;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) napravlenie.x = -1;


        if (napravlenie.magnitude > 0.01f)
        { //����������, ���� ������ ������. ��� �������� 0 ������������� ������
          //������������ �����, ����� ���� � ����� �� ��� ������ �����, �� �������� �� ��������� �� ���������� ������ ����������
            napravlenie.Normalize();
        }
        _csMove.Set_Napravlenie_WASD(napravlenie);

        

#if UNITY_WEBGL
        if (web_mobile) { }
        else { prizel_mouse(); }
#else
        prizel_mouse();
#endif



    }

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
