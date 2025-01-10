using UnityEngine;
using System.Collections.Generic;


public class TrajectorySimulator : MonoBehaviour
{
    public Rigidbody ballRigidbody; // Reference to the ball's Rigidbody
    public int resolution = 30; // Number of points to calculate for the trajectory
    private LineRenderer lineRenderer;

    // List to store the ball's state (position and velocity) at regular intervals
    private List<(Vector3 position, Vector3 velocity)> trajectoryData;


    void Start()
    {
        // Ensure the Rigidbody is assigned
        if (ballRigidbody == null)
        {
            ballRigidbody = GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                Debug.LogError("Rigidbody not found! Please assign a Rigidbody to the ball.");
                return;
            }
        }

        // Initialize LineRenderer to visualize trajectory
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null) // Make sure lineRenderer is initialize
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = resolution;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Initialize the trajectory data list
        trajectoryData = new List<(Vector3, Vector3)>();

        // Simulate and draw trajectory
        DrawTrajectory();
        PrintTrajectoryData();
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

            // Calculate velocity at this point (v = u + g * t)
            Vector3 calculatedVelocity = currentVelocity + gravity * time;

            // Store the calculated position and velocity in the trajectory data
            trajectoryData.Add((calculatedPosition, calculatedVelocity));

            // Store the calculated position
            trajectoryPoints[i] = calculatedPosition;
        }

        // Apply trajectory points to LineRenderer
        lineRenderer.SetPositions(trajectoryPoints);
    }
    // Print the trajectory data for debugging
    void PrintTrajectoryData()
    {
        UnityEngine.Debug.Log("Trajectory Data:");
        foreach (var data in trajectoryData)
        {
            UnityEngine.Debug.Log($"Position: {data.position}, Velocity: {data.velocity}");
        }
    }
}
