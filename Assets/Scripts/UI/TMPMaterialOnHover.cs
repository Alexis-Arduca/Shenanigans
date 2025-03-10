using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TMPHoverMaterialChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Reference to the TextMesh Pro component (for UI, use TextMeshProUGUI)
    public TextMeshProUGUI tmpText;

    // The material you want to use when the pointer is over the element
    public Material hoverMaterial;

    // To store the default material preset
    private Material defaultMaterial;

    private void Awake()
    {
        // Get the TMP component if not manually assigned
        if (tmpText == null)
            tmpText = GetComponent<TextMeshProUGUI>();

        // It's a good idea to instantiate a copy of the default material to avoid unintended modifications to shared materials.
        defaultMaterial = Instantiate(tmpText.fontMaterial);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the material preset when hovered over
        if (hoverMaterial != null)
            tmpText.fontMaterial = hoverMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to the default material preset when not hovered over
        tmpText.fontMaterial = defaultMaterial;
    }
}
