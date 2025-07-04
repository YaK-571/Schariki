using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




//���� ����� ����������� ���������� IDragHandler � �� ����� ������ ������
//����� CsStikNew �� ��������� ���� ���������� IDragHandler
//�� ������ ����� �����-�� ������� ��� ����� ���������� IDragHandler
//� ��� ������ public void OnDrag(PointerEventData eventData)
public class CsStikNew : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] csMove target;

    [SerializeField] Image stik1;
    [SerializeField] Image stik2_graniza;
    private float radius_ogranichenie = 0f;

    bool knopka_naschata = false;

    Vector2 StartPosition;
    Vector2 StikPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = stik1.rectTransform.localPosition;
        StikPosition = StartPosition;
        // ������ ����� � �������� ������ ����� ���� (�������������� ������� ����)
        radius_ogranichenie = stik2_graniza.rectTransform.rect.width / 2f;

    }

    // Update is called once per frame
    void Update()
    {
        if(knopka_naschata)
        {
            target.Set_Napravlenie_stik((StikPosition - StartPosition)/ radius_ogranichenie);
        }
    }

    //���������� ����� �� ����� ������� �� ������
    public void OnDrag(PointerEventData eventData)
    {
        //������� � if ����������� ����� ������� �� ����� � ����������. +- ���. ���-�� ��� ������ ������� ���������
        //1 - �������� stik2_graniza.rectTransform - � ����� ������� ������� ������������� (������� �������� �����)
        //2 - eventData.position - ������� ������ ����
        //3 - ��� ������ ���� ������, ��������� � ������ ������. �� ������ ������ �� �����
        //4 - out StikPosition - ��������� ����������, ������� �� ������ ��������
        //��� out - �������� ����� ��� �������� ���������� �� ������
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(stik2_graniza.rectTransform, eventData.position, null, out StikPosition))
        {

            if ((StikPosition - StartPosition).magnitude > radius_ogranichenie)
            //�������� ��������� ������ ���� �� ������ ��������
            {
                StikPosition = (StikPosition - StartPosition).normalized * radius_ogranichenie + StartPosition;
                //����������� ����������� ����� ������
                //���� �������� ��������� ������ ������� ������ ����, ����� ����� �� ����, ��������� ������� �������� + StartPosition
            }

            stik1.rectTransform.localPosition = StikPosition; //����������� �� �����

        }

    }

    //���������� ��� ������� �� �����.
    public void OnPointerDown(PointerEventData eventData)
    {
        knopka_naschata = true;
    }

    //���������� ��� ���������� ������. �������� ������ ��� ���������� OnPointerDown
    public void OnPointerUp(PointerEventData eventData)
    {
        stik1.rectTransform.localPosition = StartPosition; //���������� ���� � �����
        knopka_naschata = false;

        StikPosition = StartPosition;
        //��� ����� ��������� ��������� ���
        //���� ������ �� ���� �� ������ �� ��������
        //�� ������ ������� � �� ������� � ��� ���������
        //����� ���� ��������� ��� ��� ��������� �����
    }
}
