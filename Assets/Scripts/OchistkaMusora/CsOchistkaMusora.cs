using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOchistkaMusora : MonoBehaviour
{
    [SerializeField] CsOchistkaMusora roditel;
    private bool verh;

    //������� �������� ����/��������
    //���� � ���� ������� ������ ����������� ������������ ������, �� ������� ���

    //� ���� ����� ������� ������, �� ������� ����� �� �������, ��� ������� � ������ ����� ��������
    public void Delete(bool verh_ = false)
    {

        verh = verh_;

        if (roditel)
        {
            roditel.UdaleniePustogoRoditelja(verh);
        }

        Destroy(gameObject);

    }

    public void UdaleniePustogoRoditelja(bool verh_)
    {
        verh = verh_;
        if (verh)
        {
            Delete(verh);
        }
        else if (transform.childCount <= 1)
        {
            Delete(verh);
        }
    }

}
