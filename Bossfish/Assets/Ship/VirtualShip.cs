

using UnityEngine;
using System.Collections.Generic;

public class VirtualShip : MonoBehaviour
{
    public Rigidbody rb;
    public Rigidbody ship;
    [HideInInspector] public List<Rigidbody> onTheBoard = new List<Rigidbody>();

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        lastPosition = ship.position;
        lastRotation = ship.rotation;
    }

    private void FixedUpdate()
    {
        rb.position = ship.position;
        rb.rotation = ship.rotation;
        rb.position = lastPosition;
        rb.rotation = lastRotation;

        Vector3 deltaPosition = ship.position - lastPosition;
        Quaternion deltaRotation = ship.rotation * Quaternion.Inverse(lastRotation);

        foreach (Rigidbody rb in onTheBoard)
        {
            if (rb == null) continue;

            // ���������� ����
            rb.MovePosition(rb.position + deltaPosition);

            // ���������
            Vector3 dir = rb.position - ship.position;
            dir = deltaRotation * dir;
            Vector3 newPos = ship.position + dir;

            rb.MovePosition(newPos);
            rb.MoveRotation(deltaRotation * rb.rotation);
        }

        lastPosition = ship.position;
        lastRotation = ship.rotation;
    }

    //Add object on the board
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        onTheBoard.Add(other.GetComponent<Rigidbody>());
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && !onTheBoard.Contains(rb))
            onTheBoard.Add(rb);
    }

    //Remove object from the board
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
        onTheBoard.Remove(other.gameObject.GetComponent<Rigidbody>());
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
            onTheBoard.Remove(rb);
    }

    //Apply object's weight on the real ship
    private void OnCollisionStay(Collision collision)
    {
        if (onTheBoard.Contains(collision.rigidbody))
        {
            ContactPoint contact = collision.contacts[0];

            Vector3 force = Vector3.down * collision.rigidbody.mass * Physics.gravity.magnitude;

            ship.AddForceAtPosition(force, contact.point);
        }
    }
}