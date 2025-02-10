using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;




[RequireComponent(typeof(Rigidbody))]
public class RandTrajectory : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0.312f, 1, 1.334f); // Starting position of ball 
    public Vector3 initialVelocity = new Vector3(0, 5, 2); // Initial velocity
    public Vector3 initialSpin = new Vector3(0, 0, 0); // Spin 
    public int trajectoryResolution = 11; // Number of points for trajectory visualization
    public float logInterval = 0.5f; // Time interval for logging position and velocity

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

        // Start logging ball state
        StartCoroutine(LogBallState());
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

        lineRenderer.positionCount = trajectoryResolution;

        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = timestep * i;

            Vector3 calculatedPosition = currentPosition +
                                         currentVelocity * time +
                                         0.5f * gravity * time * time;

            lineRenderer.SetPosition(i, calculatedPosition);
        }
    }
    System.Collections.IEnumerator LogBallState()
    {
        while (true)
        {
            // Log the current position and velocity of the ball
            Vector3 currentPosition = ballRigidbody.position;
            Vector3 currentVelocity = ballRigidbody.linearVelocity;

            UnityEngine.Debug.Log($"Time: {Time.time:F2} - Position: {currentPosition} - Velocity: {currentVelocity}");

            // Wait for the specified interval before logging again
            yield return new WaitForSeconds(logInterval);
        }
    }
    
    
    //AUTOMATICALLY CALLED ON COLLISION WITH FLOOR
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FloorArea"))
        {
            LaunchNewBall();
        }
    }
    
    public void LaunchNewBall()
    {
        // Randomize initial position within desired range
        Vector3 randomPosition = new Vector3(
            Random.Range(-0.9f, 0.9f), // Adjust range X
            Random.Range(1, 1.4f), // Adjust range Y
            Random.Range(-2f, -1.4f)  // Adjust range Z
        );

        // Randomize initial velocity
        Vector3 randomVelocity = new Vector3(
            Random.Range(-0.6f, 0.6f), // Adjust range X
            Random.Range(3f, 4.0f),   // Adjust range Y
            Random.Range(2.0f, 4f)    // Adjust range Z
        );

        // Randomize initial spin
        Vector3 randomSpin = new Vector3(
            Random.Range(-5.0f, 5.0f),
            Random.Range(-5.0f, 5.0f),
            Random.Range(-5.0f, 5.0f)
        );

        // Reset ball's position and physics properties
        ballRigidbody.position = randomPosition;
        ballRigidbody.linearVelocity = randomVelocity;
        ballRigidbody.angularVelocity = randomSpin;

        // Optionally, reset the ball's rotation
        ballRigidbody.rotation = Quaternion.identity;
    }


}
