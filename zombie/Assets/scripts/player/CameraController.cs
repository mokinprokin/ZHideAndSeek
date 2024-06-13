
using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    public NetworkIdentity identity;
    public float smoothTime = 0.1f; // You can adjust this value for smoother movement

    float xRotation = 0.0f;
    Vector3 smoothVelocity = Vector3.zero;

    void Start()
    {
        if (!identity.isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void LateUpdate()
    {
        if (identity.isLocalPlayer)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70.0f, 60.0f);

            Quaternion targetRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothTime);

            playerBody.Rotate(Vector3.up * mouseX);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}