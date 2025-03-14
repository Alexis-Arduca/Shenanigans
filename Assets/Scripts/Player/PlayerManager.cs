using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager localPlayer;

    void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
    }
}
