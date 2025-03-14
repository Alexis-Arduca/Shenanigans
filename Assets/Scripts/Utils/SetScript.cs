using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SetScript : MonoBehaviour
{
    private GameObject networkManager;

    void Start()
    {
        networkManager = GameObject.Find("NetworkManager");
    }

    public void StopMyHost()
    {
        Debug.Log("Stop Host");
        networkManager.GetComponent<NetworkManager>().StopHost();
    }
}
