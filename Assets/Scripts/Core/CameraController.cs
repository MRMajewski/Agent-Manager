using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float rotateSpeed = 100f;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 50f;

    private Vector3 dragOrigin;

    void LateUpdate()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        HandleCameraZoom();
    }

    void HandleCameraMovement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move.z += 1;
        if (Input.GetKey(KeyCode.S)) move.z -= 1;
        if (Input.GetKey(KeyCode.A)) move.x -= 1;
        if (Input.GetKey(KeyCode.D)) move.x += 1;

        Vector3 direction = transform.right * move.x + transform.forward * move.z;
        direction.y = 0;

        transform.Translate(direction * panSpeed * Time.unscaledDeltaTime, Space.World);
    }

    void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotateSpeed * Time.unscaledDeltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.unscaledDeltaTime;

            transform.Rotate(Vector3.up, rotationX, Space.World);
            transform.Rotate(Vector3.right, -rotationY);
        }
    }

    void HandleCameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        position.y -= scroll * zoomSpeed;
        position.y = Mathf.Clamp(position.y, minZoom, maxZoom);

        transform.position = position;
    }
}