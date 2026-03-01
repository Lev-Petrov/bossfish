using UnityEngine;

public class LadderScript : Interactive
{
    public Transform landingPoint;
    public Transform boardPoint;

    PlayerSwim player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSwim>();
    }


    public override void Interaction()
    {
        if(player != null) {return;}

        if (player.isSwimming)
        {
            player.transform.position = boardPoint.position;
        }
        else
        {
            player.transform.position = landingPoint.position;
        }
    }
}
