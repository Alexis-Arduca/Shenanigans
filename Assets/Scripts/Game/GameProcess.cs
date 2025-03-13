using System.Collections;
using UnityEngine;
using TMPro;

public class GameProcess : MonoBehaviour
{
    public float drawingTime = 120f;

    [Header("Easter Egg")]
    private int resetButtonCount = 0;
    public GameObject secret;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void DrawingStart()
    {
        GameEventsManager.instance.drawingEvents.OnDrawingStart();
        // StartCoroutine(WaitAndCompleteDrawing());
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

    public void EasterEgg()
    {
        resetButtonCount += 1;

        if (resetButtonCount > 30) {
            secret.SetActive(true);
            resetButtonCount = 0;
        } else {
            secret.SetActive(false);
        }
    }
}
