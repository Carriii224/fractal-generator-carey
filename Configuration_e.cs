using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration_e : MonoBehaviour
{
    public float size = 5f; // Overall size of the Hilbert Curve
    // My 3 Parameters in the UI: Iterations, Angle, and Number of Division

   
    // DIVISION
    public float divNo;
        public float DivNo
        {
            get { return divNo; }
            set
            {
                divNo = value;
                Debug.Log($"Iterations updated to: {divNo}");
                ReminderRegenerate(); // Automatically update shape when value changes
            }
        }



    // ANGLE
    public float angleR;
    public float AngleR
    {
        get { return angleR; }
        set
        {
            angleR = value;
            Debug.Log($"Iterations updated to: {angleR}");
            ReminderRegenerate(); // Automatically update shape when value changes
        }
    }


    // ITERATION
    public float iterations;
    public float Iterations
    {
        get { return iterations; }
        set
        {
            iterations = value;
            Debug.Log($"Iterations updated to: {iterations}");
            ReminderRegenerate(); // Automatically update shape when value changes
        }
    }

    public void ReminderRegenerate()
    {
        // Logic to regenerate the shape based on the new iterations
        Debug.Log("Regenerating shape with iterations: " + iterations);
        // Your shape update logic here
        GenerateHilbertCurve();
    }
    public float lineWidth = 0.02f; // Width of the LineRenderer

    private List<Vector3> points; // Points that make up the Hilbert Curve

    void Start()
    {
        
    }

    public void GenerateHilbertCurve()
    {
        points = new List<Vector3>();

        // Start the recursive generation
        GenerateHilbert(iterations, Vector3.zero, size, 1);

        // Draw the curve using a LineRenderer
        DrawCurve();
    }

    // Recursive method to generate the Hilbert Curve
    void GenerateHilbert(float iteration, Vector3 position, float size, int orientation)
    {
        if (iteration == 0)
            return;

        size /= 2f; // Halve the size at each recursion

        switch (orientation)
        {
            case 0: // Bottom-left to top-right orientation
                GenerateHilbert(iteration - 1, position, size, 1);
                AddPoint(position + new Vector3(size, 0, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, 0, 0), size, 0);
                AddPoint(position + new Vector3(size, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, size, 0), size, 0);
                AddPoint(position + new Vector3(0, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(0, size, 0), size, 3);
                break;

            case 1: // Top-left to bottom-right orientation
                GenerateHilbert(iteration - 1, position, size, 0);
                AddPoint(position + new Vector3(0, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(0, size, 0), size, 1);
                AddPoint(position + new Vector3(size, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, size, 0), size, 1);
                AddPoint(position + new Vector3(size, 0, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, 0, 0), size, 2);
                break;

            case 2: // Bottom-right to top-left orientation
                GenerateHilbert(iteration - 1, position, size, 3);
                AddPoint(position + new Vector3(size, 0, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, 0, 0), size, 2);
                AddPoint(position + new Vector3(size, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, size, 0), size, 2);
                AddPoint(position + new Vector3(0, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(0, size, 0), size, 0);
                break;

            case 3: // Top-right to bottom-left orientation
                GenerateHilbert(iteration - 1, position, size, 2);
                AddPoint(position + new Vector3(0, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(0, size, 0), size, 3);
                AddPoint(position + new Vector3(size, size, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, size, 0), size, 3);
                AddPoint(position + new Vector3(size, 0, 0));
                GenerateHilbert(iteration - 1, position + new Vector3(size, 0, 0), size, 1);
                break;
        }
    }

    // Adds a point to the list
    void AddPoint(Vector3 point)
    {
        points.Add(point);
    }

    // Draws the curve using a LineRenderer
    void DrawCurve()
    {
        // Remove any existing LineRenderer
        LineRenderer existingLineRenderer = GetComponent<LineRenderer>();
        if (existingLineRenderer != null)
        {
            Destroy(existingLineRenderer);
        }

        // Add and configure a new LineRenderer
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = points.Count;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        lineRenderer.SetPositions(points.ToArray());
        // // Remove any existing LineRenderer;
        // LineRenderer existingLineRenderer = GetComponent<LineRenderer>();
        // if (existingLineRenderer != null)
        // {
        //     Destroy(existingLineRenderer);
        // }

        // // Add a new LineRenderer
        // LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        // if (lineRenderer = null)
        // {
        //     Debug.Log("Line Render doesn't exist");
        // }
        // Debug.Log(points.Count);

        // lineRenderer.Count = points.Count;
        // lineRenderer.useWorldSpace = false;
        // lineRenderer.startWidth = lineWidth;
        // lineRenderer.endWidth = lineWidth;

        // // Set the points for the LineRenderer
        // lineRenderer.SetPositions(points.ToArray());
    }
}
