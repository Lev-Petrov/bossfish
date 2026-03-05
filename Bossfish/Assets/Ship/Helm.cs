
using UnityEngine;
using UnityEngine.InputSystem;

public class Helm : Interactive
{
    [Header("Input")]
    public InputAction moveAction;
    public InputAction cancelAction;

    [Header("References")]
    public ShipController ship;
    public PlayerWalk player;
    public Transform standPoint;

    [Header("Helm Settings")]
    [Range(0, 180)]
    public float maxRot;

    public float rotSpeed;

    [Header("Runtime")]
    bool playerAtHelm;
    float currentRot;

    private void OnEnable()
    {
        moveAction.Enable();
        cancelAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        cancelAction.Disable();
    }

    public override void Interaction(GameObject interactor)
    {
        playerAtHelm = true;
        player.canWalk = false;
        player.rb.isKinematic = true;
    }

    private void Update()
    {
        if (playerAtHelm == false) {return;}

        if (cancelAction.WasPressedThisFrame()) 
        {
            playerAtHelm = false;
            player.canWalk = true;
            player.rb.isKinematic = false;
            return;
        }

        player.rb.position = standPoint.position;


        Vector2 input = moveAction.ReadValue<Vector2>();

        if (input.x != 0)
        {   //Знаходить поточну швидкість обертання
            currentRot = Mathf.MoveTowards(currentRot, maxRot * -input.x, rotSpeed * Time.deltaTime);
            //Розвертає штурвал
            Vector3 helmRot = new Vector3(90 - currentRot, 90, -90);
            transform.localRotation = Quaternion.Euler(helmRot);

            ship.input.x = currentRot / 179;
        }
    }

}
