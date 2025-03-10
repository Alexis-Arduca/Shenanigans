using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelection : MonoBehaviour
{
    public string[] themes;

    public string GetRandomTheme()
    {
        return themes[Random.Range(0, themes.Length)];
    }

    public string[] GetThemes()
    {
        return themes;
    }
}
