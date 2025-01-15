using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class BallData
{
    public float time;
    public Vector3 position;
    public Vector3 velocity;
    

    public BallData(float time, Vector3 position, Vector3 velocity)
    {
        this.time = time;
        this.position = position;
        this.velocity = velocity;
        
    }
}

public class UnityTCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    public Rigidbody ballRigidbody; // Assign the ball Rigidbody in the Inspector

    void Start()
    {
        try
        {
            // Connect to the server
            client = new TcpClient("127.0.0.1", 5000); // Replace with server IP and port
            stream = client.GetStream();
            UnityEngine.Debug.Log("Connected to Python server");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Connection error: " + e.Message);
        }

        // Check if the Rigidbody is assigned
        if (ballRigidbody == null)
        {
            UnityEngine.Debug.LogError("Ball Rigidbody is not assigned! Please assign it in the Inspector.");
        }
    }

   

    void Update()
    {
        if (stream != null && stream.CanWrite && ballRigidbody != null)
        {
            // Get the ball's position and velocity
            float time = Time.time;
            Vector3 position = ballRigidbody.position;
            Vector3 velocity = ballRigidbody.linearVelocity;
            

            // Create a BallData instance
            BallData ballData = new BallData(time, position, velocity );

            // Serialize to JSON
            string message = JsonUtility.ToJson(ballData);

            // Send the message
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            try
            {
                stream.Write(data, 0, data.Length);
                UnityEngine.Debug.Log($"Sent ball data: {message}");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("Error sending data: " + e.Message);
            }
        }
    }


    void OnApplicationQuit()
    {
        // Close the connection
        if (stream != null) stream.Close();
        if (client != null) client.Close();
        UnityEngine.Debug.Log("Disconnected from server");
    }
}