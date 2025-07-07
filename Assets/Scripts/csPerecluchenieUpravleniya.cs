using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPerecluchenieUpravleniya : MonoBehaviour
{
    [SerializeField] csMove csMove;
    [SerializeField] csGyroskop csGyroskop;
    bool stik = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Perecluchenie()
    {
        if (stik)
        {
            csMove.enabled = false;
            csGyroskop.enabled = true;
            stik =false;
        }
        else 
        {
            csMove.enabled = true;
            csGyroskop.enabled = false;
            stik =true; 
        }

    }
}
