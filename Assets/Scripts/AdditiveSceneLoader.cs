using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AdditiveSceneLoader : MonoBehaviour
{
#if UNITY_EDITOR
    [Tooltip("Drag a scene asset from your project folder here (Editor only).")]
    public SceneAsset sceneAsset;
#endif

    [Tooltip("Name of the scene to load additively. This is set automatically if you assign a SceneAsset above.")]
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

    public void LoadSceneAdditively()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Loading scene '{sceneName}' additively...");
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogError("No scene specified. Please assign a scene asset or set the scene name.");
        }
    }
}