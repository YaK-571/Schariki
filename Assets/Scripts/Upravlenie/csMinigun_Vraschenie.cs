using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMinigun_Vraschenie : MonoBehaviour
{
    [SerializeField] int skorost;
    [SerializeField] GameObject target;
    void Update()
    {
        transform.position = target.transform.position;
        transform.eulerAngles =  new Vector3(0,0,transform.eulerAngles.z + skorost * Time.deltaTime);
    }
}
