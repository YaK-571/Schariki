using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_VFX : MonoBehaviour
{
    [SerializeField] GameObject _vfx_vzruw_prefab;

    public void f_VFX()
    {
        GameObject vfx_vzrus_spawn = Instantiate(_vfx_vzruw_prefab);
        vfx_vzrus_spawn.transform.position = transform.position;
    }
}
