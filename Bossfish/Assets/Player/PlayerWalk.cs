using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    [Header("Walk Settings")]
    public InputAction moveAction;
    public Rigidbody rb;
    public float speed;

    private void OnEnable()
    {
        moveAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 move = transform.forward * input.y + transform.right * input.x;
        move *= speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);
    }
}
