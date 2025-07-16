using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSkorostSvjaski : MonoBehaviour
{
    [SerializeField] private float speed_default_peredacha;
    [SerializeField] csSchar[] _scSchar;
    
    public void odinakovaja_skorost(float speed_default)
    {
        int i = 0;
        while (i < _scSchar.Length)
        {
            //немного разнообразия скорость шариков в связке может с низкой вероятностью немного различаться
            speed_default_peredacha = speed_default;
            int skorost = Random.Range(1, 10);
            if (skorost == 1)
            {
                speed_default_peredacha = speed_default_peredacha - 0.1f;
            }
            else if (skorost == 2)
            {
                speed_default_peredacha = speed_default_peredacha + 0.1f;
            }
            //передай всей связке данные
            _scSchar[i].set_speed_default(speed_default_peredacha);
            i++;
        }
    }
}
