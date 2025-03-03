using UnityEngine;

public class DrawOnTexture : MonoBehaviour
{
    public PenManager myPen;
    public int textureWidth;
    public int textureHeight;

    private Texture2D texture;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        texture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        ClearTexture();
        rend.material.mainTexture = texture;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DrawAtMousePosition();
        }
    }

    private void DrawAtMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= textureWidth;
            pixelUV.y *= textureHeight;

            DrawCircle((int)pixelUV.x, (int)pixelUV.y);
            texture.Apply();
        }
    }

    private void DrawCircle(int x, int y)
    {
        int penSize = myPen.GetPenSize();

        for (int i = -penSize; i < penSize; i++)
        {
            for (int j = -penSize; j < penSize; j++)
            {
                if (i * i + j * j <= penSize * penSize)
                {
                    int px = Mathf.Clamp(x + i, 0, textureWidth - 1);
                    int py = Mathf.Clamp(y + j, 0, textureHeight - 1);
                    texture.SetPixel(px, py, myPen.GetPenColor());
                }
            }
        }
    }

    public void ClearTexture()
    {
        Color[] clearPixels = new Color[textureWidth * textureHeight];

        for (int i = 0; i < clearPixels.Length; i++)
        {
            clearPixels[i] = Color.white;
        }
        texture.SetPixels(clearPixels);
        texture.Apply();
    }
}
