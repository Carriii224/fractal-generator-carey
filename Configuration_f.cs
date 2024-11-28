using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration_f : MonoBehaviour
{
    public float iterations = 3; // Number of recursion levels
    public float initialRadius = 5f; // Radius of the initial circle
    public float scaleFactor = 0.5f; // Scaling factor for child circles
    public int circleSegments = 100; // Number of line segments to draw each circle
    public float angleR;
    private List<GameObject> circleObjects = new List<GameObject>(); // Stores the generated circles

    // Properties to dynamically update parameters
    private float _iterations;
    public float Iterations
    {
        get => iterations;
        set
        {
            iterations = value;
            Debug.Log($"Iterations updated to: {iterations}");
            ReminderRegenerate();
        }
    }

    private float _initialRadius;
    public float InitialRadius
    {
        get => initialRadius;
        set
        {
            initialRadius = value;
            Debug.Log($"InitialRadius updated to: {initialRadius}");
            ReminderRegenerate();
        }
    }

    private float _scaleFactor;
    public float ScaleFactor
    {
        get => scaleFactor;
        set
        {
            scaleFactor = value;
            Debug.Log($"ScaleFactor updated to: {scaleFactor}");
            ReminderRegenerate();
        }
    }

    public void ReminderRegenerate()
    {
        Debug.Log("Regenerating fractal...");
        ClearExistingCircles(); // Clear any existing fractal circles
        DrawCircleFractal(Vector3.zero, initialRadius, iterations); // Generate new fractal
    }

    void Start()
    {
        // Initial fractal generation on start
    }

    // Recursive function to draw the fractal
    void DrawCircleFractal(Vector3 position, float radius, float depth)
    {
        if (depth == 0)
            return;

        // Draw the current circle
        DrawCircle(position, radius);

        // Determine the positions of the smaller circles
        float childRadius = radius * scaleFactor; // Scale down the child circles
        float distanceFromCenter = radius + childRadius; // Position at the edge of the parent circle

        // Calculate positions for child circles (4 positions around the parent)
        Vector3[] childPositions = new Vector3[]
        {
            position + new Vector3(0, distanceFromCenter, 0), // Top
            position + new Vector3(distanceFromCenter, 0, 0), // Right
            position + new Vector3(0, -distanceFromCenter, 0), // Bottom
            position + new Vector3(-distanceFromCenter, 0, 0), // Left
        };

        // Recurse for each child circle
        foreach (var childPosition in childPositions)
        {
            DrawCircleFractal(childPosition, childRadius, depth - 1);
        }
    }

    // Draw a single circle using a LineRenderer
    void DrawCircle(Vector3 center, float radius)
    {
        // Create a new GameObject for the LineRenderer
        GameObject circleObj = new GameObject("Circle");
        LineRenderer lineRenderer = circleObj.AddComponent<LineRenderer>();

        lineRenderer.positionCount = circleSegments + 1; // Add an extra point to close the loop
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        // Generate points for the circle
        for (int i = 0; i <= circleSegments; i++)
        {
            float angleR = i * Mathf.PI * 2f / circleSegments; // Divide the circle into equal segments
            float x = Mathf.Cos(angleR) * radius + center.x;
            float y = Mathf.Sin(angleR) * radius + center.y;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }

        // Store the created GameObject for future cleanup
        circleObjects.Add(circleObj);
    }

    // Clears all previously created circles
    void ClearExistingCircles()
    {
        foreach (var obj in circleObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        circleObjects.Clear();
    }
}
