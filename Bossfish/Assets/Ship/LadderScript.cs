using UnityEngine;

public class LadderScript : Interactive
{
    public PlayerSwim playerSwim;
    public Transform landingPoint;
    public Transform boardPoint;
    public override void Interaction()
    {
        if (playerSwim)
        {
            playerSwim.transform.SetParent(null);
            playerSwim.transform.position = boardPoint.position;
        }
        else
        {
            playerSwim.transform.SetParent(null);
            playerSwim.transform.position = landingPoint.position;
        }
    }
}
