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
            _gameManager.HP(-1);
        }
    }
       
    
}
