using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class GameProcess : NetworkBehaviour
{
    public AssembleDraw assembleDraw;
    public TMP_Text timerText;
    public float drawingTime = 60f;
    private int playerDone = 0;
    private int maxPlayer;
    private bool isDrawingCompleted = false;

    [Header("Easter Egg")]
    private int resetButtonCount = 0;
    public GameObject secret;

    void Start()
    {
        if (isServer)
        {
            maxPlayer = NetworkManager.singleton.numPlayers;

            StartCoroutine(WaitAndCompleteDrawing());
        }
    }

    private IEnumerator WaitAndCompleteDrawing()
    {
        float timeLeft = drawingTime;

        while (timeLeft > 0 && playerDone < maxPlayer && !isDrawingCompleted)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerUI(timeLeft);
            yield return null;
        }

        EndOfTimer();
    }

    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void EndOfTimer()
    {
        assembleDraw.CmdSyncDisplay();
    }

    public void PlayerDrawComplete()
    {
        GameEventsManager.instance.drawingEvents.OnDrawingComplete();

        CmdSyncPlayerFinish();
    }

    [Command]
    private void CmdSyncPlayerFinish()
    {
        playerDone += 1;

        if (playerDone == maxPlayer)
        {
            StopAllCoroutines();
            EndOfTimer();
        }
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