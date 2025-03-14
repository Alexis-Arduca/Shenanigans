using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class CustomNetworkManager : NetworkManager
{
    // UI GameObjects for waiting and joined players.
    private GameObject wait1;
    private GameObject wait2;
    private GameObject wait3;
    private GameObject join1;
    private GameObject join2;
    private GameObject join3;

    // Audio clip to play when a client connects.
    public AudioClip connectSound;
    // Optional AudioSource component for playing the sound.
    public AudioSource audioSource;

    // Called when the host starts.
    public override void OnStartHost()
    {
        base.OnStartHost();
        string localIP = GetLocalIPAddress();
        Debug.Log($"Host has started. Local IP Address: {localIP}");

        wait1 = GameObject.Find("WaitPlayer1");
        wait2 = GameObject.Find("WaitPlayer2");
        wait3 = GameObject.Find("WaitPlayer3");

        join1 = GameObject.Find("JoinedPlayer1");
        join2 = GameObject.Find("JoinedPlayer2");
        join3 = GameObject.Find("JoinedPlayer3");

        join1.SetActive(false);
        join2.SetActive(false);
        join3.SetActive(false);
    }

    // Called on the client when it connects to the server.
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Client connected; requesting player spawn.");

        // Play the connection sound if assigned.
        if (connectSound != null)
        {
            if (audioSource != null)
            {
                // Play using the assigned AudioSource.
                audioSource.PlayOneShot(connectSound);
            }
            else
            {
                // Fallback: play the sound at the origin.
                AudioSource.PlayClipAtPoint(connectSound, Vector3.zero);
            }
        }

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

        // Update UI elements based on the connection id.
        if (conn.connectionId == 1)
        {
            wait1.SetActive(false);
            join1.SetActive(true);
        }
        else if (conn.connectionId == 2)
        {
            wait2.SetActive(false);
            join2.SetActive(true);
        }
        else if (conn.connectionId == 3)
        {
            wait3.SetActive(false);
            join3.SetActive(true);
        }

        // Instantiate the player prefab and register it with the connection.
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
