using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public InputAction interactAction;
    public Transform cam;
    RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, 2))
        {
            if (hit.collider.TryGetComponent<Interactive>(out Interactive interactive))
            {
                if (interactAction.triggered)
                {
                    interactive.Interaction();
                    Debug.Log("Interaction with" + hit.collider.name);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(cam.position, cam.forward * 2);
    }
}