using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public DrawingManager drawingManager;
    public PenManager myBrush;

    void Update()
    {
        // ---> Delete Later (PC Test)
        if (Input.GetMouseButtonDown(0))
        {
            drawingManager.StartDrawing(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && drawingManager.IsDrawing())
        {
            drawingManager.AddAPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drawingManager.SetDrawing(false);
        }
        // ---> Delete Later (PC Test)

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                drawingManager.StartDrawing(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && drawingManager.IsDrawing())
            {
                drawingManager.AddAPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                drawingManager.SetDrawing(false);
            }
        }
    }
}
