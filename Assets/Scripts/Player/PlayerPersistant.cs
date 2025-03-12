using UnityEngine;
using Mirror;

public class PersistentPlayer : NetworkBehaviour
{
    private static PersistentPlayer instance;

    void Awake()
    {
        // // If an instance already exists and it's not this, destroy this object.
        // if (instance != null && instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        // instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Add any network or player-specific logic here.
}