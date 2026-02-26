using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayCast : MonoBehaviour
{
    public InputAction interactAction;
    public Transform cam;

    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;
        interactAction.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 2))
        {
            if(hit.collider.TryGetComponent<Interactive>(out Interactive interactive)) 
            {
                interactive.Interaction();
            }
        }
        Debug.Log("Interact pressed");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.position, cam.forward * 2);
    }
}