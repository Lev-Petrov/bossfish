using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    [Header("Walk Settings")]
    public InputAction moveAction;
    public Rigidbody rb;
    public float speed;
    public bool canWalk;

    [Header("Sound Setting")]
    public AudioSource audioSource;
    public AudioResource[] stepsSounds;
    public float timeBtwSteps;
    bool isWalking;


    private void OnEnable()
    {
        moveAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        StartCoroutine(SoundsPlayer());
    }

    private void FixedUpdate()
    {
        if (!canWalk) return;

        Vector2 input = moveAction.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            Vector3 move = transform.forward * input.y + transform.right * input.x;
            move *= speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            isWalking = true;
        }
        else {isWalking = false;}  
    }

    IEnumerator SoundsPlayer()
    {
        while (true)
        {
            if (isWalking)
            {
                int stepIndex = Random.Range(0, stepsSounds.Length);
                audioSource.resource = stepsSounds[stepIndex];
                audioSource.Play();
            }
            yield return new WaitForSeconds(timeBtwSteps / Mathf.Max(speed, 0.01f));
        }
    }
}
