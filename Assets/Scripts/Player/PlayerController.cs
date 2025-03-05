using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnColorChanged))]
    private Color playerColor;

    private SpriteRenderer sr;
    public float moveSpeed = 5f;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Attempt to get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer component missing on player prefab.");
            return;
        }

        // Generate a random color
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        // Send the color to the server to sync with all clients
        CmdSetColor(randomColor);
    }

    [Command]
    private void CmdSetColor(Color color)
    {
        playerColor = color;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        // Attempt to get the SpriteRenderer component if it's not already assigned
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.color = newColor;
        else
            Debug.LogError("SpriteRenderer component missing on player object.");
    }

    void Update()
    {
        // Only the local player should handle movement input.
        if (!isLocalPlayer) return;

#if UNITY_EDITOR
        // In the Editor, use mouse input for testing.
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
#else
        // On mobile, use touch input.
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
