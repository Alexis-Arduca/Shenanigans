using UnityEngine;

public class SphereTrail : MonoBehaviour
{
    public Material planeMaterial;
    public RenderTexture texture;
    public Transform plane;
    
    public Material lineMaterial;
    private LineRenderer _lineRenderer;
    private Transform _transform;
    private Vector3 _lastPosition;
    
    private void Start()
    {
        if (!planeMaterial || !texture)
        {
            #if UNITY_EDITOR
                Debug.LogError("Material or texture not set!");
            #endif
            return;
        }
        planeMaterial.mainTexture = texture;
        _transform = transform;
        _lastPosition = _transform.position;
        InitLineRenderer();
    }

    private void InitLineRenderer()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        
        // Set material
        _lineRenderer.material = lineMaterial;
        
        // Set width
        _lineRenderer.startWidth = 1;
        _lineRenderer.endWidth = 0.5f;
        
        _lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        Paint();
    }
    
    private void Paint()
    {
        if (!plane) return;
        
        Vector3 currentPosition = new Vector3(_transform.position.x, plane.position.y + 0.01f, _transform.position.z);

        // Add a new point to the line if the position has changed
        if (currentPosition != _lastPosition)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, currentPosition);
            _lastPosition = currentPosition;
        }
    }
}
