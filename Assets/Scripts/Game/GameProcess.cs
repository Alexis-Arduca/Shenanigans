using System.Collections;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    public float drawingTime = 120f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DrawingStart()
    {
        GameEventsManager.instance.drawingEvents.OnDrawingStart();
        StartCoroutine(WaitAndCompleteDrawing());
    }

    private IEnumerator WaitAndCompleteDrawing()
    {
        yield return new WaitForSeconds(drawingTime);
        DrawingDone();
    }

    public void DrawingDone()
    {
        GameEventsManager.instance.drawingEvents.OnDrawingComplete();
    }    
}
