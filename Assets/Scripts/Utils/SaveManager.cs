using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SaveManager : NetworkBehaviour
{
    public AssembleDraw assembleDraw;
    public RenderTexture renderTexture;
    public GameObject myRawImage;

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

        byte[] bArray2 = texture.EncodeToPNG();

        Texture2D imgTest = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        imgTest.LoadImage(bArray2);
        imgTest.Apply();

        myRawImage.GetComponent<RawImage>().texture = imgTest;

    }
}
