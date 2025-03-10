//using UnityEngine;
//using Mirror;

//public class CustomNetworkManager : NetworkManager
//{
//    // Called when the host starts
//    public override void OnStartHost()
//    {
//        base.OnStartHost();
//        Debug.Log("Host has started.");
//    }

//    // Called on the client when it connects to the server.
//    // Note: This override is now parameterless.
//    public override void OnClientConnect()
//    {
//        base.OnClientConnect();
//        Debug.Log("Client connected; requesting player spawn.");

//        // Only add a player if this client is not the host.
//        // If we're not running as a server (host), request to add a player.
//        if (!NetworkServer.active)
//        {
//            NetworkClient.AddPlayer();
//        }
//    }

//    // Called on the server when a player should be added.
//    // This method now takes a NetworkConnectionToClient parameter.
//    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
//    {
//        Debug.Log($"OnServerAddPlayer called for connection {conn.connectionId}.");

//        // Check if this connection is the host (assumed to have connectionId 0).
//        // If so, do not spawn a player (host remains a spectator).
//        if (conn.connectionId == 0)
//        {
//            Debug.Log("Host (PC) is connected as spectator – no player spawned.");
//            return;
//        }

//        // For mobile clients, spawn a player object.
//        Debug.Log($"Spawning player for client {conn.connectionId}.");
//        GameObject player = Instantiate(playerPrefab);
//        NetworkServer.AddPlayerForConnection(conn, player);
//    }
//}
