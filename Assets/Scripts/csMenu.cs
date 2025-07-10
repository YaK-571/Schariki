using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csMenu : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _self_UI;
    [SerializeField] bool _save; //����� �� ��������� ���� ��� ������� ����� �������. ��� ���� ����� � ������

    int tschislo_aptetschka = 0;

    void Start()
    {
        //�����
        Time.timeScale = 0f;

        //���������� ��� ������ � ���� �����
        if (_save)
        {
            _gameManager.Save();
            var date = Progress.GameInstance.date;
            tschislo_aptetschka = date.aptetschka;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        //������ �����
        Time.timeScale = 1f;
        //��������� ������ �� �������� ����� � � ������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Play()
    {
        //������ �����
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void Glavnie_menu()
    {
        Progress.GameInstance.set_nomer_lvl(0);
        //������ �����
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
 
    public void Next_lvl()
    {
        //������ �����
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void aptetschka()
    {
        if (tschislo_aptetschka >= 1)
        {
            tschislo_aptetschka--;
            Progress.GameInstance.Usilenie(5);
            _gameManager.HP(3);
            Play();
        }
    }
}
