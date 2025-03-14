using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;  // Assign your sound clip in the Inspector.
    private AudioSource audioSource;

    void Start()
    {
        // Try to get an AudioSource attached to the button; if none, add one.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // This method is called when the pointer enters the button area.
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
