using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Look Settings")]
    public InputAction lookAction;
    public Transform cam;
    public float mouseSensitivity;

    private Vector2 rotation; // rotation.x = yaw, rotation.y = pitch

    private void OnEnable()
    {
        lookAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        lookAction.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        // Читаємо рух миші
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        // Збільшуємо rotation на delta
        rotation.x += mouseDelta.x; // yaw
        rotation.y += mouseDelta.y; // pitch
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f); // обмеження нахилу камери

        // Оновлюємо локальну ротацію камери (нахил)
        cam.localRotation = Quaternion.Euler(-rotation.y, 0f, 0f);

        // Оновлюємо ротацію персонажа (yaw)
        transform.localRotation = Quaternion.Euler(0f, rotation.x, 0f);
    }
}
