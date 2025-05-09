using UnityEngine;

public class Highlight : MonoBehaviour
{
    private Color startColor; // Store the original color
    private Renderer objectRenderer; // Cache the Renderer component

    void Start()
    {
        // Get the Renderer component once at the start
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            startColor = objectRenderer.material.color; // Store the initial color
        }
        else
        {
            Debug.LogError("Renderer not found on the GameObject!");
        }
    }

    void OnMouseEnter()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.gray4; // Change color to yellow
        }
    }

    void OnMouseExit()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = startColor; // Revert to the original color
        }
    }
}
