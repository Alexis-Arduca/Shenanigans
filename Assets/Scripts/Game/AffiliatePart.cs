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

        if (isLocalPlayer)
        {
            CmdRequestPlayerID();
        }
    }

    [Command]
    void CmdRequestPlayerID()
    {
        int playerID = connectionToClient.connectionId;
        TargetReceivePlayerID(connectionToClient, playerID);
    }

    [TargetRpc]
    void TargetReceivePlayerID(NetworkConnectionToClient target, int playerID)
    {   
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
