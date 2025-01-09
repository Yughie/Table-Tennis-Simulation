using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallTrajectoryController : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0.312f, 1, 1.334f); // Starting position of ball 
    public Vector3 initialVelocity = new Vector3(0, 5, 2); // Initial velocity
    public Vector3 initialSpin = new Vector3(0, 0, 0); // Spin 
    public int trajectoryResolution = 11; // Number of points for trajectory visualization

    private Rigidbody ballRigidbody;
    private LineRenderer lineRenderer;

    void Start()
    {
        // assigned rigidbody
        ballRigidbody = GetComponent<Rigidbody>();


        ballRigidbody.position = initialPosition;

        ballRigidbody.linearVelocity = initialVelocity;
        ballRigidbody.angularVelocity = initialSpin;

        // line for trajectory
        InitializeLineRenderer();

        // line visual for trajectory
        DrawTrajectory();
    }

    void InitializeLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = trajectoryResolution;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;  
        lineRenderer.endColor = Color.green;  
    }

    void DrawTrajectory()
    {
        Vector3 gravity = Physics.gravity; // unity's gravity
        float timestep = 0.1f; // time interval between points
        Vector3 currentPosition = initialPosition;
        Vector3 currentVelocity = initialVelocity;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = timestep * i;
            
            Vector3 calculatedPosition = currentPosition +
                                         currentVelocity * time +
                                         0.5f * gravity * time * time;

            lineRenderer.SetPosition(i, calculatedPosition);
        }
    }
}
