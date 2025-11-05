using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOtschistkaMusora_2 : MonoBehaviour
{
    int prioritet_udalenija;

    public void otschistka_misora(int a)
    {
        if (prioritet_udalenija == a)
        {
            Destroy(gameObject);
        }
    }
}
