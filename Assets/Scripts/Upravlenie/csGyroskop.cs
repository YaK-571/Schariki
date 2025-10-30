using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class csGyroskop : MonoBehaviour
{

    [Header("���������")]
    public float baseSensitivity = 0.5f;
    public float maxSensitivity = 2f;
    public float tiltThreshold = 30f; // ���� ��� ������������ ����������������
    public float deadZone = 0;     // ������� ����
    [SerializeField] csMove _moveController;

    private Quaternion _initialAttitude;

    public int inversija_x = 1;
    public int inversija_y = 1;

#if UNITY_WEBGL
    //��� ��������� � ����
    private DeviceOrientation orientation;
    //������� �������� (��� ������� ��������� ��������� � ����)
    private int inversija_web = 1;
#endif


    //[SerializeField] TextMeshProUGUI textMeshProUGUI1;
    //[SerializeField] TextMeshProUGUI textMeshProUGUI2;


    void Start()
    {
        Input.gyro.enabled = true; //��������� ���������
        StartCoroutine(CalibrateNextFrame());
        //�������� ��� ������ ��� �� ������� ���������� ������
        //������� ��������� � ��������� �����
    }

    void Update()
    {
        // 1. ������������ ������� ������
        Quaternion deltaRotation = Quaternion.Inverse(_initialAttitude) * Input.gyro.attitude;
        Vector3 eulerTilt = deltaRotation.eulerAngles;
        float tiltX = Mathf.DeltaAngle(0, eulerTilt.x);
        float tiltY = Mathf.DeltaAngle(0, eulerTilt.y);

        // 2. ������������ ���������������� (������� �� �������)
        float tiltFactor = Mathf.Clamp01(Mathf.Max(Mathf.Abs(tiltX), Mathf.Abs(tiltY)) / tiltThreshold);
        float sensitivity = Mathf.Lerp(baseSensitivity, maxSensitivity, tiltFactor);

        // 3. �������� �������� �� ��������� (����������� ��� Y)
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
        // 4. ��������� �����-�������� � ��������� ����������������
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
        yield return new WaitForSeconds(0.2f); // ��� ���� ���� (����������) ��� ����-����
        //��� ���������� ����� ���� ���� - �� ����� ������ ����� �� �����������
        inversija_x = Progress.GameInstance.date.inversija_x;
        inversija_y = Progress.GameInstance.date.inversija_y;

        baseSensitivity = Progress.GameInstance.date.chuvstvitelnost_gyro_base;
        maxSensitivity = Progress.GameInstance.date.chuvstvitelnost_gyro_max;
        Calibrate();       // �������� ����������
    }


}

/*//------------          �������  1            --------------------------
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
         // offsetX = Mathf.Clamp(offsetX, -maxX, maxX);
         // offsetY = Mathf.Clamp(offsetY, -maxY, maxY);

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

     */


/*//------------          �������  2            --------------------------
[SerializeField] csMove _csMove;
[Header("��������� �������")]
[SerializeField] public float sensitivity = 3f;
float sensitivityFactor;
public float deadZone;
private Quaternion baseRotation = Quaternion.identity;
Vector2 napravlenie;

void Start()
{
    sensitivityFactor = sensitivity / 180f; //��� ��� ������ ��� ����
    Input.gyro.enabled = true;
    Calibrate(); // �������������� ���������� ��� ������
}

void Update()
{
    Debug.Log(deadZone);
    Debug.Log(sensitivity);
    // �������� ��������������� ��������
    Quaternion calibratedRotation = GetCalibratedRotation();

    // ������������ ���������� ��� Unity (����� ���������� ��� �����)
    Quaternion correction = new Quaternion(0, 0, 1, 0);
    Quaternion correctedRotation = calibratedRotation * correction;
    Vector3 euler = correctedRotation.eulerAngles;

    // ����������� ���� ������ � �������� �������
    // ����� ������������ ������ ������ ��� (��������, X � Y)
    napravlenie.x = -Mathf.DeltaAngle(0, euler.y) * sensitivityFactor;
    napravlenie.y = Mathf.DeltaAngle(0, euler.x) * sensitivityFactor;

    //������ ������� - ������ ���� - ��� ���������, ����� ������ ������ �������� �� �������� ������
    if (Mathf.Abs(napravlenie.x) < deadZone) napravlenie.x = 0f;
    if (Mathf.Abs(napravlenie.y) < deadZone) napravlenie.y = 0f;

    // ���������� ��������������� �����������
    if (napravlenie.magnitude > 0.0001f)
    {
        _csMove.Set_Napravlenie_gyro(napravlenie.normalized);
    }
    else
    {
        _csMove.Set_Napravlenie_gyro(Vector2.zero);
    }
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
*/

