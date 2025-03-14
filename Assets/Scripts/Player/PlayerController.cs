using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnColorChanged))]
    private Color playerColor;
    
    [SyncVar] public int playerID;

    private SpriteRenderer sr;
    public float moveSpeed = 5f;

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerID = connectionToClient.connectionId; // Assigner l'ID unique du joueur
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Debug pour vérifier l'ID du joueur local
        Debug.Log($"Je suis le joueur local avec l'ID : {playerID}");

        // Attempt to get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer component missing on player prefab.");
            return;
        }

        // Générer une couleur aléatoire et la synchroniser sur tous les clients
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        CmdSetColor(randomColor);
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        playerColor = color;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.color = newColor;
        else
            Debug.LogError("SpriteRenderer component missing on player object.");
    }

    void Update()
    {
        if (!isLocalPlayer) return;

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
            targetPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
#endif
    }
}
