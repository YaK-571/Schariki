using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CsSpawnScharow : MonoBehaviour
{
    //��� �������
    [SerializeField] RectTransform _levo_UI;
    [SerializeField] RectTransform _pravo_UI;
    Vector3 _levo_position_UI;
    Vector3 _pravo_position_UI;
    Vector3 levo;
    Vector3 pravo;
    float size;

    //��� ������ �����
    [SerializeField] GameObject _schar;
    Vector3 ScharPosition= Vector3.zero;
    void Start()
    {
        StartCoroutine(SpawnScharow());
    }


    IEnumerator SpawnScharow()
    {
        while (true)
        { 
            //-----------------����������� ������ ������� �������� ������---------------

            //�������� ���������� ������� �� �������
            _levo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _levo_UI.position);
            _pravo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _pravo_UI.position);
            //����������� ���������� ������� � ������� ����������
            levo = Camera.main.ScreenToWorldPoint(new Vector3(_levo_position_UI.x, _levo_position_UI.y, 0.0f));
            pravo = Camera.main.ScreenToWorldPoint(new Vector3(_pravo_position_UI.x, _pravo_position_UI.y, 0.0f));
            //��� z==0 ��� ������� ������
            //levo.z = 0.0f; //�����, ����� ������ �� ������ �� ������ ����

            size = (pravo.x - levo.x) * 0.9f; //�����, ����� �� �� ������� ������ �� ����� ������� ��� ������������ ������
                                              // Debug.Log(pravo.y + " "+ levo.y);
            gameObject.transform.localScale = new Vector3(size, transform.localScale.y, 0);
            // gameObject.transform.position = pravo;


            //---------------����� �������----------------------
            //��������� ����� � �������� ��������
            ScharPosition = new Vector3(Random.Range(0, size) - size / 2, transform.position.y, 0f);
            Instantiate(_schar, ScharPosition, Quaternion.identity); //��� Quaternion.identity - ��������� �������
            yield return new WaitForSeconds(1f);//���������� ��������� ����� �������
        }
    }
}
