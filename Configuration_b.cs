using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Configuration_b : MonoBehaviour
{
    public float initialSize = 5f; // Size of the initial triangle, 1 to start with

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
        GenerateSnowflake();
    }



   private List<Vector3> points; // Stores the vertices of the Koch snowflake


   void Start()
   {   //Add in debugging message to check if the divisions number are key in correctly
    
    if (divNo < 3)
    {
        Debug.LogWarning("divNo must be at least 3. Setting it to 3.");
        divNo = 3;
    }
    if (iterations < 0)
    {
        Debug.LogWarning("Iterations must be non-negative. Setting it to 0.");
        iterations = 0;
    }
    }



   public void GenerateSnowflake()
   {    
       Debug.Log("Generating Snowflake with iterations: " + iterations);
       // Initalize the starting shape: a equilateral triangle
       points = new List<Vector3>
       {
        //    new Vector3(-initialSize / 2f, -initialSize * Mathf.Sqrt(3) / 6f, 0),
        //    new Vector3(initialSize / 2f, -initialSize * Mathf.Sqrt(3) / 6f, 0),
           new Vector3(-initialSize / 2f, 0, 0),
           new Vector3(initialSize / 2f, 0, 0),
           new Vector3(0, initialSize * Mathf.Sqrt(3) / 2f, 0),
       };

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

       // geometry-level: to iterate through all segments of current shape (points.Count = No. of sides)
       for (int i = 0; i < points.Count - 1; i++)
       {   
           // single-side-level: a list to store all the points found on one side
           var divPoints = new List<Vector3>();
           // fetch the points on the 2 ends of one side
           Vector3 p1 = points[i]; //Start
           Vector3 p2 = points[i + 1]; //End
           newPoints.Add(p1);
           divPoints.Add(p1);
            
           // define a step-size based on the division numbers given
           float stepSize = 1f/divNo;

           // find the segmented points lie on the one segment, and update the list everytime
           for (int j = 1; j < divNo; j++)
           {
            divPoints.Add(Vector3.Lerp(p1, p2, stepSize*j));
           }


        //    Vector3 a = Vector3.Lerp(p1, p2, 1f / 3f);
        //    Vector3 b = Vector3.Lerp(p1, p2, 2f / 3f);


           // Now we have all points along the one side
           // Interate to find the spike points generated out of the line of the side
        //    var spikePoints = new List<Vector3>();
           for (int k = 1; k < divNo-1; k++ )
           {
            Vector3 a = divPoints[k];
            Vector3 b = divPoints[k+1];
            // Calculate the distance & length based on the division in between
            Vector3 direction = (b - a).normalized;
            float length = Vector3.Distance(a,b);
            // Generate c which is the Spike Point
            Vector3 c = a + Quaternion.Euler(0, 0, -angleR) * direction * length;

            // Add new points in order: p1->a->c->b
            newPoints.Add(a);
            newPoints.Add(c);

           }
           newPoints.Add(divPoints[divPoints.Count-1]);
           
        //    Vector3 direction = (b - a).normalized;
        //    float length = Vector3.Distance(a, b);
        //    Vector3 c = a + Quaternion.Euler(0, 0, -angleR) * direction * length;

        //    newPoints.Add(p1);
        //    newPoints.Add(a);
        //    newPoints.Add(c);
        //    newPoints.Add(b);
            // Store all the points on one side
            
       }
    //    newPoints.Add(points[points.Count - 1]);
       // Add the final point in the current shape to close the loop
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
