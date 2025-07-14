using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPause : MonoBehaviour
{
    [SerializeField] GameObject _UI_pause;

    public void pause()
    {
        Time.timeScale = 0f;
        _UI_pause.SetActive(true);
    }
}
