using UnityEngine;

public class ForwardBackwardMovement : MonoBehaviour
{
    public Transform cameraMovementTransform;   // Object that actually moves
    public Transform CameraRotationTransform;   // Object that controls look direction
    public float moveSpeed = 3f;

    bool moveForward = false;
    bool moveBackward = false;

    void Update()
    {
        if (cameraMovementTransform == null || CameraRotationTransform == null) return;

        // Get forward direction based only on Y rotation
        Vector3 forward = CameraRotationTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        if (moveForward)
        {
            cameraMovementTransform.position += forward * moveSpeed * Time.deltaTime;
        }

        if (moveBackward)
        {
            cameraMovementTransform.position -= forward * moveSpeed * Time.deltaTime;
        }
    }

    public void StartMoveForward()
    {
        moveForward = true;
    }

    public void StopMoveForward()
    {
        moveForward = false;
    }

    public void StartMoveBackward()
    {
        moveBackward = true;
    }

    public void StopMoveBackward()
    {
        moveBackward = false;
    }
}