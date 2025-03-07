using UnityEngine;
using Mirror;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    // Reference to the custom NetworkManager
    public NetworkManager networkManager;

    // (Optional) UI elements for IP address and status
    public TMP_InputField ipAddressInput;
    public TextMeshProUGUI statusText;
    
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