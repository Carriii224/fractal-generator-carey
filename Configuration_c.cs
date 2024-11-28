using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration_c : MonoBehaviour
{
    public float initialSize = 5f; // Size of the initial square
    public float iterations = 3;    // Number of recursive subdivisions
    public Color squareColor = Color.black; // Color for the squares
    public float angleR;

    public float Iterations
    {
        get { return iterations; }
        set
        {
            iterations = Mathf.Clamp(value, 0, 6); // Limit iterations for performance
            Debug.Log($"Iterations updated to: {iterations}");
            ReminderRegenerate();
        }
    }

    void Start()
    {
        
    }

    public void ReminderRegenerate()
    {
        Debug.Log($"Regenerating shape with iterations={iterations}");
        GenerateCarpet();
    }

    void GenerateCarpet()
    {
        Debug.Log("Generating Sierpinski Carpet");

        // Clear previous objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Define the initial square
        Vector3 bottomLeft = new Vector3(-initialSize / 2f, -initialSize / 2f, 0);

        // Start the recursive subdivision
        Sierpinski(bottomLeft, initialSize, iterations);
    }

    void Sierpinski(Vector3 bottomLeft, float size, float iter)
    {
        if (iter <= 0)
        {
            // Base case: Draw the square
            DrawSquare(bottomLeft, size);
            return;
        }

        // Size of smaller squares
        float newSize = size / 3f;

        // Generate 9 smaller squares, skipping the middle one
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                // Skip the middle square
                if (row == 1 && col == 1)
                    continue;

                // Calculate the bottom-left corner of the smaller square
                Vector3 newBottomLeft = bottomLeft + new Vector3(col * newSize, row * newSize, 0);

                // Recursively subdivide
                Sierpinski(newBottomLeft, newSize, iter - 1);
            }
        }
    }

    void DrawSquare(Vector3 bottomLeft, float size)
    {
        // Create a new GameObject for the square
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Quad);
        square.transform.parent = transform;
        square.transform.localScale = new Vector3(size, size, 1);
        square.transform.position = bottomLeft + new Vector3(size / 2f, size / 2f, 0); // Center the square

        // Set the square's color
        var renderer = square.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Unlit/Color"));
        renderer.material.color = squareColor;
    }
}