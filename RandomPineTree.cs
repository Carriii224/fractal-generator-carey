using UnityEngine;

public class RandomPineTree : MonoBehaviour
{
    public GameObject branchPrefab; // Prefab for the branch (e.g., a thin rectangular cube)
    public float branchLength = 2f; // Initial length of the branch
    public float scaleFactor = 0.5f; // Initial length of the branch
    
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
        // Logic to regenerate the shape based on the new iterations/angle/division
        Debug.Log("Regenerating shape with iterations: " + iterations);
        Debug.Log("Regenerating shape with iterations: " + angleR);
        // Clear all existing branches
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Regenerate the fractal tree
        GenerateBranch(Vector3.zero, Quaternion.identity, branchLength, 0);
    }
    
    void Start()
    {
        
    }

    private void GenerateBranch(Vector3 position, Quaternion rotation, float length, float depth)
    {
        if (depth >= iterations) return; // Stop recursion at max depth

        // Create the branch
        GameObject branch = Instantiate(branchPrefab, position, rotation);
        branch.transform.localScale = new Vector3(length * 0.2f, length, 0.2f); // Thin vertical branch
        branch.transform.parent = transform; // Parent for hierarchy organisation

        // Calculate the end position of the branch
        Vector3 endPosition = position + rotation * Vector3.up * length;

        // Create the left branch
        Quaternion leftRotation = rotation * Quaternion.Euler(0, 0, angleR);
        GenerateBranch(endPosition, leftRotation, length * scaleFactor, depth + 1);

        // Create the middle branch (straight up)
        GenerateBranch(endPosition, rotation, length * scaleFactor, depth + 1);

        // Create the right branch
        Quaternion rightRotation = rotation * Quaternion.Euler(0, 0, -angleR);
        GenerateBranch(endPosition, rightRotation, length * scaleFactor, depth + 1);
    }
}
