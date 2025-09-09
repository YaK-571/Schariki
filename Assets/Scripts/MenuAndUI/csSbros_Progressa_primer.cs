using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class csSbros_Progressa_primer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _primer_text;
    [SerializeField] TMP_InputField _vvod_input;
    [SerializeField] int a, b, resultat, vvod_igroka;
    // Start is called before the first frame update

    void OnEnable()
    {
        a = Random.Range(1, 10);
        b = Random.Range(1, 10);
        resultat = a + b;

        _primer_text.text = a + " + " + b + " =";
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void set_vvod_igroka()
    {
        Debug.Log(123);
        int.TryParse(_vvod_input.text, out vvod_igroka) ;
        //безопасный способ преобразовать текст в int
        //если _vvod_input.text удаётся преобразовать в int, то значение сохраняется тут же в vvod_igroka

    }
    public void Sbros_progressa()
    {
        if (vvod_igroka == resultat)
        {
            Progress.GameInstance.Sbros_progressa();
        }
    }
}
