using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class csVspuschka : MonoBehaviour
{
    [SerializeField] float _time_dead;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void teleport(Vector3 posizija)
    {
        //поворот случайным образом
        transform.eulerAngles= new Vector3(0,0, Random.Range(-360,360));
        
        //телепорт
        transform.position = posizija;
        StartCoroutine(Test());
        
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(_time_dead);
        //transform.position = new Vector3(0, -20);
        Destroy(gameObject);
    }
}
