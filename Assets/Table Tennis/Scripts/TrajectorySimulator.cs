using UnityEngine;

public class TrajectorySimulator : MonoBehaviour
{
    public Rigidbody ballRigidbody; // Reference to the ball's Rigidbody
    public int resolution = 30; // Number of points to calculate for the trajectory
    private LineRenderer lineRenderer;

    void Start()
    {
        // Ensure the Rigidbody is assigned
        if (ballRigidbody == null)
        {
            ballRigidbody = GetComponent<Rigidbody>();
        }

        // Initialize LineRenderer to visualize trajectory
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = resolution;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Simulate and draw trajectory
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3 gravity = Physics.gravity; // Use Unity's default gravity
        float timestep = 0.1f; // Time interval between each point
        Vector3[] trajectoryPoints = new Vector3[resolution]; // Array to store points
        Vector3 currentVelocity = ballRigidbody.linearVelocity;
        Vector3 currentPosition = ballRigidbody.position;

        for (int i = 0; i < resolution; i++)
        {
            float time = timestep * i;

            // Calculate position using the formula:
            // position = initialPosition + velocity * time + 0.5 * gravity * time^2
            Vector3 calculatedPosition = currentPosition +
                                         currentVelocity * time +
                                         0.5f * gravity * time * time;

            // Store the calculated position
            trajectoryPoints[i] = calculatedPosition;
        }

        // Apply trajectory points to LineRenderer
        lineRenderer.SetPositions(trajectoryPoints);
    }
}
