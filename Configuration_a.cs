using System.Collections.Generic;
using UnityEngine;

public class Configuration_a : MonoBehaviour
{
    public float initialSize = 5f; // Radius of the hexagon

    private List<Vector3> points; // Stores vertices of the Koch snowflake

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
            GenerateSnowflake();
        }




    void Start()
    {
        
    }

    public void GenerateSnowflake()
    {
        Debug.Log("Generating Hexagon Koch Snowflake with iterations: " + iterations);

        // Initialize the starting shape: a regular hexagon
        points = new List<Vector3>();
        for (int i = 0; i < 6; i++) // 6 sides for hexagon
        {
            float angle = Mathf.Deg2Rad * (60 * i); // Angle of each hexagon vertex
            points.Add(new Vector3(Mathf.Cos(angle) * initialSize, Mathf.Sin(angle) * initialSize, 0));
        }

        // Close the loop
        points.Add(points[0]);

        // Apply the Koch algorithm for the specified iterations
        for (int i = 0; i < iterations; i++)
        {
            ApplyKoch();
        }

        // Draw the snowflake
        DrawSnowflake();
    }

    void ApplyKoch()
    {
        var newPoints = new List<Vector3>();

        for (int i = 0; i < points.Count - 1; i++) // Iterate over all edges
        {
            Vector3 p1 = points[i];
            Vector3 p2 = points[i + 1];

            // Add the starting point of the segment
            newPoints.Add(p1);

            // Calculate points a and b (dividing the segment into thirds)
            Vector3 a = Vector3.Lerp(p1, p2, 1f / 3f);
            Vector3 b = Vector3.Lerp(p1, p2, 2f / 3f);

            // Calculate the spike point (c)
            Vector3 direction = (b - a).normalized;
            float length = Vector3.Distance(a, b);
            Vector3 c = a + Quaternion.Euler(0, 0, angleR) * direction * length;

            // Add points in the correct order: a -> c -> b
            newPoints.Add(a);
            newPoints.Add(c);
            newPoints.Add(b);
        }

        // Add the final point to close the loop
        newPoints.Add(points[points.Count - 1]);

        points = newPoints;
    }

    void DrawSnowflake()
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
    }
}