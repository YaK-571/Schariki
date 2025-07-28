using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

//[System.Serializable] //ОБРАТИТЬ ВНИМАНИЕ, ЧТО ЭТО НЕ SerializeField
//public struct Scharu
//{
//    [Tooltip("Префаб шара для спавна")]
//    [SerializeField] public GameObject prefab;
//    [Tooltip("Количество шаров этого типа в каждой очереди")]
//    [SerializeField] public Struct_kolichestvo[] Struct_kolichestvo;
    

//}
//[System.Serializable]
//public struct Struct_kolichestvo
//{
//    [SerializeField] public int kolichestvo_start;
//    private int kolichestvo_ostalos;
//    private int kolichestvo_niz;//нужно для рандома, чтобы знать сколько шаров суммарно было до этого шара
//    private int kolichestvo_verh;
//    //сделать переменные по простому через public нельзя, т.к.
//    //они начинают отображаться в инспекторе даже без [SerializeField]
    
//    public int get_kolichestvo_ostalos()
//    { return kolichestvo_ostalos; }
//    public void set_kolichestvo_ostalos(int a)
//    { kolichestvo_ostalos = a; }
//    public int get_kolichestvo_niz()
//    { return kolichestvo_niz; }
//    public void set_kolichestvo_niz(int a)
//    { kolichestvo_niz = a; }
//    public int get_kolichestvo_verh()
//    { return kolichestvo_verh; }
//    public void set_kolichestvo_verh(int a)
//    { kolichestvo_verh = a; }
//}

public class OLDCsSpawnScharow : MonoBehaviour
{ }
//{

//    [SerializeField] bool start_spawner;
//    [SerializeField] float skorost;
//    //для размера
//    [SerializeField] RectTransform _levo_UI;
//    [SerializeField] RectTransform _pravo_UI;
//    Vector3 _levo_position_UI;
//    Vector3 _pravo_position_UI;
//    Vector3 levo;
//    Vector3 pravo;
//    float size_spawner;

//    //для спавна шаров

//    Vector3 ScharPosition = Vector3.zero;
//    [SerializeField] public Scharu[] _scharu;
//    private int size_array_scharu = 0;
//    private int typ_spawn = 0;

//    //[SerializeField] GameObject _debug_odin_schar;


//    private int[] sum_scharov_ostalos;
//    private int[] sum_scharov_max;

//    void Start()
//    {
//        size_array_scharu = _scharu.Length; //получаем количество шаров разных видов

//        //считаем сколько шаров будет заспавнено за уровень всего
//        //нужно для рандомного выбора типа
//        //обычный рандом среди 9 типов шаров нельзя использовать, если мы хотим, чтобы шары спавнились в заданной пропорции
//        //например обычных шаров было больше в 10 раз.
//        //поэтому нужно при рандомной генерации отталикиваться не от количества типов шаров самих по себе
//        //а от их общей сумме в каждом типе


//        if (start_spawner) StartCoroutine(SpawnScharow(0));
//    }


//    IEnumerator SpawnScharow(int otchered)
//    {
//        int i = 0;
//        while (i < size_array_scharu)
//        {

//            _scharu[i].Struct_kolichestvo[otchered].set_kolichestvo_niz(sum_scharov_max[otchered]);
//            sum_scharov_max[otchered]+=_scharu[i].Struct_kolichestvo[otchered].kolichestvo_start;
//            _scharu[i].Struct_kolichestvo[otchered].set_kolichestvo_verh(sum_scharov_max[otchered]);
//            i++;
//        }
//        sum_scharov_ostalos[otchered] = sum_scharov_max[otchered];

//        while (true)
//        {
//            //-----------------ограничение спавна шариков размером экрана---------------

//            //получаем координаты обьекта на канвасе
//            _levo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _levo_UI.position);
//            _pravo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _pravo_UI.position);
//            //преобразуем координаты канваса в обычные координаты
//            levo = Camera.main.ScreenToWorldPoint(new Vector3(_levo_position_UI.x, _levo_position_UI.y, 0.0f));
//            pravo = Camera.main.ScreenToWorldPoint(new Vector3(_pravo_position_UI.x, _pravo_position_UI.y, 0.0f));
//            //где z==0 это позиция камеры
//            //levo.z = 0.0f; //нужно, чтобы обьект не улетал на задний план

//            size_spawner = (pravo.x - levo.x) * 0.9f; //коэфф, чтобы он не спавнил шарики на самой границе вне досягаемости камеры
//                                                      // Debug.Log(pravo.y + " "+ levo.y);
//            gameObject.transform.localScale = new Vector3(size_spawner, transform.localScale.y, 0);
//            // gameObject.transform.position = pravo;


//            /*
//            //---------------ПРОСТОЙ СПАВН ШАРИКА----------------------
//            ScharPosition = RandomPosition();
//            Instantiate(_debug_odin_schar, ScharPosition, Quaternion.identity); //где Quaternion.identity - дефолтный ротеншн
//            */


//            //---------------СПАВН ШАРИКОВ----------------------
//            /*
//            ScharPosition = RandomPosition();
//            //если шары ещё есть, то заспавни
//            if (sum_scharov > 0)
//            {
//                typ_spawn = Random_Typ_Schara();
//                Instantiate(_scharu[typ_spawn].prefab, ScharPosition, Quaternion.identity); //где Quaternion.identity - дефолтный ротеншн
//                sum_scharov--;
//                _scharu[typ_spawn].kolichestvo--;
//            }
//            */



//            yield return new WaitForSeconds(skorost);//перезапуск короутины через секунду
//        }
//    }

//    private Vector3 RandomPosition()
//    {
//        //получение рандомной точки в границах экрана для места спавна шарика
//        return new Vector3(Random.Range(0, size_spawner) - size_spawner / 2, transform.position.y, 0f);
//    }
//    /*
//    private int Random_Typ_Schara()
//    {
//        int typ = 0;
//        bool sdvig_typ = false; //выбираем ли тот тип, который сгенерировал рандом или берём следующий если не осталось шаров
//        int a = Random.Range(0, sum_scharov_max);

//        int i = 0;
        
//        //определяем тип в которое вошло рандомое число
//        while (i < size_array_scharu)
//        {
//            if ((a >= _scharu[i].get_niz()) && (a < _scharu[i].get_verh()))
//            {
//                typ = i;
//                break;//если нужный тип найден - дальше не перебираем
//            }
//            i++;
//        }

//        //но если количество оставшихся незаспавненных шаров == 0, то спавнем не этот тип, а следующий
//        if (_scharu[typ].kolichestvo == 0)
//        { sdvig_typ = true; }

//        if (sdvig_typ)
//        {
            
//            int nomer_oborota= 0;
//            while (nomer_oborota < size_array_scharu)
//            {
//                nomer_oborota++;
//                typ++;
//                //если типы перебрали до конца - начни оборот сначала
//                if (typ >= size_array_scharu)  typ = 0;
//                if (_scharu[typ].kolichestvo > 0) break;

//            }
//        }

//            return typ;
//    }*/

//}
