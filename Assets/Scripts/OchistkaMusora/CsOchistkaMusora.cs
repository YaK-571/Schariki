using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsOchistkaMusora : MonoBehaviour
{
    [SerializeField] CsOchistkaMusora roditel;
    
    //Удаляем падающий груз/верёвочку
    //если у шара остался только абстрактный родительский обьект, то удаляем его
    public void Delete()
    {
        if(roditel)
        {
            roditel.UdaleniePustogoRoditelja();
        }
        Destroy(gameObject);
    }

    public void UdaleniePustogoRoditelja()
    {
        if(transform.childCount <= 1)
        {
            Delete();
        }
    }


}
