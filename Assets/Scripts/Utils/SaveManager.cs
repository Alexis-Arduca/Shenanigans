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
    public GameObject myPlane;

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

        Texture2D texture = (Texture2D)myPlane.GetComponent<Renderer>().material.mainTexture;

        byte[] bArray2 = texture.EncodeToJPG();

        CmdSyncDrawing(bArray2);
    }

    [Command(requiresAuthority = false)]
    public void CmdSyncDrawing(byte[] drawArray)
    {
        RenderTexture.active = renderTexture;

        Texture2D imgTest = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        imgTest.LoadImage(drawArray);
        imgTest.Apply();

        RenderTexture.active = null;

        GameObject instance = Instantiate(myRawImage);
        instance.GetComponent<RawImage>().texture = imgTest;
        assembleDraw.UploadDrawing(instance);
        Destroy(instance, 0.1f);
    }
}
