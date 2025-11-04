using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsVsruv_new : MonoBehaviour
{

    [SerializeField] AudioSource _zvuk_lopanie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Vsruv()
    {
        if(_zvuk_lopanie)
        {
            _zvuk_lopanie.Play();
        }
        Destroy(gameObject);
    }
}
