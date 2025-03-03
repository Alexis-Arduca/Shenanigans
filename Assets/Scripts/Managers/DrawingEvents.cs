using System;
using UnityEngine;

public class DrawingEvents
{
    public event Action onDrawingStart;
    public void OnDrawingStart()
    {
        if (onDrawingStart != null)
        {
            onDrawingStart();
        }
    }

    public event Action onDrawingComplete;
    public void OnDrawingComplete()
    {
        if (onDrawingComplete != null)
        {
            onDrawingComplete();
        }
    }
}
