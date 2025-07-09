using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csMenu : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _self_UI;
    [SerializeField] bool _save; //����� �� ��������� ���� ��� ������� ����� �������. ��� ���� ����� � ������
    // Start is called before the first frame update
    void Start()
    {
        //�����
        Time.timeScale = 0f;

        //���������� ��� ������ � ���� �����
        if(_save)
        {
            _gameManager.Save();
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
        //������ �����
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void aptetschka()
    {
        
        _gameManager.HP(3);
        Play();
    }
}
