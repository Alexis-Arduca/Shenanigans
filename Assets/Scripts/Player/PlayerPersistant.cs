using Mirror;

public class PlayerPersistence : NetworkBehaviour
{
    public override void OnStartAuthority()
    {
        // Keep player object between scenes
        DontDestroyOnLoad(gameObject);
    }
}