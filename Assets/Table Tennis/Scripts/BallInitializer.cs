using UnityEngine;

public class BallInitializer : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0, 1, 0); // Default position above the table
    public Vector3 initialVelocity = new Vector3(0, 0, 1); // Initial speed forward
    public Vector3 initialSpin = new Vector3(0, 10, 0);    // Spin around the Y-axis

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Initialize the position
        transform.position = initialPosition;

        // Set the initial velocity
        rb.linearVelocity = initialVelocity;

        // Apply the spin (angular velocity)
        rb.angularVelocity = initialSpin;
    }
    void update()
    {

    }
}
