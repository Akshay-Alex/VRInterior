using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCameraMovement : MonoBehaviour
{
    public Transform cameraRotationTransform; // Assign in inspector
    public float rotationSpeed = 0.2f;

    float yaw = 0f;
    float pitch = 0f;

    int activeFingerId = -1;

    void Start()
    {
        if (cameraRotationTransform == null)
        {
            Debug.LogError("Camera Transform not assigned!");
            return;
        }

        Vector3 angles = cameraRotationTransform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        // TOUCH INPUT
        foreach (Touch touch in Input.touches)
        {
            // If no finger is controlling camera yet
            if (activeFingerId == -1)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        activeFingerId = touch.fingerId;
                    }
                }
            }

            // If this finger is the one controlling camera
            if (touch.fingerId == activeFingerId)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    RotateCamera(touch.deltaPosition.x, touch.deltaPosition.y);
                }

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    activeFingerId = -1;
                }
            }
        }

        // MOUSE INPUT (Editor / WebGL desktop)
        if (Input.GetMouseButton(0) && activeFingerId == -1)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                float swipeX = Input.GetAxis("Mouse X");
                float swipeY = Input.GetAxis("Mouse Y");

                RotateCamera(swipeX * 50f, swipeY * 50f);
            }
        }
    }

    void RotateCamera(float x, float y)
    {
        yaw += x * rotationSpeed;
        pitch -= y * rotationSpeed;

        pitch = Mathf.Clamp(pitch, -80f, 80f);

        cameraRotationTransform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}