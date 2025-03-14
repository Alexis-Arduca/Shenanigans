using UnityEngine;

public class AffiliatePart : MonoBehaviour
{
    public GameObject headPart;
    public GameObject bodyPart;
    public GameObject legsPart;

    void Start()
    {
        ulong playerID = PersistentPlayer.localPlayer.netId;

        headPart.SetActive(false);
        bodyPart.SetActive(false);
        legsPart.SetActive(false);

        if (playerID == 1)
        {
            headPart.SetActive(true);
        }
        else if (playerID == 2)
        {
            bodyPart.SetActive(true);
        }
        else if (playerID == 3)
        {
            legsPart.SetActive(true);
        }
    }
}
