using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class GameProcess : NetworkBehaviour
{
    public AssembleDraw assembleDraw;
    public float drawingTime = 120f;
    private int playerDone = 0;
    private int maxPlayer;
    private bool isDrawingCompleted = false;

    [Header("Easter Egg")]
    private int resetButtonCount = 0;
    public GameObject secret;

    void Start()
    {
        maxPlayer = NetworkManager.singleton.numPlayers;

        GameEventsManager.instance.drawingEvents.OnDrawingStart();
        StartCoroutine(WaitAndCompleteDrawing());
    }

    private IEnumerator WaitAndCompleteDrawing()
    {
        float timeLeft = drawingTime;

        while (timeLeft > 0 && playerDone < maxPlayer && !isDrawingCompleted)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        EndOfTimer();
    }

    public void EndOfTimer()
    {
        assembleDraw.CmdSyncDisplay();
    }

    public void PlayerDrawComplete()
    {
        CmdSyncPlayerFinish();
        GameEventsManager.instance.drawingEvents.OnDrawingComplete();
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