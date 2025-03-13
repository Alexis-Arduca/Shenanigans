using UnityEngine;
using System.Collections.Generic;

public class DrawOnTexture : MonoBehaviour
{
    public PenManager myPen;
    private int textureWidth;
    private int textureHeight;
    public RenderTexture myRenderTexture;

    private bool canDraw = true;
    private bool isBucketSelect = false;
    private Texture2D myTexture;
    private Renderer myRend;
    private Vector2? lastDrawPosition = null;

    void Start()
    {
        GameEventsManager.instance.drawingEvents.onDrawingStart += UpdateDrawingState;
        GameEventsManager.instance.drawingEvents.onDrawingComplete += UpdateDrawingState;
    }
    
    void Awake()
    {
        myRend = GetComponent<Renderer>();

        UpdateTextureSize();
        ClearTexture();
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
            // ---> Delete Later (PC TEST)
            if (Input.GetMouseButton(0))
            {
                DrawAtPosition(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lastDrawPosition = null;
            }
            // ---> Delete Later (PC TEST)

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
                {
                    DrawAtPosition(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lastDrawPosition = null;
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
    }

    private void DrawAtPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform) {
            Texture2D tex = (Texture2D)hit.collider.GetComponent<Renderer>().material.mainTexture;
            Vector2 pixelUV = hit.textureCoord;

            pixelUV.x *= textureWidth;
            pixelUV.y *= textureHeight;

            if (isBucketSelect) {
                Color targetColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                Color fillColor = myPen.GetPenColor();
                BucketFill(new Vector2Int((int)pixelUV.x, (int)pixelUV.y), targetColor, fillColor);
            } else if (lastDrawPosition.HasValue) {
                DrawLine(lastDrawPosition.Value, pixelUV);
            } else {
                DrawCircle((int)pixelUV.x, (int)pixelUV.y);
            }

            lastDrawPosition = pixelUV;
            myTexture.Apply();
        }
    }

    private void BucketFill(Vector2Int pixel, Color targetColor, Color fillColor)
    {
        if (targetColor == fillColor) return;

        Stack<Vector2Int> pixels = new Stack<Vector2Int>();
        pixels.Push(pixel);

        while (pixels.Count > 0)
        {
            Vector2Int p = pixels.Pop();

            if (p.x < 0 || p.x >= textureWidth || p.y < 0 || p.y >= textureHeight)
                continue;

            Color currentColor = myTexture.GetPixel(p.x, p.y);

            if (currentColor == targetColor)
            {
                myTexture.SetPixel(p.x, p.y, fillColor);

                pixels.Push(new Vector2Int(p.x + 1, p.y));
                pixels.Push(new Vector2Int(p.x - 1, p.y));
                pixels.Push(new Vector2Int(p.x, p.y + 1));
                pixels.Push(new Vector2Int(p.x, p.y - 1));
            }
        }

        myTexture.Apply();
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = (x0 < x1) ? 1 : -1;
        int sy = (y0 < y1) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            DrawCircle(x0, y0);

            if (x0 == x1 && y0 == y1) break;
            int e2 = err * 2;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
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

    public void BucketOption()
    {
        isBucketSelect = !isBucketSelect;
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
