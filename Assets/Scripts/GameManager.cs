using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _coin;
    [SerializeField] private int _hp;
    [SerializeField] GameObject _Canvas_GameOver;
    [SerializeField] GameObject _Canvas_Finish;
    [SerializeField] csUI_HP _ui_hp;
    [SerializeField] TextMeshProUGUI _ui_coin;


    [SerializeField] bool _timer;
    [SerializeField] float _vremja;
    

    //��� ��������� ����
    [SerializeField] GameObject schit;
    bool schit_actyven = false;

    [SerializeField] GameObject progress;

    public void HP(int uron)
    {
        if (schit_actyven==false)
        {
            _hp = _hp + uron;
            if (_hp <= 0)
            {
                _Canvas_GameOver.SetActive(true);
            }
            //������ ����� ��������
            _ui_hp.Update_HP(_hp);
        }
    }

    public void Coin(int coin)
    {
        _coin += coin;
        _ui_coin.text = _coin.ToString(); //��������� � ������
    }

    private void Start()
    {
        if(_timer)
        StartCoroutine(TimerFinish());

        SpawnGameInstanse();
        
        _ui_hp.Update_HP(_hp);

    }

    private IEnumerator TimerFinish()
    {
        yield return new WaitForSeconds(_vremja);
        _Canvas_Finish.SetActive(true);
    }

    public void activazija_schita()
    {
        schit_actyven = true;
        schit.SetActive(true);
        StartCoroutine(vremja_schita());
    }

    private IEnumerator vremja_schita()
    {
        yield return new WaitForSeconds(5f);
        schit_actyven = false;
        schit.SetActive(false);
    }

    public void Save()
    {
        Progress.GameInstance.Save(_coin);
    }

   
    //���� ������ �� � �������� ����, � ����� �� �����, �� ��������� ������
    //��������� � ���, ��� ����������� ������ �������� ���� �������
    //��� ���������� �� ����� ������� ����, �� ��������� �� ����� ����������, ����� ��������� ������ �����
    //����� � �� ����, ��������� ������
    //����� ����������� ��� �������, �� ����� ������� ������ ��� ��� ������� ������ �������
    //� ������ ������ � �������� ��������� ��� ������ �� �� ���������� ��-�� ������������ ������ ���� �������
    public void SpawnGameInstanse()
    {
        if(Progress.GameInstance)
        {

        }
        else
        {
            
           Instantiate(progress,Vector3.zero, Quaternion.identity);
         
        }
    }
}
