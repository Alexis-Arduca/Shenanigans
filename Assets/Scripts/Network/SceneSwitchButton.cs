using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SceneSwitchButton : MonoBehaviour
{
    [Scene]
    public string targetScene;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (SceneSwitcher.Instance != null)
        {
            // Pass only the scene name without path
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(targetScene);
            SceneSwitcher.Instance.CmdSwitchScene(sceneName);
        }
    }
}