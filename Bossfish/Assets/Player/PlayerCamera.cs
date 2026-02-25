using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Look Settings")]
    public InputAction lookAction;
    public Transform cam;
    public float mouseSensitivity;
    Vector2 rotation;

    private void OnEnable()
    {
        lookAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        lookAction.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        // Знаходить напрямок обертання
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
        mouseDelta *= mouseSensitivity;
        rotation += mouseDelta;

        //Обертає камеру
        rotation.y = Mathf.Clamp(rotation.y, -90, 90);

        transform.localRotation = Quaternion.Euler(0, rotation.x, 0f);
        cam.localRotation = Quaternion.Euler(-rotation.y, 0, 0);
    }
}
