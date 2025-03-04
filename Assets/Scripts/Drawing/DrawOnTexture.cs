using UnityEngine;

public class DrawOnTexture : MonoBehaviour
{
    public PenManager myPen;
    private int textureWidth;
    private int textureHeight;
    public RenderTexture myRenderTexture;

    private bool canDraw = false;
    private Texture2D myTexture;
    private Renderer myRend;



    void Start()
    {
        textureWidth = (int)Screen.width;
        textureHeight = (int)Screen.height;

        myRend = GetComponent<Renderer>();

        myTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        myRend.material.mainTexture = myTexture;

        ClearTexture();

        GameEventsManager.instance.drawingEvents.onDrawingStart += UpdateDrawingState;
        GameEventsManager.instance.drawingEvents.onDrawingComplete += UpdateDrawingState;
    }

    void OnDisable()
    {
        GameEventsManager.instance.drawingEvents.onDrawingStart -= UpdateDrawingState;
        GameEventsManager.instance.drawingEvents.onDrawingComplete -= UpdateDrawingState;
    }

    void Update()
    {
        if (textureWidth != Screen.width || textureHeight != Screen.height)
        {
            UpdateTextureSize();
        }

        if (canDraw)
        {
            if (Input.GetMouseButton(0))
            {
                DrawAtPosition(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    DrawAtPosition(touch.position);
                }
            }
        }
    }

    void UpdateTextureSize()
    {
        textureWidth = Screen.width;
        textureHeight = Screen.height;

        myRenderTexture.Release();

        myRenderTexture.width = Screen.width;
        myRenderTexture.height = Screen.height;

        myRenderTexture.Create();

        myTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        myRend.material.mainTexture = myTexture;
        ClearTexture();
    }

    private void DrawAtPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform) {
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= textureWidth;
            pixelUV.y *= textureHeight;

            DrawCircle((int)pixelUV.x, (int)pixelUV.y);
            myTexture.Apply();
        }
    }

    private void DrawCircle(int x, int y)
    {
        int penSize = myPen.GetPenSize();

        for (int i = -penSize; i < penSize; i++) {
            for (int j = -penSize; j < penSize; j++) {
                if (i * i + j * j <= penSize * penSize) {
                    int px = Mathf.Clamp(x + i, 0, textureWidth - 1);
                    int py = Mathf.Clamp(y + j, 0, textureHeight - 1);
                    myTexture.SetPixel(px, py, myPen.GetPenColor());
                }
            }
        }
    }

    public void ClearTexture()
    {
        Color[] clearPixels = new Color[textureWidth * textureHeight];

        for (int i = 0; i < clearPixels.Length; i++) {
            clearPixels[i] = Color.white;
        }
        myTexture.SetPixels(clearPixels);
        myTexture.Apply();
    }

    public void UpdateDrawingState()
    {
        canDraw = !canDraw;
    }
}
