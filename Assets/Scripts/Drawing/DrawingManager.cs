using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawingManager : MonoBehaviour
{
    public Camera myCamera;
    public PenManager myBrush;

    private LineRenderer currentLineRenderer;
    private bool isDrawing = false;

    /// ===[ SETTER ]=== \\\
    ///====================\\\
    public void SetDrawing(bool draw)
    {
        isDrawing = draw;
    }

    /// ===[ GETTER ]=== \\\
    ///====================\\\
    public bool IsDrawing()
    {
        return isDrawing;
    }

    /// ===[ FUNCTION ]=== \\\
    ///====================\\\
    public void StartDrawing(Vector2 screenPos)
    {
        CreateBrush(screenPos);
        isDrawing = true;
    }

    private void CreateBrush(Vector2 screenPos)
    {
        GameObject brushInstance = Instantiate(myBrush.GetBrush());
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector3 worldPos = myCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, worldPos);
        currentLineRenderer.SetPosition(1, worldPos);
    }

    public void AddAPoint(Vector2 screenPos)
    {
        if (currentLineRenderer != null)
        {
            Vector3 worldPos = myCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            currentLineRenderer.positionCount++;

            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, worldPos);
        }
    }
}
