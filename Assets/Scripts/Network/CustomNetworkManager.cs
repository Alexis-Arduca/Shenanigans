using UnityEngine;
using Mirror;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class CustomNetworkManager : NetworkManager
{
    // Called when the host starts
    public override void OnStartHost()
    {
        base.OnStartHost();
        string localIP = GetLocalIPAddress();
        Debug.Log($"Host has started. Local IP Address: {localIP}");
    }

    // Called on the client when it connects to the server.
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Client connected; requesting player spawn.");

        // Only add a player if this client is not the host.
        if (!NetworkServer.active)
        {
            if (!NetworkClient.ready)
            {
                NetworkClient.Ready();
            }
            NetworkClient.AddPlayer();
        }
    }

    // Called on the server when a player should be added.
    // Using AddPlayerForConnection will instantiate the player object for this connection.
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log($"OnServerAddPlayer called for connection {conn.connectionId}.");

        // For this example, we treat connectionId 0 as the host (spectator) and skip spawning a player.
        if (conn.connectionId == 0)
        {
            Debug.Log("Host (PC) is connected as spectator – no player spawned.");
            return;
        }

        // Instantiate the player prefab and register it with the connection.
        Debug.Log($"Spawning player for client {conn.connectionId}.");
        GameObject playerInstance = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, playerInstance);
    }

    // Helper method to retrieve the local IP address.
    private string GetLocalIPAddress()
    {
        string localIP = "Not available";
        try
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = unicastAddress.Address.ToString();
                            break;
                        }
                    }
                }
                if (localIP != "Not available")
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error retrieving local IP address: {e.Message}");
        }
        return localIP;
    }
}
