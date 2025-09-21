using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class csVuvodMonet : MonoBehaviour
{
    [SerializeField] int nomer_lvl=0;
    [SerializeField] TextMeshProUGUI _text_coin;
    Progress MyGameInstance;

    private void Start()
    {
        StartCoroutine(ObnovlenieNextFrame());
    }

    public void obnovlenie_monet()
    {
        if (nomer_lvl == 0)
        {
            _text_coin.text = MyGameInstance.date.Coin.ToString(); }
        if (nomer_lvl >0) _text_coin.text = MyGameInstance.date.Coin_record[nomer_lvl-1].ToString();
        
    }

    private IEnumerator ObnovlenieNextFrame()
    { // ещё вариант — ждать, пока Progress не будет инициализирован
        yield return null; //ЖДЁМ КАДР

        MyGameInstance = Progress.GameInstance;
        while (Progress.GameInstance == null)
        yield return null;
        obnovlenie_monet();
    }
}
