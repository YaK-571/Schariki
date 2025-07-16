using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CsDebugInputField : MonoBehaviour
{
    public csGyroskop targetGyro; // Перетащи сюда гироскоп из сцены
    public string targetVariable = "sensitivity"; // Имя переменной (rotationRate, gravity и т.д.)

    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(UpdateGyroValue);
    }

    private void UpdateGyroValue(string text)
    {
        if (float.TryParse(text, out float value))
        {
            // Меняем нужную переменную в гироскопе
            if (targetVariable == "sensitivity")
                targetGyro.baseSensitivity = value/1000;
            else if (targetVariable == "maxSensitivity")
                targetGyro.maxSensitivity = value/1000;
            else if (targetVariable == "tiltThreshold")
                targetGyro.tiltThreshold = value;
            // Добавь другие переменные по необходимости
        }
    }
}