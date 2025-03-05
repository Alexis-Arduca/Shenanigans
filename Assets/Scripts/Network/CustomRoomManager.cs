using Mirror;
using UnityEngine;

public class CustomRoomManager : NetworkRoomManager
{
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"Client {conn.connectionId} connected to the lobby.");
    }

    public override void OnRoomServerDisconnect(NetworkConnectionToClient conn)
    {
        Debug.Log($"Client {conn.connectionId} disconnected from the lobby.");
    }

    public override void OnRoomServerPlayersReady()
    {
        Debug.Log("All players are ready. Starting the game...");
        // Change to the game scene
        ServerChangeScene(GameplayScene);
    }

    public override void OnRoomClientEnter()
    {
        Debug.Log("Client entered the lobby.");
    }

    public override void OnRoomClientExit()
    {
        Debug.Log("Client exited the lobby.");
    }
}
