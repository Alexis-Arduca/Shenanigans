using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] private GameObject pcMenu;
    [SerializeField] private GameObject mobileMenu;
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (mobileMenu != null) mobileMenu.SetActive(true);
            if (pcMenu != null) pcMenu.SetActive(false);
        }
        else
        {
            if (mobileMenu != null) mobileMenu.SetActive(false);
            if (pcMenu != null) pcMenu.SetActive(true);
        }
    }
}
