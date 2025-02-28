using UnityEngine;

public class PenManager : MonoBehaviour
{
    public GameObject brushPrefab;
    public Color penColor = Color.black;
    public float penSize = 0.5f;
    public Texture2D penTexture;
    
    void Start()
    {
        ApplySettingsToBrush();
    }

    public GameObject GetBrush()
    {
        ApplyToBrushInstance(brushPrefab);
    
        return brushPrefab;
    }

    public void SetColor(int color)
    {
        switch(color) {
            case 1:
                penColor = Color.red;
                break;
            case 2:
                penColor = Color.blue;
                break;
            case 3:
                penColor = Color.yellow;
                break;
            case 4:
                penColor = Color.green;
                break;
        }
        ApplySettingsToBrush();
    }

    public void SetSize(float newSize)
    {
        penSize = Mathf.Clamp(newSize, 0.01f, 1f);
        ApplySettingsToBrush();
    }

    public void SetTexture(Texture2D newTexture)
    {
        penTexture = newTexture;
        ApplySettingsToBrush();
    }

    private void ApplySettingsToBrush()
    {
        if (brushPrefab != null)
        {
            LineRenderer lineRenderer = brushPrefab.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.startColor = penColor;
                lineRenderer.endColor = penColor;
                lineRenderer.startWidth = penSize;
                lineRenderer.endWidth = penSize;

                if (penTexture != null)
                {
                    lineRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
                    lineRenderer.sharedMaterial.mainTexture = penTexture;
                }
            }
        }
    }

    public void ApplyToBrushInstance(GameObject brushInstance)
    {
        LineRenderer lineRenderer = brushInstance.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.startColor = penColor;
            lineRenderer.endColor = penColor;
            lineRenderer.startWidth = penSize;
            lineRenderer.endWidth = penSize;

            if (penTexture != null)
            {
                lineRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.sharedMaterial.mainTexture = penTexture;
            }
        }
    }
}
