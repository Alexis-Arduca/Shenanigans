using UnityEngine;
using Mirror;

public class AffiliatePart : NetworkBehaviour
{
    public GameObject headPart;
    public GameObject bodyPart;
    public GameObject legsPart;

    void Start()
    {
        headPart.SetActive(false);
        bodyPart.SetActive(false);
        legsPart.SetActive(false);

        if (isServer)
        {
            AssignParts();
        }
    }

    [Server]
    void AssignParts()
    {
        foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            int playerID = conn.connectionId;

            if (playerID != 0) {
                TargetReceivePlayerID(conn, playerID);
            }
        }
    }

    [TargetRpc]
    void TargetReceivePlayerID(NetworkConnectionToClient target, int playerID)
    {
        Debug.Log("Caca " + playerID);

        if (playerID == 1)
        {
            headPart.SetActive(true);
        }
        else if (playerID == 2)
        {
            bodyPart.SetActive(true);
        }
        else
        {
            legsPart.SetActive(true);
        }
    }
}
