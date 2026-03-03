
using UnityEngine;
using System.Collections.Generic;

public class VirtualShip : MonoBehaviour
{
    public Rigidbody rb; // is kinematic
    public Rigidbody ship;
    [HideInInspector] public List<Rigidbody> onTheBoard = new List<Rigidbody>();

    private void FixedUpdate()
    {
        rb.position = ship.position;
        rb.rotation = ship.rotation;
    }

    //Add object on the board
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
        onTheBoard.Add(other.GetComponent<Rigidbody>());
    }

    //Remove object from the board
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
        onTheBoard.Remove(other.gameObject.GetComponent<Rigidbody>());
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