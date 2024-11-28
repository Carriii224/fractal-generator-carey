using UnityEngine;

public class Configuration_d : MonoBehaviour
{
    public float iterations = 5;  // Number of recursion levels
    public float size = 5f;     // Size of the initial triangle
    public Material triangleMaterial;
    public RectTransform canvasRect;
    public float angleR;

    // Start with the initial triangle
    private Vector3 p1, p2, p3;

    void Awake()
    {   
        p1 = new Vector3(-size / 2, -Mathf.Sqrt(3) * size / 6, 0); // Bottom-left
        p2 = new Vector3(size / 2, -Mathf.Sqrt(3) * size / 6, 0);  // Bottom-right
        p3 = new Vector3(0, Mathf.Sqrt(3) * size / 3, 0);          // Top
    }

    void Start()
    {
         if (triangleMaterial == null)
        {
        // Create a default material if none is assigned
        triangleMaterial = new Material(Shader.Find("Unlit/Color"));
        triangleMaterial.color = Color.green; // Set the colour to green or any other
        }
        
    }

    public void ReminderRegenerate()
    {   Debug.Log("ReminderRegenerate called");
        Sierpinski_(p1, p2, p3, iterations);
    }

    void Sierpinski_(Vector3 p1, Vector3 p2, Vector3 p3, float depth)
    {
        if (depth == 0)
        {
            CreateTriangle(p1, p2, p3);
        }
        else
        {
            // Calculate midpoints
            Vector3 mid1 = (p1 + p2) / 2;
            Vector3 mid2 = (p2 + p3) / 2;
            Vector3 mid3 = (p3 + p1) / 2;

            // Recursive calls
            Sierpinski_(p1, mid1, mid3, depth - 1);
            Sierpinski_(mid1, p2, mid2, depth - 1);
            Sierpinski_(mid3, mid2, p3, depth - 1);
        }
    }

    void CreateTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Debug.Log($"Creating triangle: p1={p1}, p2={p2}, p3={p3}");
        // Create a new GameObject with a MeshFilter and MeshRenderer
        GameObject triangle = new GameObject("Triangle");
        MeshFilter meshFilter = triangle.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = triangle.AddComponent<MeshRenderer>();

        // Assign the material
        meshRenderer.material = triangleMaterial;

        // Create the triangle mesh
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] { p1, p2, p3 };
        mesh.triangles = new int[] { 0, 1, 2 };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
}
