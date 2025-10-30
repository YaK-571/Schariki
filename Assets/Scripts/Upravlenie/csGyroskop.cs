using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class csGyroskop : MonoBehaviour
{

    [Header("Настройки")]
    public float baseSensitivity = 0.5f;
    public float maxSensitivity = 2f;
    public float tiltThreshold = 30f; // Угол для максимальной чувствительности
    public float deadZone = 0;     // Мертвая зона
    [SerializeField] csMove _moveController;

    private Quaternion _initialAttitude;

    public int inversija_x = 1;
    public int inversija_y = 1;

#if UNITY_WEBGL
    //для гироскопа в вебе
    private DeviceOrientation orientation;
    //поворот телефона (для ручного изменения гироскопа в вебе)
    private int inversija_web = 1;
#endif


    //[SerializeField] TextMeshProUGUI textMeshProUGUI1;
    //[SerializeField] TextMeshProUGUI textMeshProUGUI2;


    void Start()
    {
        Input.gyro.enabled = true; //включение гироскопа
        StartCoroutine(CalibrateNextFrame());
        //гироскоп при старте ещё не передаёт корректные данные
        //поэтому калибруем в следующем кадре
    }

    void Update()
    {
        // 1. Рассчитываем текущий наклон
        Quaternion deltaRotation = Quaternion.Inverse(_initialAttitude) * Input.gyro.attitude;
        Vector3 eulerTilt = deltaRotation.eulerAngles;
        float tiltX = Mathf.DeltaAngle(0, eulerTilt.x);
        float tiltY = Mathf.DeltaAngle(0, eulerTilt.y);

        // 2. Динамическая чувствительность (зависит от наклона)
        float tiltFactor = Mathf.Clamp01(Mathf.Max(Mathf.Abs(tiltX), Mathf.Abs(tiltY)) / tiltThreshold);
        float sensitivity = Mathf.Lerp(baseSensitivity, maxSensitivity, tiltFactor);

        // 3. Получаем скорость от гироскопа (инвертируем ось Y)
        Vector3 gyroRate = Input.gyro.rotationRateUnbiased;

        Vector2 gyroInput = new Vector2(0f, 0f);
#if UNITY_WEBGL
        orientation = Input.deviceOrientation;
        if (orientation == DeviceOrientation.LandscapeLeft) inversija_web = -1;
        else if (orientation == DeviceOrientation.LandscapeRight) inversija_web = 1;
        gyroInput = new Vector2(gyroRate.x * inversija_web* inversija_y, gyroRate.y * inversija_web* inversija_x);
        //textMeshProUGUI1.text = orientation.ToString();
        //textMeshProUGUI2.text = inversija_web.ToString();

#else
        gyroInput = new Vector2(-gyroRate.y*inversija_y, gyroRate.x*inversija_x);
#endif
        // 4. Фильтруем микро-движения и применяем чувствительность
        if (gyroInput.magnitude > deadZone)
        {
            Vector2 acceleration = gyroInput * sensitivity;
            _moveController.Set_Napravlenie_gyro(acceleration);
        }
        else
        {
            _moveController.Set_Napravlenie_gyro(Vector2.zero);
        }
    }

    public void Calibrate()
    {
        _initialAttitude = Input.gyro.attitude;
    }

    IEnumerator CalibrateNextFrame()
    {
        yield return new WaitForSeconds(0.2f); // ждём один кадр (зачёркнуто) ждём чуть-чуть
        //при калибровке через один кадр - всё равно иногда сразу не срабатывает
        inversija_x = Progress.GameInstance.date.inversija_x;
        inversija_y = Progress.GameInstance.date.inversija_y;

        baseSensitivity = Progress.GameInstance.date.chuvstvitelnost_gyro_base;
        maxSensitivity = Progress.GameInstance.date.chuvstvitelnost_gyro_max;
        Calibrate();       // вызываем калибровку
    }


}

/*//------------          ВАРИАНТ  1            --------------------------
     [Header("Настройки прицела")]
     public float sensitivity = 0.3f;
     public float maxX = 2f; // Максимальное смещение прицела по X
     public float maxY = 2f; // Максимальное смещение прицела по Y

     private Quaternion baseRotation = Quaternion.identity;

     void Start()
     {
         Input.gyro.enabled = true;
         Calibrate(); // Автоматическая калибровка при старте
     }

     void Update()
     {
         // Получаем откалиброванное вращение
         Quaternion calibratedRotation = GetCalibratedRotation();

         // Корректируем координаты для Unity (можно подправить под нужды)
         Quaternion correction = new Quaternion(0, 0, 1, 0);
         Quaternion correctedRotation = calibratedRotation * correction;

         Vector3 euler = correctedRotation.eulerAngles;

         // Преобразуем углы Эйлера в смещение прицела
         // Можно использовать только нужные оси (например, X и Y)
         float offsetX = -Mathf.DeltaAngle(0, euler.y) * sensitivity;
         float offsetY = Mathf.DeltaAngle(0, euler.x) * sensitivity;

         // Ограничиваем смещение
         // offsetX = Mathf.Clamp(offsetX, -maxX, maxX);
         // offsetY = Mathf.Clamp(offsetY, -maxY, maxY);

         // Применяем смещение к позиции прицела
         transform.localPosition = new Vector3(offsetX, offsetY, transform.localPosition.z);
     }

     // Метод для калибровки — вызывать по кнопке
     public void Calibrate()
     {
         baseRotation = Input.gyro.attitude;
     }

     // Получение откалиброванного вращения
     private Quaternion GetCalibratedRotation()
     {
         return Quaternion.Inverse(baseRotation) * Input.gyro.attitude;
     }

     */


/*//------------          ВАРИАНТ  2            --------------------------
[SerializeField] csMove _csMove;
[Header("Настройки прицела")]
[SerializeField] public float sensitivity = 3f;
float sensitivityFactor;
public float deadZone;
private Quaternion baseRotation = Quaternion.identity;
Vector2 napravlenie;

void Start()
{
    sensitivityFactor = sensitivity / 180f; //чат гпт сказал так нада
    Input.gyro.enabled = true;
    Calibrate(); // Автоматическая калибровка при старте
}

void Update()
{
    Debug.Log(deadZone);
    Debug.Log(sensitivity);
    // Получаем откалиброванное вращение
    Quaternion calibratedRotation = GetCalibratedRotation();

    // Корректируем координаты для Unity (можно подправить под нужды)
    Quaternion correction = new Quaternion(0, 0, 1, 0);
    Quaternion correctedRotation = calibratedRotation * correction;
    Vector3 euler = correctedRotation.eulerAngles;

    // Преобразуем углы Эйлера в смещение прицела
    // Можно использовать только нужные оси (например, X и Y)
    napravlenie.x = -Mathf.DeltaAngle(0, euler.y) * sensitivityFactor;
    napravlenie.y = Mathf.DeltaAngle(0, euler.x) * sensitivityFactor;

    //создаём дедзону - мёртвую зону - для гироскопа, чтобы совсем мелкие движения не шарахали прицел
    if (Mathf.Abs(napravlenie.x) < deadZone) napravlenie.x = 0f;
    if (Mathf.Abs(napravlenie.y) < deadZone) napravlenie.y = 0f;

    // Отправляем нормализованное направление
    if (napravlenie.magnitude > 0.0001f)
    {
        _csMove.Set_Napravlenie_gyro(napravlenie.normalized);
    }
    else
    {
        _csMove.Set_Napravlenie_gyro(Vector2.zero);
    }
}
// Метод для калибровки — вызывать по кнопке
public void Calibrate()
{
    baseRotation = Input.gyro.attitude;
}

// Получение откалиброванного вращения
private Quaternion GetCalibratedRotation()
{
    return Quaternion.Inverse(baseRotation) * Input.gyro.attitude;
}
}
*/

