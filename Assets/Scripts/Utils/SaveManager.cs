using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public RenderTexture renderTexture;
    public RawImage myRawImage;

    void Start()
    {
        GameEventsManager.instance.drawingEvents.onDrawingComplete += SaveDrawing;
    }

    void OnDisable()
    {
        GameEventsManager.instance.drawingEvents.onDrawingComplete -= SaveDrawing;
    }

    public void SaveDrawing()
    {
        RenderTexture.active = renderTexture;

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = null;

        myRawImage.texture = texture;
    }
}
