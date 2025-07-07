using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTachpad : MonoBehaviour
{
    [SerializeField] csMove target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); //где 0 - первое нажатие на экран, если их несколько одновременно

            if (touch.phase == TouchPhase.Began)  // касание началось, Began - аналог клика
            {
                Vector2 touchPosition = touch.position; // позиция в пикселях на экране
                Vector2 worldPoint2D = Camera.main.ScreenToWorldPoint(touchPosition); //трансформация в мировые координаты

                gameObject.transform.position = worldPoint2D; //перемещение прицела
                target.vustrel(); //выстрел
            }
        }
    }
}
