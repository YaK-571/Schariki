using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsPodpiskaSmenaPrizela : MonoBehaviour
{
    public static event System.Action Event_GalkaVukl;

    [SerializeField] Image _galka;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GalkaVukl()
    {
        _galka.gameObject.SetActive(false);
    }
}
