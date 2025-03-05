using TMPro;
using UnityEngine;

public class GyroBall : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Rigidbody _rb;
    private Quaternion _initialRotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
        _initialRotation = Input.gyro.attitude;
    }
    
    void Update()
    {
        Quaternion currentRotation = Input.gyro.attitude;
        text.text = $"Gyro: {currentRotation.eulerAngles}";
        
        // Calculate the offset from the initial rotation
        Quaternion offset = Quaternion.Inverse(_initialRotation) * currentRotation;
        
        // Convert the offset quaternion to Euler angles
        Vector3 offsetEulerAngles = offset.eulerAngles;
        
        // Apply torque based on the offset angles
        Vector3 torque = new Vector3(-offsetEulerAngles.x, 0, -offsetEulerAngles.z);

        // Scale the torque to make the ball move more noticeably
        _rb.AddTorque(torque * 10, ForceMode.Impulse);
    }
}
