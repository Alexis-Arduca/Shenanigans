using UnityEngine;
using Mirror;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    // Reference to the custom NetworkManager
    public NetworkManager networkManager;

    // UI elements for the PC and mobile menus
    [SerializeField] private GameObject pcMenu;
    [SerializeField] private GameObject mobileMenu;

    // (Optional) UI elements for IP address and status
    public TMP_InputField ipAddressInput;
    public TextMeshProUGUI statusText;

    private void Start()
    {
        // Automatically switch UI based on the platform
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // Running on a mobile device: show mobile UI
            if (mobileMenu != null) mobileMenu.SetActive(true);
            if (pcMenu != null) pcMenu.SetActive(false);
        }
        else
        {
            // Running on PC/Standalone: show PC UI
            if (mobileMenu != null) mobileMenu.SetActive(false);
            if (pcMenu != null) pcMenu.SetActive(true);
        }
    }

    public void StartHost()
    {
        Debug.Log("Starting as Host (PC)");
        networkManager.StartHost();
        if (statusText != null)
            statusText.text = "Host started.";
    }

    public void StartClient()
    {
        if (ipAddressInput != null && !string.IsNullOrEmpty(ipAddressInput.text))
        {
            networkManager.networkAddress = ipAddressInput.text;
        }
        else
        {
            networkManager.networkAddress = "YOUR_PC_IP_HERE";
        }
        Debug.Log("Starting client with address: " + networkManager.networkAddress);
        networkManager.StartClient();
        if (statusText != null)
            statusText.text = "Client connecting...";
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}