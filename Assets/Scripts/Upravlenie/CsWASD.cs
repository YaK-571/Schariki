using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsWASD : MonoBehaviour
{
    [SerializeField] csMove _csMove;
    Vector2 napravlenie = Vector2.zero;
    private Vector3 lastMousePosition; //������� ���� � ������� �����
    private Vector3 mousePos; //� ���� �����

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) napravlenie.y = 1;
        else if (Input.GetKey(KeyCode.S)) napravlenie.y = -1;
        else napravlenie.y = 0;
        if (Input.GetKey(KeyCode.D)) napravlenie.x = 1;
        else if (Input.GetKey(KeyCode.A)) napravlenie.x = -1;
        else napravlenie.x = 0;


        if (napravlenie.magnitude > 0.01f)
        { //����������, ���� ������ ������. ��� �������� 0 ������������� ������
          //������������ �����, ����� ���� � ����� �� ��� ������ �����, �� �������� �� ��������� �� ���������� ������ ����������
            napravlenie.Normalize();
        }
        _csMove.Set_Napravlenie_WASD(napravlenie);

        prizel_mouse();
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
