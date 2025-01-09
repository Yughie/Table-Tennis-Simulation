using UnityEngine;

public class CameraBall : MonoBehaviour
{
    public Transform ball;
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the ball's position
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (ball != null)
        {
            
            Vector3 targetPosition = ball.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

           
            transform.LookAt(ball);
        }
    }
}
