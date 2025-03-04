using UnityEngine;
using UnityEngine.TerrainTools;

public class SphereTrail : MonoBehaviour
{
    public Material material;
    public RenderTexture texture;
    
    public GameObject brushPrefab;
    public Transform plane;
    
    private void Start()
    {
        material.mainTexture = texture;
    }

    private void Update()
    {
        Paint();
    }
    
    private void Paint()
    {
        if (!brushPrefab || !plane) return;

        // Instantiate a brush
        GameObject brush = Instantiate(brushPrefab, transform.position, Quaternion.identity);
        
        // Make sure it's aligned with the painting plane
        brush.transform.position = new Vector3(transform.position.x, plane.position.y + 0.01f, transform.position.z);
        
        // // Optionally, destroy after a delay to reduce clutter
        // Destroy(brush, 2f);
    }
}
