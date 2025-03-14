using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelection : MonoBehaviour
{
    public string[] themes;
    public GameObject[] themesPrefab;
    public GameObject mobileManager;
    public TMPro.TMP_Text themeDisplay;

    void Start()
    {
        themeDisplay.text = $"{GetRandomTheme()}";
    }

    private string GetRandomTheme()
    {
        int myRandom = Random.Range(0, themes.Length);

        GameObject newObject = Instantiate(themesPrefab[myRandom], mobileManager.transform);

        newObject.transform.position = new Vector3(1267f, 359.5f, -674.5f);
        newObject.transform.rotation = Quaternion.Euler(-3.033f, 1.269f, -24.113f);
        newObject.transform.localScale = new Vector3(5000f, 5000f, 3f);

        return themes[myRandom];
    }

    private string[] GetThemes()
    {
        return themes;
    }
}
