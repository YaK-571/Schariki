using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cs_VFX_ballu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _zifra;
    int _ballu;
    [SerializeField] float _vremja;

    private void Start()
    {
        StartCoroutine(udalenie());
    }
    public void VFX_ballu(string ballu)
    {
        if(_zifra)
        _zifra.text= ballu;
    }

    private IEnumerator udalenie()
    {
        yield return new WaitForSeconds(_vremja);
        Destroy(gameObject);
    }
}
