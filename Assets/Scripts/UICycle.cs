using UnityEngine;
using System.Collections;
using Mirror; // Needed for network commands

public class TutorialScreenCycler : MonoBehaviour
{
    [System.Serializable]
    public class CycleElement
    {
        public GameObject gameObject; // The GameObject to activate
        public float activeTime;      // How long to keep it active (in seconds)
    }

    public CycleElement[] cycleElements;  // List of elements to cycle through
    [Scene] public string targetScene;    // Scene to switch to (set in inspector)

    private void Start()
    {
        if (cycleElements.Length > 0)
        {
            StartCoroutine(CycleAndSwitchScene());
        }
        else
        {
            Debug.LogWarning("No GameObjects assigned to cycle through.");
        }
    }

    private IEnumerator CycleAndSwitchScene()
    {
        // Cycle through each element once.
        for (int i = 0; i < cycleElements.Length; i++)
        {
            // Deactivate all GameObjects
            foreach (var element in cycleElements)
            {
                element.gameObject.SetActive(false);
            }

            // Activate the current GameObject
            cycleElements[i].gameObject.SetActive(true);

            // Wait for its active duration
            yield return new WaitForSeconds(cycleElements[i].activeTime);
        }

        // After the final element, switch scenes via Mirror.
        if (SceneSwitcher.Instance != null)
        {
            // Get scene name without path/extension
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(targetScene);
            SceneSwitcher.Instance.CmdSwitchScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneSwitcher instance not found. Unable to switch scene.");
        }
    }
}
