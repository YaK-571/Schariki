using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGraniza_Verh : MonoBehaviour
{

    [SerializeField] GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<csBomba>())

        {
            if(collision.GetComponent<CsVsruv_new>())
            { collision.GetComponent<CsVsruv_new>().Vsruv(); }//тут звук
            
            collision.GetComponent<csBomba>()._vzruv();
             Destroy(collision.GetComponent<csBomba>().gameObject);
            _gameManager.HP(-1);
        }
    }
       
    
}
