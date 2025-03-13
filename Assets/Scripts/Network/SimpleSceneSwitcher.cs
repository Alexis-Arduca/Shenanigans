using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleSceneSwitcher : MonoBehaviour
{
#if UNITY_EDITOR
    [Tooltip("Drag a scene asset from your project folder here (Editor only).")]
    public SceneAsset sceneAsset;
#endif

    [Tooltip("Name of the scene to switch to. This is set automatically if you assign a SceneAsset above.")]
    public string sceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneAsset != null)
        {
            sceneName = sceneAsset.name;
        }
    }
#endif

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Switching to scene '{sceneName}'...");
            // Use Single mode to switch scenes (i.e., close the current scene)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("No scene specified. Please assign a scene asset or set the scene name.");
        }
    }
}
