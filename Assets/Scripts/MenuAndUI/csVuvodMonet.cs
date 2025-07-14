using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class csVuvodMonet : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _text_coin;

    private void Start()
    {
        obnovlenie_monet();
    }

    public void obnovlenie_monet()
    {
        _text_coin.text = Progress.GameInstance.date.Coin.ToString();
    }
}
