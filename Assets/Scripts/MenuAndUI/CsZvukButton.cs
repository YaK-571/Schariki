using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsZvukButton : MonoBehaviour
{
    [SerializeField] AudioSource _zvuk;
    
  public void Zvuk()
    {
        _zvuk.Play();
    }
}
