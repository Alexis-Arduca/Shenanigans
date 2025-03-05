using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class deviceManager : MonoBehaviour
{
    [SerializeField] private GameObject pcMenu;
    [SerializeField] private GameObject mobileMenu;
    // Start is called before the first frame update
    private void Start()
    {
        // Automatically switch UI based on the platform
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // Running on a mobile device: show mobile UI
            if (mobileMenu != null) mobileMenu.SetActive(true);
            if (pcMenu != null) pcMenu.SetActive(false);
        }
        else
        {
            // Running on PC/Standalone: show PC UI
            if (mobileMenu != null) mobileMenu.SetActive(false);
            if (pcMenu != null) pcMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
