using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroBall : MonoBehaviour
{
    private Rigidbody _rb;
    private Quaternion _initialRotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
        _initialRotation = Input.gyro.attitude;
    }
    
    void FixedUpdate()
    {
        Quaternion currentRotation = Input.gyro.attitude;
        
        // Does not work
        Quaternion offset = Quaternion.Inverse(_initialRotation) * currentRotation;
        Vector3 torque = new Vector3(-offset.x, 0, -offset.y);
        _rb.AddTorque(torque, ForceMode.Impulse);
        
        // Does not work
        // Vector3 torque = new Vector3(-currentRotation.x, 0, -currentRotation.y);
        // _rb.AddTorque(torque, ForceMode.Impulse);
        
    }
}