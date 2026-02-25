using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Float : MonoBehaviour
{
    [Header("References")]
    public WaterSurface targetSurface;
    public Rigidbody rb;
    public Transform[] floatPoints;

    [Header("Buoyancy")]
    public float buoyancy = 10f;
    public float damping = 5f;

    [Header("Stability")]
    public float centerOfMassOffset = -0.5f;
    public float angularDragValue = 3f;
    public float maxAngularVelocity = 10f;

    WaterSearchParameters searchParameters = new WaterSearchParameters();
    WaterSearchResult searchResult = new WaterSearchResult();

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
        if (targetSurface == null || floatPoints.Length == 0)
            return;

        for (int i = 0; i < floatPoints.Length; i++)
        {
            // Пошук рівня води в конкретній точці
            searchParameters.startPositionWS = floatPoints[i].position;
            searchParameters.targetPositionWS = floatPoints[i].position;
            searchParameters.error = 0.01f;
            searchParameters.maxIterations = 8;

            if (targetSurface.ProjectPointOnWaterSurface(searchParameters, out searchResult))
            {
                float waterLevel = searchResult.projectedPositionWS.y;
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
        }

        // Вертикальне демпфування (тільки в центрі мас)
        float verticalVelocity = rb.linearVelocity.y;
        float dampingForce = -verticalVelocity * damping;

        rb.AddForce(Vector3.up * dampingForce, ForceMode.Force);
    }
}