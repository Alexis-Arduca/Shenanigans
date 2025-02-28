using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public Camera myCamera;
    public PenManager myBrush;
    public RenderTexture renderTexture;
    public RawImage myRawImage;

    private LineRenderer currentLineRenderer;
    private bool isDrawing = false;

    void Update()
    {
        // ---> Delete Later (PC Test)
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isDrawing)
        {
            AddAPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
        // ---> Delete Later (PC Test)

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartDrawing(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                AddAPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDrawing = false;
            }
        }
    }

    void StartDrawing(Vector2 screenPos)
    {
        CreateBrush(screenPos);
        isDrawing = true;
    }

    void CreateBrush(Vector2 screenPos)
    {
        GameObject brushInstance = Instantiate(myBrush.GetBrush());
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector3 worldPos = myCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));

        currentLineRenderer.positionCount = 2;
        currentLineRenderer.SetPosition(0, worldPos);
        currentLineRenderer.SetPosition(1, worldPos);
    }

    void AddAPoint(Vector2 screenPos)
    {
        if (currentLineRenderer != null)
        {
            Vector3 worldPos = myCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, worldPos);
        }
    }

    public void SaveDrawing()
    {
        RenderTexture.active = renderTexture;

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Drawing.png", bytes);
        Debug.Log("Dessin sauvegardé à : " + Application.persistentDataPath + "/Drawing.png");

        myRawImage.texture = texture;
    }
}
