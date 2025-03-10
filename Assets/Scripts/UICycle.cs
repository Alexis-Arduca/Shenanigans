using UnityEngine;
using System.Collections;

public class GameObjectCycler : MonoBehaviour
{
    [System.Serializable]
    public class CycleElement
    {
        public GameObject gameObject; // The GameObject to activate
        public float activeTime;      // Time in seconds to keep it active
    }

    public CycleElement[] cycleElements; // Array of GameObjects and their active durations

    private void Start()
    {
        if (cycleElements.Length > 0)
        {
            StartCoroutine(CycleThroughGameObjects());
        }
        else
        {
            Debug.LogWarning("No GameObjects assigned to cycle through.");
        }
    }

    private IEnumerator CycleThroughGameObjects()
    {
        int index = 0;

        while (true)
        {
            // Deactivate all GameObjects
            foreach (var element in cycleElements)
            {
                element.gameObject.SetActive(false);
            }

            // Activate the current GameObject
            cycleElements[index].gameObject.SetActive(true);

            // Wait for the specified active time
            yield return new WaitForSeconds(cycleElements[index].activeTime);

            // Move to the next GameObject in the array
            index = (index + 1) % cycleElements.Length;
        }
    }
}
