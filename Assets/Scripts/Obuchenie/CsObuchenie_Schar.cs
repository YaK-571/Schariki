using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsObuchenie_Schar : MonoBehaviour
{

    public static event System.Action<int> ObuschenieStep;//событие для подписки

    [SerializeField] public bool actyve = true;
    [SerializeField] int step = 1;
    private void OnDestroy()
    {
        if(actyve)
        ObuschenieStep?.Invoke(step);//Вызов события у всех, кто на него подписан
        //(в данном случае в гейм менеджере CsObuchenie
    }
}
