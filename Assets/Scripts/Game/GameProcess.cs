using System.Collections;
using UnityEngine;
using TMPro;

public class GameProcess : MonoBehaviour
{
    public ThemeSelection theme;
    public float drawingTime = 120f;
    public TMPro.TMP_Text themeDisplay;

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
        themeDisplay.text = $"Your unique theme: {theme.GetRandomTheme()}";
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
