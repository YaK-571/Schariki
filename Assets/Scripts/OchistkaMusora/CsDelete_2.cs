using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsDelete_2 : MonoBehaviour
{

    [SerializeField] bool verh;
    [SerializeField] int prioritet_udalenija;
    //где 1 - шар
    //2 - груз
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CsOtschistkaMusora_2>())
        {
            if (verh)
            //если это верхн€€ колизи€ - то они удал€ют компоненты в строго заданном пор€дке
            //чтобы не получилось так, что взорвЄтс€ шар, а груз упадЄт обратно
            //поэтому сначала груз, потом шар и т.п.
            { collision.GetComponent<CsOtschistkaMusora_2>().otschistka_misora(prioritet_udalenija); }
            else
            { //если это нижн€€ коллизи€ - удал€й сразу всЄ то, что туда падает
                Destroy(collision.GetComponent<CsOtschistkaMusora_2>());
            }
        }
    }
}
