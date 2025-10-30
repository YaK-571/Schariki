using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPerecluchenieUpravleniya : MonoBehaviour
{
    [SerializeField] csMove csMove;
    [SerializeField] csTachpad csTachpad;
    [SerializeField] csGyroskop csGyroskop;
    [SerializeField] GameObject UIStik;
    int tip_upravlenija;

    private void Start()
    {
        Perecluchenie_Upravlenija();
    }

    public void Perecluchenie_Upravlenija()
    {
        tip_upravlenija = Progress.GameInstance.get_tip_upravlenija();
        if (tip_upravlenija == 0)
        { VKL_Gyroskop_i_Stik(); }
        else if (tip_upravlenija == 1)
        { VKL_Gyroskop();
        }
        else if (tip_upravlenija == 2)
        { VKL_Stik(); }
    }

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
