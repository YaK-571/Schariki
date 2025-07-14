using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGyroskop : MonoBehaviour
{
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
     //   offsetX = Mathf.Clamp(offsetX, -maxX, maxX);
      //  offsetY = Mathf.Clamp(offsetY, -maxY, maxY);

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
}