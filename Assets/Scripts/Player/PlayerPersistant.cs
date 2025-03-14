using UnityEngine;
using Mirror;

public class PersistentPlayer : NetworkBehaviour
{
    public static PersistentPlayer localPlayer;

    void Awake()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Add any network or player-specific logic here.
}
