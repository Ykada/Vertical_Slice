using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_Movement : MonoBehaviour
{
    [Header("Camera and main Position")]
    [SerializeField] private Transform cameraposistion;
    [SerializeField] private Transform centerpoint;

    [Header("Movement Settings")]
    [SerializeField] private float cameraSpeed;
    private Vector3 offset;
    private Vector3 smoothVelocity = Vector3.zero;

    [Header("Mouse Control Settings")]
    [SerializeField] private float mousesmoothing;

    [Header("Rotation Limits (Turning Radius)")]
    [SerializeField] private float maxHorizontalAngle;
    [SerializeField] private float maxVerticalAngle;

    private float yaw;
    private float pitch;

    private void Start()
    {
        offset = cameraposistion.position - centerpoint.position;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {

        if (Keyboard.current.spaceKey.isPressed)
        {
            GetComponent<Camera_Movement>().enabled = true;
        }

        if(Keyboard.current.leftShiftKey.isPressed)
        {
            GetComponent<Camera_Movement>().enabled = false;
        }

        if (Mouse.current != null)
        {
            yaw += Mouse.current.delta.x.ReadValue() * mousesmoothing;
            pitch -= Mouse.current.delta.y.ReadValue() * mousesmoothing;

            yaw = Mathf.Clamp(yaw, -maxHorizontalAngle, maxHorizontalAngle);
            pitch = Mathf.Clamp(pitch, -maxVerticalAngle, maxVerticalAngle);
        }

        Quaternion camerarotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = centerpoint.position + camerarotation * offset;

        cameraposistion.position = Vector3.SmoothDamp(
            cameraposistion.position,
            desiredPosition,
            ref smoothVelocity,
            cameraSpeed * Time.deltaTime
        );

        cameraposistion.LookAt(centerpoint.position);
    }
}

