using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

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
            Debug.Log("Connected to Python server");
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("Connection error: " + e.Message);
        }
    }

    void Update()
    {
        if (stream != null && stream.CanWrite)
        {
            // Get the ball's position
            Vector3 ballPosition = ballRigidbody.position;

            // Create a JSON message
            string message = JsonUtility.ToJson(new { x = ballPosition.x, y = ballPosition.y, z = ballPosition.z });

            // Send the message
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            stream.Write(data, 0, data.Length);
            UnityEngine.Debug.Log("Sent: " + message);
        }
    }

    void OnApplicationQuit()
    {
        // Close the connection
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}