using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsDelete : MonoBehaviour
{
    [SerializeField] bool verh = false;
    //если шар летит вверх и он наткнётся на очистку мусора, то шарик лопнет и груз упадёт обратно
    //это забавно и вызовет у игрока недоумение
    //но чтобы этого не было, наверху надо удалять весь обьект целиком
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CsOchistkaMusora>())
        {
           
            collision.GetComponent<CsOchistkaMusora>().Delete(verh);
        }

    }
}
