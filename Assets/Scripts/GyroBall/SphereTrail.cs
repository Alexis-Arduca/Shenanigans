using UnityEngine;

public class SphereTrail : MonoBehaviour
{
    public Material material;
    public RenderTexture texture;
    
    public GameObject brushPrefab;
    public Transform plane;
    
    private Transform _transform;
    
    private void Start()
    {
        if (!material || !texture)
        {
            Debug.LogError("Material or texture not set!");
            return;
        }
        material.mainTexture = texture;
        _transform = transform;
    }

    private void Update()
    {
        Paint();
    }
    
    private void Paint()
    {
        if (!brushPrefab || !plane) return;

        // Instantiate a brush
        GameObject brush = Instantiate(brushPrefab, _transform.position, Quaternion.identity);
        
        // Make sure it's aligned with the painting plane
        brush.transform.position = new Vector3(_transform.position.x, plane.position.y + 0.01f, _transform.position.z);
    }
}
