using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CsObushenieStart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string name_lvl;
    void Start()
    {
        if(Progress.GameInstance.date.Coin==1)
        {
            SceneManager.LoadScene(name_lvl);
        }
    }


}
