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
    public List<GameObject> finalsDrawing = new List<GameObject>();
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
        // RenderTexture.active = renderTexture;

        // Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        // texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        // texture.Apply();

        // RenderTexture.active = null;

        Texture2D texture = (Texture2D)myPlane.GetComponent<Renderer>().material.mainTexture;

        byte[] bArray2 = texture.EncodeToJPG();

        myRawImage.GetComponent<RawImage>().texture = texture;

        // CmdSyncDrawing(bArray2);
    }

    [Command]
    public void CmdSyncDrawing(byte[] drawArray)
    {
        RpcSyncDrawing(drawArray);
    }

    [ClientRpc]
    public void RpcSyncDrawing(byte[] drawArray)
    {
        RenderTexture.active = renderTexture;

        Texture2D imgTest = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        imgTest.LoadImage(drawArray);
        imgTest.Apply();

        RenderTexture.active = null;

        // ---> Have to Update That to be use for the final draw presentation
        myRawImage.GetComponent<RawImage>().texture = imgTest;
        finalsDrawing.Add(myRawImage);
        // ---> Have to Update That to be use for the final draw presentation
    }
}
