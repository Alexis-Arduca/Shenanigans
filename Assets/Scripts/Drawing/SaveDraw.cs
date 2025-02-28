using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDraw : MonoBehaviour
{
    public RenderTexture renderTexture;
    public RawImage myRawImage;

    // Update is called once per frame
    void Update()
    {
        
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
