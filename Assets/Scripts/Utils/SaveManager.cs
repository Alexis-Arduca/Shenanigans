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

        myRawImage.GetComponent<RawImage>().texture = texture;

        assembleDraw.UploadDrawing(myRawImage);

        Vector3 spawnPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        CmdSpawnObjectOnServer(spawnPosition);
    }

    // ---> NEED MORE TEST (Send the drawing at the host)
    [Command]
    void CmdSpawnObjectOnServer(Vector3 position)
    {
        GameObject newObject = Instantiate(myRawImage, position, Quaternion.identity);
        NetworkServer.Spawn(newObject);
    }
    // ---> NEED MORE TEST (Send the drawing at the host)   
}
