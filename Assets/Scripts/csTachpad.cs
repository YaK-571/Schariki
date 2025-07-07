using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTachpad : MonoBehaviour
{
    [SerializeField] csMove target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //��� 0 - ������ ������� �� �����, ���� �� ��������� ������������

            if (touch.phase == TouchPhase.Began)  // ������� ��������, Began - ������ �����
            {
                Vector2 touchPosition = touch.position; // ������� � �������� �� ������
                Vector2 worldPoint2D = Camera.main.ScreenToWorldPoint(touchPosition); //������������� � ������� ����������

                gameObject.transform.position = worldPoint2D; //����������� �������
                target.vustrel(); //�������
            }
        }
    }
}
