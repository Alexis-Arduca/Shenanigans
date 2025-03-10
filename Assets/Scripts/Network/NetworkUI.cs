using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro; // Import TextMeshPro namespace

public class NetworkUI : MonoBehaviour
{
    public NetworkManager manager; // Reference to NetworkManager
    public Button hostButton;
    public Button joinButton;
    public TMP_InputField ipAddressInput; // Use TMP_InputField for TMP
    //public TMP_Text statusText; // Use TMP_Text instead of Text

    void Start()
    {
        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(JoinGame);
    }

    void StartHost()
    {
        manager.StartHost();
        //statusText.text = "Hosting game...";
    }

    void JoinGame()
    {
        string ipAddress = ipAddressInput.text;
        if (!string.IsNullOrEmpty(ipAddress))
        {
            manager.networkAddress = ipAddress;
            manager.StartClient();
            //statusText.text = "Connecting to " + ipAddress + "...";
        }
        else
        {
            //statusText.text = "Please enter a valid IP address!";
        }
    }
}
