using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csNastrojki : MonoBehaviour
{
    [SerializeField] GameObject _Canvas_Nastrojki;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Vkluchi_Nastrojki()
    {
        _Canvas_Nastrojki.SetActive(true);
    }

    public void Zakroj_Nastrojki()
    {
        _Canvas_Nastrojki.SetActive(false);
    }

    public void Sbros_progressa()
    {
        Progress.GameInstance.Sbros_progressa();
    }
}
