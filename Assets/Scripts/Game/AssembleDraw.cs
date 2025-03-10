using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleDraw : MonoBehaviour
{
    private List<GameObject> partDrawing = new List<GameObject>();

    public void UploadDrawing(GameObject drawing)
    {
        partDrawing.Add(drawing);
    }

    public void DisplayFinalDraw()
    {
        /// =========================================================== \\\
        /// Here the goal is too get how many image we have to display. \\\
        /// And with that information we have to place the image from   \\\
        /// the top one to the last one. After that we can display them \\\
        ///                                                             \\\
        /// But we know there will a 2/3/4 players modes so we have to  \\\
        /// adapt for each gamemode.                                    \\\
        /// =========================================================== \\\

        for (int i = 0; i < partDrawing.Count; i++) {
            partDrawing[i].SetActive(true);
        }
    }
}
