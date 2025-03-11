using TMPro;
using UnityEngine;

public class GyroBall : MonoBehaviour
{
    private Quaternion _deviceGyro;
    private Rigidbody _rb;
    public float torqueMod = 2;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }
    
    void Update()
    {
        _deviceGyro = Input.gyro.attitude;
        
        Vector3 torque = new Vector3(-_deviceGyro.x, 0, -_deviceGyro.y);
        _rb.AddTorque(torque * torqueMod, ForceMode.Impulse);
    }
}
