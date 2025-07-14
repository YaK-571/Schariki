using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class csGraniza : MonoBehaviour
{

    [SerializeField] RectTransform _yakor_UI;

    Vector3 yakor_position_UI;

    Vector3 yakor_WorldPosition;
    void Start()
    {
        StartCoroutine(AutoPosizion());
        
    }

    IEnumerator AutoPosizion()
    {
        while (true)
        { 
            //������ ���, ����� ������� ������������ ����� � �� ������ ������� �� ������� ������
            //�������� ���������� ������� �� �������
            yakor_position_UI = RectTransformUtility.WorldToScreenPoint(null, _yakor_UI.position);
            //����������� ���������� ������� � ������� ����������
            yakor_WorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(yakor_position_UI.x, yakor_position_UI.y, 0.0f));
            yakor_WorldPosition.z = 0.0f;
            gameObject.transform.position = yakor_WorldPosition;
            yield return new WaitForSeconds(1f);
        }
        
    }
}
