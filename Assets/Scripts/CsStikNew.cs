using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




//ЕСЛИ ПОСЛЕ ПОДКЛЮЧЕНИЯ ИНТЕРФЕЙСА IDragHandler И ДР ЮНИТИ ВЫДАЁТ ОШИБКУ
//КЛАСС CsStikNew не реализует член интерфейса IDragHandler
//то создай сразу какую-то функцию для этого интерфейса IDragHandler
//в моём случае public void OnDrag(PointerEventData eventData)
public class CsStikNew : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] csMove target;

    [SerializeField] Image stik1;
    [SerializeField] Image stik2_graniza;
    private float radius_ogranichenie = 0f;

    bool knopka_naschata = false;

    Vector2 StartPosition;
    Vector2 StikPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = stik1.rectTransform.localPosition;
        StikPosition = StartPosition;
        // Радиус круга — половина ширины белой зоны (предполагается круглая зона)
        radius_ogranichenie = stik2_graniza.rectTransform.rect.width / 2f;

    }

    // Update is called once per frame
    void Update()
    {
        if(knopka_naschata)
        {
            target.Set_Napravlenie_stik((StikPosition - StartPosition)/ radius_ogranichenie);
        }
    }

    //вызывается когда мы водим пальцем по экрану
    public void OnDrag(PointerEventData eventData)
    {
        //условие в if преобразует точку нажатия на экран в координаты. +- так. Как-то там меняет систему координат
        //1 - аргумент stik2_graniza.rectTransform - в какой области нажатие отслеживается (границы большого стика)
        //2 - eventData.position - позиция щелчка мыши
        //3 - тут должна быть камера, связанная с точкой экрана. Но сейчас камера не нужна
        //4 - out StikPosition - локальные координаты, которые мы хотели получить
        //где out - ключевое слово для передачи аргументов по ссылке
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(stik2_graniza.rectTransform, eventData.position, null, out StikPosition))
        {

            if ((StikPosition - StartPosition).magnitude > radius_ogranichenie)
            //проверка насколько далеко мышь от центра джостика
            {
                StikPosition = (StikPosition - StartPosition).normalized * radius_ogranichenie + StartPosition;
                //ограничение перемещения стика кругом
                //стик начинает крутиться вокруг нижнего левого угла, чтобы этого не было, добавляем вручную смещение + StartPosition
            }

            stik1.rectTransform.localPosition = StikPosition; //перемещение за мышью

        }

    }

    //вызывается при нажатии на экран.
    public void OnPointerDown(PointerEventData eventData)
    {
        knopka_naschata = true;
    }

    //вызывается при отпускании кнопки. Работает только при реализации OnPointerDown
    public void OnPointerUp(PointerEventData eventData)
    {
        stik1.rectTransform.localPosition = StartPosition; //возвращаем стик в центр
        knopka_naschata = false;

        StikPosition = StartPosition;
        //без этого возникает следующий баг
        //если нажать на стик но никуда не потянуть
        //то прицел полетит в ту сторону с той скоростью
        //какая была последний раз при опускании стика
    }
}
