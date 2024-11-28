using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    // A list to store references to the disabled GameObjects
    private List<GameObject> disabledObjects = new List<GameObject>();

    // This function will be called to disable (hide) the rendered objects
    public void ClearAllRenderedObjects()
    {
        // Find all GameObjects with a LineRenderer component
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            GameObject obj = lineRenderer.gameObject;
            obj.SetActive(false); // Disable the GameObject
            disabledObjects.Add(obj); // Add to the list for later reactivation
        }

        // Find all GameObjects with a MeshRenderer component
        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            GameObject obj = meshRenderer.gameObject;
            obj.SetActive(false); // Disable the GameObject
            disabledObjects.Add(obj); // Add to the list for later reactivation
        }

        Debug.Log("All rendered objects temporarily cleared.");
    }

    // This function will be called to re-enable the rendered objects
    public void RestoreAllRenderedObjects()
    {
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(true); // Re-enable the GameObject
        }

        disabledObjects.Clear(); // Clear the list after restoring
        Debug.Log("All rendered objects restored.");
    }
}