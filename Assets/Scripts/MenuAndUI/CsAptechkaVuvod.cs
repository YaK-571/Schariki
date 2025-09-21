using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CsAptechkaVuvod : MonoBehaviour
{
    int kolvo;
    [SerializeField] TextMeshProUGUI _aptechki_kolvo;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        kolvo = Progress.GameInstance.date.aptetschka;
        _aptechki_kolvo.text = kolvo.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
