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

        newObject.transform.position = new Vector3(-1.5f, 3.5f, 18);

        return themes[myRandom];
    }

    private string[] GetThemes()
    {
        return themes;
    }
}
