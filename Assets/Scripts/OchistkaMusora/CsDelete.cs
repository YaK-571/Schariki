using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsDelete : MonoBehaviour
{
    [SerializeField] bool verh = false;
    //���� ��� ����� ����� � �� ��������� �� ������� ������, �� ����� ������ � ���� ����� �������
    //��� ������� � ������� � ������ ����������
    //�� ����� ����� �� ����, ������� ���� ������� ���� ������ �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CsOchistkaMusora>())
        {
           
            collision.GetComponent<CsOchistkaMusora>().Delete(verh);
        }

    }
}
