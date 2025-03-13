using Mirror;
using UnityEngine;

public class GyroNetworked : NetworkBehaviour
{
    [Header("Gyro Settings")]
    public float torqueMod = 2f; // Renamed to match your working example

    private Rigidbody _rb;
    private Quaternion _deviceGyro;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Mirror v93 uses OnStartLocalPlayer for local client initialization
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyro enabled on local player");
        }
        else
        {
            Debug.LogError("Gyroscope not supported");
        }
    }

    void Update()
    {
        // Only the local player processes gyro input
        if (!isLocalPlayer) return;

        // Get gyro attitude (rotation)
        _deviceGyro = Input.gyro.attitude;

        // Calculate torque from gyro data (mirroring your working example)
        Vector3 torque = new Vector3(-_deviceGyro.x, 0, -_deviceGyro.y);

        // Send torque to the server via Command
        CmdApplyGyroTorque(torque);
    }

    [Command]
    void CmdApplyGyroTorque(Vector3 torque)
    {
        // Apply torque on the server's Rigidbody
        _rb.AddTorque(torque * torqueMod, ForceMode.Impulse); // Match ForceMode.Impulse
    }
}