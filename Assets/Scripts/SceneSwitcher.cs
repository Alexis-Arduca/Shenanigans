using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SceneSwitcher : NetworkBehaviour
{
    public static SceneSwitcher Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSwitchScene(string sceneName)
    {
        string cleanName = GetCleanSceneName(sceneName);
        Debug.Log($"Attempting to switch to: {cleanName}");

        if (IsSceneValid(cleanName))
        {
            NetworkServer.SetAllClientsNotReady();
            NetworkManager.singleton.ServerChangeScene(cleanName);
        }
    }

    private bool IsSceneValid(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string nameInBuild = System.IO.Path.GetFileNameWithoutExtension(path);

            Debug.Log($"Build Index {i}: {nameInBuild}");

            if (nameInBuild == sceneName)
                return true;
        }
        return false;
    }

    private string GetCleanSceneName(string input)
    {
        // Remove path and extension if present
        return System.IO.Path.GetFileNameWithoutExtension(input);
    }
}