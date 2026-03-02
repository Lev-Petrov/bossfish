using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Look Settings")]
    public InputAction lookAction;
    public Transform cam;
    public float mouseSensitivity = 100f;

    Vector2 rotation;

    private void OnEnable()
    {
        lookAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        // ? Беремо поточний поворот, щоб не перезаписувати його
        Vector3 bodyAngles = transform.localEulerAngles;
        Vector3 camAngles = cam.localEulerAngles;

        rotation.x = bodyAngles.y;
        rotation.y = -camAngles.x;
    }

    private void OnDisable()
    {
        lookAction.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
        mouseDelta *= mouseSensitivity * Time.deltaTime;

        rotation.x += mouseDelta.x;
        rotation.y += mouseDelta.y;

        // Обмеження вертикального огляду
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

        // ? Обертаємо тіло тільки по Y
        transform.localRotation = Quaternion.Euler(0f, rotation.x, 0f);

        // ? Камеру тільки по X
        cam.localRotation = Quaternion.Euler(-rotation.y, 0f, 0f);
    }
}