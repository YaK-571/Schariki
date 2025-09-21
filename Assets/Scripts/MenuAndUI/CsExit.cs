using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsExit : MonoBehaviour
{
    public void Start()
    {
#if UNITY_WEBGL
        gameObject.SetActive(false);
#endif
    }

    public void Exit(   )
    {
        Application.Quit();
    }
}
