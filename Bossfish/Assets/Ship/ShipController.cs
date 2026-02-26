using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public Transform virtualShip;

    private void Update()
    {
        virtualShip.position = transform.position;
        virtualShip.rotation = transform.rotation;
    }
}