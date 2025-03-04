using UnityEngine;
using Mirror;

public class CustomPlayerSpawner : NetworkManager
{
    // Track if this instance is the host
    private bool isHost = false;

    // Override OnStartHost to set the host flag to true
    public override void OnStartHost()
    {
        base.OnStartHost();
        isHost = true;
        Debug.Log("Host has started.");
    }

    // Override OnServerConnect to check if the client is the host or a regular client
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log($"Client {conn.connectionId} connected.");

        // If it's the host, don't spawn a player object for them
        if (isHost && conn.identity != null && conn.identity.isLocalPlayer)
        {
            Debug.Log("Host (PC) connected - No player spawned (Spectator Mode)");
            return;
        }

        // For clients, spawn a player object
        if (conn.identity != null && !conn.identity.isLocalPlayer)
        {
            Debug.Log($"Spawning player for Client {conn.connectionId}");
            GameObject player = Instantiate(playerPrefab);  // Instantiate player
            NetworkServer.AddPlayerForConnection(conn, player);  // Assign the player to the connection

            // If it’s not the host, assign client authority
            if (conn.identity != null)
            {
                conn.identity.AssignClientAuthority(conn);
            }
        }
    }

    // Override OnServerAddPlayer to handle player addition logic
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // The host does not need a player object
        if (isHost && conn.identity != null && conn.identity.isLocalPlayer)
        {
            return; // Skip spawning for the host
        }

        // For clients, instantiate the player
        base.OnServerAddPlayer(conn);
    }
}
