using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class DisplayLocalIP : MonoBehaviour
{
    // Drag your Text Mesh Pro component here in the Inspector.
    public TMP_Text ipDisplay;

    void Start()
    {
        // Get the local IP and update the TMP text.
        string localIP = GetLocalIPAddress();
        ipDisplay.text = localIP;
    }

    // Retrieves the local IPv4 address.
    string GetLocalIPAddress()
    {
        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                // Only return IPv4 addresses.
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No IPv4 address found";
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error retrieving local IP: " + ex.Message);
            return "Error";
        }
    }
}
