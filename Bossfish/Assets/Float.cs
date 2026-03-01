using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Float : MonoBehaviour
{
    [Header("References")]
    public WaterSystems waterSystems;
    public Rigidbody rb;
    public Transform[] floatPoints;

    [Header("Buoyancy")]
    public float buoyancy = 10f;
    public float damping = 5f;

    [Header("Stability")]
    public float centerOfMassOffset = -0.5f;
    public float angularDragValue = 3f;
    public float maxAngularVelocity = 10f;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // Знижуємо центр мас для стабільності
        rb.centerOfMass = new Vector3(0f, centerOfMassOffset, 0f);

        // Гасимо крутіння
        rb.angularDamping = angularDragValue;
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    private void FixedUpdate()
    {
        if (waterSystems == null || floatPoints.Length == 0)
            return;

        for (int i = 0; i < floatPoints.Length; i++)
        {
            float waterLevel = waterSystems.GetWaterLevel(floatPoints[i].position);
            float depth = waterLevel - floatPoints[i].position.y;

            if (depth > 0f)
            {
                float buoyantForce = buoyancy * depth;

                rb.AddForceAtPosition(
                    Vector3.up * buoyantForce,
                    floatPoints[i].position,
                    ForceMode.Force
                );
            }
        }

        // Вертикальне демпфування (тільки в центрі мас)
        float verticalVelocity = rb.linearVelocity.y;
        float dampingForce = -verticalVelocity * damping;

        rb.AddForce(Vector3.up * dampingForce, ForceMode.Force);
    }
}