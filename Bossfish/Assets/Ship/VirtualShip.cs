using System.Collections.Generic;
using UnityEngine;

public class VirtualShip : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody ship;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        lastPosition = ship.position;
        lastRotation = ship.rotation;
    }

    private void FixedUpdate()
    {
        rb.position = lastPosition;
        rb.rotation = lastRotation;

        Vector3 deltaPosition = ship.position - lastPosition;
        Quaternion deltaRotation = ship.rotation * Quaternion.Inverse(lastRotation);

        foreach (Rigidbody rb in onTheBoard)
        {
            if (rb == null) continue;

            // позиційний зсув
            rb.MovePosition(rb.position + deltaPosition);

            // обертання
            Vector3 dir = rb.position - ship.position;
            dir = deltaRotation * dir;
            Vector3 newPos = ship.position + dir;

            rb.MovePosition(newPos);
            rb.MoveRotation(deltaRotation * rb.rotation);
        }

        lastPosition = ship.position;
        lastRotation = ship.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !onTheBoard.Contains(rb))
            onTheBoard.Add(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
            onTheBoard.Remove(rb);
    }

    public List<Rigidbody> onTheBoard = new List<Rigidbody>();
}