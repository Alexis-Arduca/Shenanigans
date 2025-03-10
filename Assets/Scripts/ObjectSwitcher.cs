using UnityEngine;
using UnityEngine.UI;

public class ToggleGameObject : MonoBehaviour
{
    // The GameObject to be toggled
    public GameObject targetObject;

    // Button for turning the object on
    public Button onButton;

    // Button for turning the object off
    public Button offButton;

    void Start()
    {
        // Ensure the onButton is assigned and add a listener for turning on the object
        if (onButton != null)
        {
            onButton.onClick.AddListener(TurnOn);
        }
        else
        {
            Debug.LogWarning("On Button is not assigned in the inspector.");
        }

        // Ensure the offButton is assigned and add a listener for turning off the object
        if (offButton != null)
        {
            offButton.onClick.AddListener(TurnOff);
        }
        else
        {
            Debug.LogWarning("Off Button is not assigned in the inspector.");
        }
    }

    // Function to activate the GameObject
    void TurnOn()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned in the inspector.");
        }
    }

    // Function to deactivate the GameObject
    void TurnOff()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Target GameObject is not assigned in the inspector.");
        }
    }
}
