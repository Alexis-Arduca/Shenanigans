using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleDraw : MonoBehaviour
{
    public GameObject topDrawing;
    public GameObject midDrawing;
    public GameObject botDrawing;

    void Start()
    {
        topDrawing.SetActive(false);
        midDrawing.SetActive(false);
        botDrawing.SetActive(false);
    }

    public void UploadDrawing(Texture2D drawText, int id)
    {
        if (id == 1) {
            topDrawing.SetActive(true);
            topDrawing.GetComponent<RawImage>().texture = drawText;
            topDrawing.SetActive(false);
        } else if (id == 2) {
            midDrawing.SetActive(true);
            midDrawing.GetComponent<RawImage>().texture = drawText;
            midDrawing.SetActive(false);
        } else {
            botDrawing.SetActive(true);
            botDrawing.GetComponent<RawImage>().texture = drawText;
            botDrawing.SetActive(false);
        }
    }

    public void CmdSyncDisplay()
    {
        topDrawing.SetActive(true);
        midDrawing.SetActive(true);
        botDrawing.SetActive(true);
    }
}
