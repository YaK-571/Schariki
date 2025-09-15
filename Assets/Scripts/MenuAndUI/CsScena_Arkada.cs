using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsScena_Arkada : MonoBehaviour
{

    [SerializeField] string name_lvl;
    public void start_lvl()
    {

        SceneManager.LoadScene(name_lvl);
    }
}
