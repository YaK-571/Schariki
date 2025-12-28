using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsObuchenie_Graniza_Udaljator : MonoBehaviour
{
    //из-за триггера на OnDestroy - шары в обучении при вылете за пределы экрана засчитываются в выполнение задания
    //и упавший мешочек - засчитывается как сбитый мешочек
    //чтобы этого не происходило - отключаем этот обьект

    [SerializeField] bool niz=false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CsObuchenie_Schar>())
        {
            collision.GetComponent<CsObuchenie_Schar>().actyve = false;
        }
        if(niz)
        {

            if (collision.GetComponent<csBomba>())
            {
                collision.gameObject.GetComponent<CsObuchenie_Schar>().actyve = true;
            }
        }
    }
}
