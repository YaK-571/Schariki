using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

//БЫСТРОЕ КОММЕНТИРОВАНИЕ CTRL+K CTRL+/
/*
[System.Serializable] //ОБРАТИТЬ ВНИМАНИЕ, ЧТО ЭТО НЕ SerializeField
public struct Scharu
{
    [Tooltip("Префаб шара для спавна")]
    [SerializeField] public GameObject prefab;
    [Tooltip("Количество шаров этого типа")]
    [SerializeField] public int kolichestvo;
    private int kolichestvo_niz;//нужно для рандома, чтобы знать сколько шаров суммарно было до этого шара
    private int kolichestvo_verh;
    public int get_niz()
    { return kolichestvo_niz; }
    public int get_verh()
    { return kolichestvo_verh; }
    public void set_niz(int a)
    { kolichestvo_niz = a; }
    public void set_verh(int a)
    { kolichestvo_verh = a; }


}*/

public class CsSpawnScharow : MonoBehaviour
{
    [SerializeField] bool start_spawner;
    //предназначен ли этот спавнер для автоматического долгого спавна шаров или для резкого выстрела дережабля
    [SerializeField] float skorost;

    [Header("Виды шаров")]
    [SerializeField] public GameObject[] prefab;
    [SerializeField] public int[] kolichestvo_start;
    [SerializeField] public int[] kolichestvo_ostalos;
    private int[] kolichestvo_niz;//нужно для рандома, чтобы знать сколько шаров суммарно было до этого шара
    private int[] kolichestvo_verh;

    [Header("Для авторазмера")]
    [SerializeField] RectTransform _levo_UI;
    [SerializeField] RectTransform _pravo_UI;
    Vector3 _levo_position_UI;
    Vector3 _pravo_position_UI;
    Vector3 levo;
    Vector3 pravo;
    float size_spawner;

    //для спавна шаров

    Vector3 ScharPosition = Vector3.zero;
    private int size_array_scharu = 0;
    private int typ_spawn = 0;
    private int sum_scharov_ostalos = 0;
    private int sum_scharov_max = 0;

    private void Awake()
    {
        size_array_scharu = prefab.Length; //получаем количество шаров разных видов

        //заполняем массивы пустыми числами. Нужно сделать сразу, т.к. изменять размер массива нельзя
        kolichestvo_ostalos = new int[size_array_scharu];
        kolichestvo_niz = new int[size_array_scharu];
        kolichestvo_verh = new int[size_array_scharu];

    }
    void Start()
    {
        if (start_spawner) Start_spawna(kolichestvo_start, skorost);
    }

    public void Start_spawna(int[] kolichestvo_start, float new_skorost)
    {
        //массивы в c# - это ссылки
        //явно копируем передающийся массив, иначе он будет изменять исходный массив в дирижабле
        kolichestvo_ostalos = (int[])kolichestvo_start.Clone();
        
        skorost = new_skorost;


        //считаем сколько шаров будет заспавнено за уровень всего
        //нужно для рандомного выбора типа
        //обычный рандом среди 9 типов шаров нельзя использовать, если мы хотим, чтобы шары спавнились в заданной пропорции
        //например обычных шаров было больше в 10 раз.
        //поэтому нужно при рандомной генерации отталикиваться не от количества типов шаров самих по себе
        //а от их общей сумме в каждом типе
        sum_scharov_max = 0;
        int i = 0;
        while (i < size_array_scharu)
        {
            kolichestvo_niz[i] = sum_scharov_max;
            sum_scharov_max = sum_scharov_max + kolichestvo_start[i];
            kolichestvo_verh[i] = sum_scharov_max;
            i++;
        }
        sum_scharov_ostalos = sum_scharov_max;
        StartCoroutine(SpawnScharow());
    }

    IEnumerator SpawnScharow()
    {
        bool spawn_okonchen = true;
        while (spawn_okonchen)
        {

            //-----------------ограничение спавна шариков размером экрана---------------

            //получаем координаты обьекта на канвасе
            _levo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _levo_UI.position);
            _pravo_position_UI = RectTransformUtility.WorldToScreenPoint(null, _pravo_UI.position);
            //преобразуем координаты канваса в обычные координаты
            levo = Camera.main.ScreenToWorldPoint(new Vector3(_levo_position_UI.x, _levo_position_UI.y, 0.0f));
            pravo = Camera.main.ScreenToWorldPoint(new Vector3(_pravo_position_UI.x, _pravo_position_UI.y, 0.0f));
            //где z==0 это позиция камеры
            //levo.z = 0.0f; //нужно, чтобы обьект не улетал на задний план

            size_spawner = (pravo.x - levo.x) * 0.9f; //коэфф, чтобы он не спавнил шарики на самой границе вне досягаемости камеры
                                                      // Debug.Log(pravo.y + " "+ levo.y);
            gameObject.transform.localScale = new Vector3(size_spawner, transform.localScale.y, 0);

            //---------------СПАВН ШАРИКОВ----------------------

            ScharPosition = RandomPosition();
            //если шары ещё есть, то заспавни
            if (sum_scharov_ostalos > 0)
            {
                typ_spawn = Random_Typ_Schara();
                Instantiate(prefab[typ_spawn], ScharPosition, Quaternion.identity); //где Quaternion.identity - дефолтный ротеншн
                sum_scharov_ostalos--;
                kolichestvo_ostalos[typ_spawn]--;
            }
            else
            { spawn_okonchen = false;
            if(start_spawner)
                { Start_spawna(kolichestvo_start, skorost); }//перезапуск, если шары кончились
            }
            yield return new WaitForSeconds(skorost);//перезапуск короутины через секунду
        }
    }

    private Vector3 RandomPosition()
    {
        //получение рандомной точки в границах экрана для места спавна шарика
        return new Vector3(Random.Range(0, size_spawner) - size_spawner / 2, transform.position.y, 0f);
    }

    private int Random_Typ_Schara()
    {
        int typ = 0;
        bool sdvig_typ = false; //выбираем ли тот тип, который сгенерировал рандом или берём следующий если не осталось шаров
        int a = Random.Range(0, sum_scharov_max);

        int i = 0;

        //определяем тип в которое вошло рандомое число
        while (i < size_array_scharu)
        {
            if ((a >= kolichestvo_niz[i]) && (a < kolichestvo_verh[i]))
            {
                typ = i;
                break;//если нужный тип найден - дальше не перебираем
            }
            i++;
        }

        //но если количество оставшихся незаспавненных шаров == 0, то спавнем не этот тип, а следующий
        if (kolichestvo_ostalos[typ] == 0)
        { sdvig_typ = true; }

        if (sdvig_typ)
        {
            int nomer_oborota = 0;
            while (nomer_oborota < size_array_scharu)
            {
                nomer_oborota++;
                typ++;
                //если типы перебрали до конца - начни оборот сначала
                if (typ >= size_array_scharu) typ = 0;
                if (kolichestvo_ostalos[typ] > 0) break;
            }
        }

        return typ;
    }

}
