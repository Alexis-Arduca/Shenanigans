using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SubtractiveSceneLoader : MonoBehaviour
{
#if UNITY_EDITOR
    [Tooltip("Drag a scene asset from your project folder here (Editor only).")]
    public SceneAsset sceneAsset;
#endif

    [Tooltip("Name of the scene to unload additively. This is set automatically if you assign a SceneAsset above.")]
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

    // Call this method to unload the scene.
    public void Unload()
    {
        // Check if the scene is loaded before attempting to unload it.
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            Debug.Log("Unloading scene: " + sceneName);
        }
        else
        {
            Debug.LogWarning("Scene '" + sceneName + "' is not loaded or does not exist.");
        }
    }
}
