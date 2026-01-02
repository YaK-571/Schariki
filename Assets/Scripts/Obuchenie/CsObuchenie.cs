using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsObuchenie : MonoBehaviour
{
    [SerializeField] GameObject _Cansvas_Obuchenie_self;
    [SerializeField] GameObject _Cansvas_Finish_Obuchenie;

    [SerializeField] float _time_podskazka1 = 3f;
    [SerializeField] float _time_finish = 3f;
    
    [SerializeField] GameObject _podskazka1;
    [SerializeField] GameObject _podskazka2;

    [SerializeField] GameObject _spavner1;
    [SerializeField] GameObject _spavner2;
    [SerializeField] GameObject _spavner3;
    [SerializeField] GameObject _spavner4;

    [SerializeField] GameObject _text1;
    [SerializeField] GameObject _text2;
    [SerializeField] GameObject _text3;
    [SerializeField] GameObject _usilenija_button;
    [SerializeField] GameObject _text4;


    [SerializeField] TextMeshProUGUI _progress_text;

    //прогресс по каждому этапу
    int step1_schar = 0;
    int step2_coin = 0;
    int step3_boom = 0;
    int step4_usilenija = 0;

    //пройден ли каждый этап
    bool step1 = false;
    bool step2 = false;
    bool step3 = false;
    bool step4 = false;
    bool all_step = false;

    // Start is called before the first frame update
    void Start()
    {
        _usilenija_button.SetActive(false);
        _spavner1.SetActive(true);
        _spavner2.SetActive(false);
        _spavner3.SetActive(false);
        _spavner4.SetActive(false);

        _text1.SetActive(true);
        _text2.SetActive(false);
        _text3.SetActive(false);
        _text4.SetActive(false);

        StartCoroutine(Podskazka1());
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnEnable()
    {
        // Подписываемся на событие ВКЛЮЧЕНИЯ объекта
        CsObuchenie_Schar.ObuschenieStep += Step;
    }

    private void OnDisable()
    {
        // Отписываемся при ВЫКЛЮЧЕНИИ объекта
        CsObuchenie_Schar.ObuschenieStep -= Step;
    }

    public void Step(int a)
    {
        if (a == 1)
        {

            step1_schar++;
            _progress_text.text = step1_schar.ToString() + "/3";
            if (step1_schar >= 3)
            {
                step1 = true;
                _spavner1.SetActive(false);
                _spavner2.SetActive(true);
                _spavner3.SetActive(false);
                //           _spavner4.SetActive(false);
                _progress_text.text = "";

                _text1.SetActive(false);
                _text2.SetActive(true);
                _text3.SetActive(false);
                _text4.SetActive(false);
            }
        }
        if (a == 2)
        {
            step2_coin++;
            if (step2_coin >= 1)
            {
                step2 = true;
                _spavner1.SetActive(false);
                _spavner2.SetActive(false);
                _spavner3.SetActive(true);
                _spavner4.SetActive(false);
                _progress_text.text = "0/3";

                _text1.SetActive(false);
                _text2.SetActive(false);
                _text3.SetActive(true);
                _text4.SetActive(false);
            }
        }
        if (a == 3)
        {
            step3_boom++;
            _progress_text.text = step3_boom.ToString() + "/3";
            if (step3_boom >= 3)
            {
                step3 = true;
                _spavner1.SetActive(false);
                _spavner2.SetActive(false);
                _spavner3.SetActive(false);
                _spavner4.SetActive(true);

                _text1.SetActive(false);
                _text2.SetActive(false);
                _text3.SetActive(false);
                _text4.SetActive(true);

                _podskazka2.SetActive(true);
                _progress_text.text = "";
                _usilenija_button.SetActive(true);
            }
        }
        if (a == 4)
        {
            step4_usilenija++;
            ;
            if (step4_usilenija >= 1)
            { step4 = true; }
        }

        if (step1 == true && step2 == true && step3 == true && step4 == true)
        {
            all_step = true;
            StartCoroutine(Finish());
     //       SceneManager.LoadScene("LVL_menu");

        }
    }

    IEnumerator Podskazka1()
    {
        yield return new WaitForSeconds(_time_podskazka1);
        _podskazka1.SetActive(true);
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(_time_finish);
        _Cansvas_Obuchenie_self.SetActive(false);
        _Cansvas_Finish_Obuchenie.SetActive(true);
    }
}
