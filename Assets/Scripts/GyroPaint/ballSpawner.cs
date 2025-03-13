using Mirror;
using UnityEngine;

public class BallSpawner : NetworkBehaviour
{
    [Header("Ball Settings")]
    // Set your ball prefab in the inspector. Make sure it has a NetworkIdentity.
    public GameObject ballPrefab;
    // Optional: an offset from the player's position where the ball should spawn.
    public Vector3 spawnOffset = new Vector3(2f, 0f, 0f);

    // This runs on the local client when this object is set up.
    public override void OnStartLocalPlayer()
    {
        // Request the ball to be spawned on the server.
        CmdSpawnBall();
    }

    // This command runs on the server.
    [Command]
    void CmdSpawnBall()
    {
        // Instantiate the ball at a position relative to the player.
        Vector3 spawnPosition = transform.position + spawnOffset;
        GameObject ballInstance = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        // Spawn the ball on the network and assign client authority to the sender.
        NetworkServer.Spawn(ballInstance, connectionToClient);
    }
}