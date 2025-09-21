using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsScena_Arkada : MonoBehaviour
{

    [SerializeField] string name_lvl;
    [SerializeField] int nomer_cartu;
    public void start_lvl()
    {
        Progress.GameInstance.set_nomer_lvl(nomer_cartu);
        SceneManager.LoadScene(name_lvl);
    }
}
