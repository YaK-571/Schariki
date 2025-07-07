using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGyroskop : MonoBehaviour
{
    [Header("��������� �������")]
    public float sensitivity = 0.3f;
    public float maxX = 2f; // ������������ �������� ������� �� X
    public float maxY = 2f; // ������������ �������� ������� �� Y

    private Quaternion baseRotation = Quaternion.identity;

    void Start()
    {
        Input.gyro.enabled = true;
        Calibrate(); // �������������� ���������� ��� ������
    }

    void Update()
    {
        // �������� ��������������� ��������
        Quaternion calibratedRotation = GetCalibratedRotation();

        // ������������ ���������� ��� Unity (����� ���������� ��� �����)
        Quaternion correction = new Quaternion(0, 0, 1, 0);
        Quaternion correctedRotation = calibratedRotation * correction;

        Vector3 euler = correctedRotation.eulerAngles;

        // ����������� ���� ������ � �������� �������
        // ����� ������������ ������ ������ ��� (��������, X � Y)
        float offsetX = -Mathf.DeltaAngle(0, euler.y) * sensitivity;
        float offsetY = Mathf.DeltaAngle(0, euler.x) * sensitivity;

        // ������������ ��������
     //   offsetX = Mathf.Clamp(offsetX, -maxX, maxX);
      //  offsetY = Mathf.Clamp(offsetY, -maxY, maxY);

        // ��������� �������� � ������� �������
        transform.localPosition = new Vector3(offsetX, offsetY, transform.localPosition.z);
    }

    // ����� ��� ���������� � �������� �� ������
    public void Calibrate()
    {
        baseRotation = Input.gyro.attitude;
    }

    // ��������� ���������������� ��������
    private Quaternion GetCalibratedRotation()
    {
        return Quaternion.Inverse(baseRotation) * Input.gyro.attitude;
    }
}