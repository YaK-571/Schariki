using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CsRecord_Coin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_coin_record;
    int record;
    // Start is called before the first frame update
    void Start()
    {
        Progress MyGameInstance = Progress.GameInstance;
        if (MyGameInstance)
        {
            record = MyGameInstance.get_coin_record();
            _text_coin_record.text = record.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
