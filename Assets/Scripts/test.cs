using UnityEngine;

public class TexturePainter : MonoBehaviour
{
    public int textureSize = 512; // Taille de la texture
    public Color brushColor = Color.red; // Couleur du pinceau
    public int brushSize = 10; // Taille du pinceau

    private Texture2D drawTexture;
    private Renderer objectRenderer;
    private Color[] brushPixels;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        // Création de la texture vide
        drawTexture = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Bilinear;

        // Remplir la texture de blanc
        Color[] clearPixels = new Color[textureSize * textureSize];
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = Color.white;

        drawTexture.SetPixels(clearPixels);
        drawTexture.Apply();

        // Appliquer la texture sur l'objet
        objectRenderer.material = new Material(Shader.Find("Standard"));
        objectRenderer.material.mainTexture = drawTexture;

        // Créer un "pinceau" sous forme de tableau de couleurs
        brushPixels = new Color[brushSize * brushSize];
        for (int i = 0; i < brushPixels.Length; i++)
            brushPixels[i] = brushColor;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 🖱️ Commence à dessiner immédiatement
        {
            HandleTouch(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0)) // Continue de dessiner en bougeant
        {
            HandleTouch(Input.mousePosition);
        }

        if (Input.touchCount > 0) // 📱 Support du tactile
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    HandleTouch(touch.position);
                }
            }
        }
    }

    // 🖌️ Gère le dessin avec un clic ou un toucher
    void HandleTouch(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) // Vérifier si on touche un objet
        {
            Vector2 pixelUV = hit.textureCoord; // Coordonnées UV du point d’impact
            pixelUV.x *= textureSize; // Conversion UV → Pixels X
            pixelUV.y = 1 - pixelUV.y; // ⚠️ Inversion de Y (car UV va de bas en haut)
            pixelUV.y *= textureSize; // Conversion UV → Pixels Y

            DrawAt(pixelUV); // Dessiner à la bonne position
        }
    }


    // Dessine sur la texture
    void DrawAt(Vector2 uvPos)
    {
        int x = (int)uvPos.x;
        int y = (int)uvPos.y;

        for (int i = -brushSize / 2; i < brushSize / 2; i++)
        {
            for (int j = -brushSize / 2; j < brushSize / 2; j++)
            {
                int px = Mathf.Clamp(x + i, 0, textureSize - 1);
                int py = Mathf.Clamp(y + j, 0, textureSize - 1);
                drawTexture.SetPixel(px, py, brushColor);
            }
        }

        drawTexture.Apply();
    }

    // 🧹 Effacer le dessin
    public void ClearCanvas()
    {
        Color[] clearPixels = new Color[textureSize * textureSize];
        for (int i = 0; i < clearPixels.Length; i++)
            clearPixels[i] = Color.white;

        drawTexture.SetPixels(clearPixels);
        drawTexture.Apply();
    }

    // 🎨 Changer la couleur du pinceau
    public void SetBrushColor(Color newColor)
    {
        brushColor = newColor;
        for (int i = 0; i < brushPixels.Length; i++)
            brushPixels[i] = newColor;
    }
}
