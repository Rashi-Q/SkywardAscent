using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // the Player

    [Header("Settings")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 1f, -10f);

    private float highestY; // camera never moves back down

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Only move up, never down — classic endless-climber camera behavior
        float targetY = target.position.y + offset.y;
        if (targetY > highestY)
        {
            highestY = targetY;
        }

        Vector3 desiredPosition = new Vector3(offset.x, highestY, offset.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}