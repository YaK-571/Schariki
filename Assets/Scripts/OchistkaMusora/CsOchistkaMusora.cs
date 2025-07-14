using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOchistkaMusora : MonoBehaviour
{
    [SerializeField] CsOchistkaMusora roditel;
    
    //������� �������� ����/��������
    //���� � ���� ������� ������ ����������� ������������ ������, �� ������� ���
    public void Delete()
    {
        if(roditel)
        {
            roditel.UdaleniePustogoRoditelja();
        }
        Destroy(gameObject);
    }

    public void UdaleniePustogoRoditelja()
    {
        if(transform.childCount <= 1)
        {
            Delete();
        }
    }


}
