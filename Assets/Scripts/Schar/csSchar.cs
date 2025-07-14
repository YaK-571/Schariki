using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSchar : MonoBehaviour
{
    [SerializeField] private float speed_default;
    [SerializeField] private float speed_uskorenie;
    Vector2 napravlenie = Vector2.zero;
    Rigidbody2D schar;
    bool uvelischenie_skorosti = true;


    private void Start()
    {
        schar = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        napravlenie.y = 1;
        napravlenie.Normalize();
        // transform.position += new Vector3(0, speed * Time.deltaTime);
        schar.velocity = speed_default * napravlenie;
    }

  public void skorost()
    {
       
        if (uvelischenie_skorosti) 
        {
            speed_default = speed_uskorenie;
            uvelischenie_skorosti = false;
        }
       
    }

    public void Zamerzanie()
    {
        speed_default = speed_default/3;
        speed_uskorenie= speed_uskorenie/2;
    }
}
