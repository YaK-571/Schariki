using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class csVsruv : MonoBehaviour
{

    [SerializeField] csSchar _schar;
    [SerializeField] GameObject _Square;
    [SerializeField] public bool _popadanie_v_schar;
    [SerializeField] GameObject component_vsruv;


    [SerializeField] HingeJoint2D _szepka;
    [SerializeField] csUskorenie_svjaski _svjaska;
    [SerializeField] AudioSource _zvuk_lopanie;

    public void Vsruv()
    {
        if (_zvuk_lopanie) {
            _zvuk_lopanie.Play(); }


        //���� � ��� ��� ���� � �� ���������� ��������, �� ���������� ��� � �������� ���

        if (_szepka && _schar)
        {
            //��������� ������ ��� �����
            _schar.skorost();
            _szepka.enabled = false;
        }

        if (_popadanie_v_schar) //������������ ������ ����, ����� ���������� ��� ����� ����� ������

        {
            _schar.enabled = false;
        }
        //���� �� ����� ���� � ������, �� � ���� ��������
        if (_svjaska)
        {
            _svjaska.Uskorenie();
        }

        // Destroy(_Square);//������� �������� ����
        //�������� ������� �� �������� �� ������
        //������ ����� ��������� �������� ���� �� � ����� ������� ������������ � ���� �� � �� ��� �������
        //���� ���, �� ������������ ����������� ������ ���� ����� �������
        if (component_vsruv.GetComponent<CsOchistkaMusora>())
        {
            component_vsruv.GetComponent<CsOchistkaMusora>().Delete();
        }
        else
        {
            Destroy(component_vsruv);
        }

    }


}