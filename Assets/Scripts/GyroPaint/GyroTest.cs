using TMPro;
using UnityEngine;

public class GyroTest : MonoBehaviour
{
    private Quaternion _deviceGyro;
    private Rigidbody _rb;
        
    public TextMeshProUGUI text;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }
    
    void Update()
    {
        _deviceGyro = Input.gyro.attitude;
        text.text = "LeftRight: " + _deviceGyro.x + "\n" +
                    "UpDown: " + _deviceGyro.y + "\n";
        
        Vector3 torque = new Vector3(-_deviceGyro.x, 0, -_deviceGyro.y);
        _rb.AddTorque(torque, ForceMode.Impulse);
    }
}
