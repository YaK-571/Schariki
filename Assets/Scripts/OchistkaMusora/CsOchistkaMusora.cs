using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOchistkaMusora : MonoBehaviour
{
    [SerializeField] CsOchistkaMusora roditel;
    private bool verh;

    //Удаляем падающий груз/верёвочку
    //если у шара остался только абстрактный родительский обьект, то удаляем его

    //а если шарик взлетел наверх, то удаляем сразу всё целиком, все обьекты в связке через родителя
    public void Delete(bool verh_ = false)
    {

        verh = verh_;

        if (roditel)
        {
            roditel.UdaleniePustogoRoditelja(verh);
        }

        Destroy(gameObject);

    }

    public void UdaleniePustogoRoditelja(bool verh_)
    {
        verh = verh_;
        if (verh)
        {
            Delete(verh);
        }
        else if (transform.childCount <= 1)
        {
            Delete(verh);
        }
    }

}
