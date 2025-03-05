using UnityEngine;

public class GyroBall : MonoBehaviour
{
    private Rigidbody rb;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
        initialRotation = Input.gyro.attitude;
    }
    
    void Update()
    {
        Quaternion currentRotation = Input.gyro.attitude;
        Quaternion offset = Quaternion.Inverse(initialRotation) * currentRotation;
        Vector3 torque = new Vector3(-offset.x, 0, -offset.y);
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
