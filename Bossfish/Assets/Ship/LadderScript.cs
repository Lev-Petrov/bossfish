using UnityEngine;

public class LadderScript : Interactive
{
    public Transform landingPoint;
    public Transform boardPoint;
    VirtualShip ship;

    private void Start()
    {
        ship = FindAnyObjectByType<VirtualShip>();
    }

    public override void Interaction(GameObject interactor)
    {
        if (ship == null) { Debug.Log("No virtual ship"); return; }

        if (ship.onTheBoard.Contains(interactor.GetComponent<Rigidbody>()))
        {
            interactor.transform.position = landingPoint.position;
        }
        else
        {
            interactor.transform.position = boardPoint.position;
        }
    }
}
