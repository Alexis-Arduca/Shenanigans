using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelection : MonoBehaviour
{
    public string[] themes;
    public TMPro.TMP_Text themeDisplay;

    void Start()
    {
        themeDisplay.text = $"Your unique theme: {GetRandomTheme()}";
    }

    private string GetRandomTheme()
    {
        return themes[Random.Range(0, themes.Length)];
    }

    private string[] GetThemes()
    {
        return themes;
    }
}
