using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class AssembleDraw : NetworkBehaviour
{
    private List<GameObject> partDrawing = new List<GameObject>();

    public void UploadDrawing(GameObject drawing)
    {
        partDrawing.Add(drawing);
    }

    public void DisplayFinalDraw()
    {
        CmdSyncDisplay();
    }

    [Command(requiresAuthority = false)]
    public void CmdSyncDisplay()
    {
        for (int i = 0; i < partDrawing.Count; i++) {
            Instantiate(partDrawing[i], new Vector3(i * 2.0f, 0, 0), Quaternion.identity);
        }
    }
}
