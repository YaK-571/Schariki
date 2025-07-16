using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPerecluchenieUpravleniya : MonoBehaviour
{
    [SerializeField] csMove csMove;
    [SerializeField] csTachpad csTachpad;
    [SerializeField] csGyroskop csGyroskop;
    [SerializeField] GameObject UIStik;

 public void VKL_Tachpad()
    {
        csTachpad.enabled = true;
        csGyroskop.enabled = false;
        UIStik.SetActive(false);

    }
    public void VKL_Gyroskop()
    {
        csTachpad.enabled = false;
        csGyroskop.enabled = true;
        UIStik.SetActive(false);
    }
    public void VKL_Stik()
    {
        csTachpad.enabled = false;
        csGyroskop.enabled = false;
        UIStik.SetActive(true);
    }
    public void VKL_Gyroskop_i_Stik()
    {
        csTachpad.enabled = false;
        csGyroskop.enabled = true;
        UIStik.SetActive(true);
    }

}
