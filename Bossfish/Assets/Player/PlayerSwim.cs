
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerSwim : MonoBehaviour
{
    [Header("Swim Settings")]
    public InputAction swimAction;
    public Rigidbody rb;
    public WaterSystems waterSystems;
    public Transform playerCamera;
    public PlayerWalk playerWalk;
    public float speed;
    bool isDived;

    float depth;

    [Header("Sound Settings")]
    public AudioSource swimSource;
    public AudioResource[] divingSounds;
    public AudioResource[] emergesSounds;

    private void OnEnable()
    {
        swimAction.Enable();
    }
    private void OnDisable()
    {
        swimAction.Disable();
    }
    private void Update()
    {
        float waterLevel = waterSystems.GetWaterLevel(transform.position);
        depth = waterLevel - transform.position.y;

        if (depth > 0.2f && !isDived)
        {
            rb.useGravity = false;
            isDived = true;
            PlayDivingSound();

            if(playerWalk != null) {playerWalk.canWalk = false;}
        }
        else if (depth < -0.2f && isDived)
        {
            rb.useGravity = true;
            isDived = false;
            PlayerEmergesSound();

            if (playerWalk != null) { playerWalk.canWalk = true; }
        }
    }

    private void PlayDivingSound()
    {
        int index = Random.Range(0, divingSounds.Length);
        swimSource.resource = divingSounds[index];
        swimSource.Play();
    }

    private void PlayerEmergesSound()
    {
        int index = Random.Range(0, emergesSounds.Length);
        swimSource.resource = emergesSounds[index];
        swimSource.Play();
    }

    private void FixedUpdate()
    {
        if (!isDived) {return;}

        Vector3 input = swimAction.ReadValue<Vector3>();
        Vector3 move = playerCamera.forward * input.z + playerCamera.right * input.x + Vector3.up * input.y;

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);

    }

}
