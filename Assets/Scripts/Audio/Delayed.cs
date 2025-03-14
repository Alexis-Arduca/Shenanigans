using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    // Reference to the AudioSource component attached to the same GameObject.
    public AudioSource audioSource;
    // Delay in seconds before the audio starts.
    public float delaySeconds = 3f;

    void Start()
    {
        // This plays the attached AudioSource's clip after delaySeconds seconds.
        audioSource.PlayDelayed(delaySeconds);
    }
}
