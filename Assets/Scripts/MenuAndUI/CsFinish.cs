using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFinish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Progress.GameInstance.Razblokirovka_urovnja();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next_lvl()
    {
        //снятие паузы
        Time.timeScale = 1f;
        Progress.GameInstance.Next_LVL();
        
    }
}
