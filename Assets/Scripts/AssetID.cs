using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetID : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // this instance asset path
        var path = AssetDatabase.GetAssetPath(this.GetInstanceID());
        Debug.Log("Asset path:" + path);

        // Get asset with path
        var asset = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject));
        Debug.Log("Asset instance id:" + asset.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
