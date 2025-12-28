using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSbrosProgressaActyve : MonoBehaviour
{
    [SerializeField] GameObject button;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        button.gameObject.SetActive(true);
#endif
    }

}
