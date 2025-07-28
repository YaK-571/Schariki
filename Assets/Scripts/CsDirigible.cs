using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CsDirigible : MonoBehaviour
{
    [SerializeField] int _HP = 1;
    float HP_max;
    [SerializeField] Image _ProgressBar;
    [SerializeField] int ballu = 100;

    [SerializeField] CsSpawnScharow _spawner;

    [SerializeField] float skorost_perezahjadki_zalpa = 30f;
    [SerializeField] float skorost_spawna = 0.1f;
    [SerializeField] int[] kolichestvo_scharov0;
    [SerializeField] int[] kolichestvo_scharov1;
    [SerializeField] int[] kolichestvo_scharov2;
    [SerializeField] int[] kolichestvo_scharov3;
    [SerializeField] int[] kolichestvo_scharov4;
    int[][] array_kolichestvo_scharov;
    int nomer_zikla = 0;
    int nomer_zikla_max = 4;

    private void Start()
    {
        HP_max = _HP;
        array_kolichestvo_scharov = new int[nomer_zikla_max + 1][];
        array_kolichestvo_scharov[0] = kolichestvo_scharov0;
        array_kolichestvo_scharov[1] = kolichestvo_scharov1;
        array_kolichestvo_scharov[2] = kolichestvo_scharov2;
        array_kolichestvo_scharov[3] = kolichestvo_scharov3;
        array_kolichestvo_scharov[4] = kolichestvo_scharov4;
        Debug.Log("Массив собран, длина: " + array_kolichestvo_scharov.Length);

        Zalp();
    }
    public int Damage(int uron)
    {
        _HP = _HP - uron;
        _ProgressBar.fillAmount = _HP / HP_max;
        if (_HP <= 0) { Destroy(gameObject); return ballu; }
        //тут надо сделать падение при уничтожении, а не ретёрн,
        //иначе баллы не вернуть и не удалить одновременно
        //а падение дирижабля - это красивей простого удлаения

        return 0;
    }

    public void Zalp()
    {
        _spawner.Start_spawna(array_kolichestvo_scharov[0], skorost_spawna);
        StartCoroutine(Perezarhadka_zalpa());
    }

    IEnumerator Perezarhadka_zalpa()
    {
        while (true)
        {
            _spawner.Start_spawna(array_kolichestvo_scharov[nomer_zikla], skorost_spawna);
            nomer_zikla++;
            if (nomer_zikla > nomer_zikla_max)
            {
                nomer_zikla = 0;
            }
            yield return new WaitForSeconds(skorost_perezahjadki_zalpa);
        }

    }
}
