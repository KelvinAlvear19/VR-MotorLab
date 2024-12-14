using UnityEngine;

public class PhysicalCable : MonoBehaviour
{
    public Transform startPoint; // Punto de inicio
    public Transform endPoint;   // Punto de fin
    [Range(2, 50)]
    public int segments = 10;    // Número de segmentos
    public float segmentLength = 0.2f; // Longitud de cada segmento
    public float radius = 0.05f; // Radio del cable
    public GameObject segmentPrefab;  // Prefab para cada segmento del cable

    private Transform[] segmentTransforms; // Almacena las referencias de cada segmento

    void Start()
    {
        GenerateCable();
    }

    void GenerateCable()
    {
        Vector3 startPosition = startPoint.position;
        Vector3 endPosition = endPoint.position;
        Vector3 direction = (endPosition - startPosition).normalized;
        float segmentSpacing = Vector3.Distance(startPosition, endPosition) / (segments - 1);

        Rigidbody previousSegment = null;

        for (int i = 0; i < segments; i++)
        {
            // Calcular posición
            Vector3 segmentPosition = startPosition + direction * segmentSpacing * i;

            // Forzar orientación correcta
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Instanciar prefab
            GameObject segment = Instantiate(segmentPrefab, segmentPosition, rotation, transform);
            segment.name = $"Cable Segment {i}";

            // Configurar Rigidbody y Joint
            Rigidbody rb = segment.GetComponent<Rigidbody>();
            if (rb == null) rb = segment.AddComponent<Rigidbody>();

            if (previousSegment != null)
            {
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousSegment;
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector3.zero;
                joint.connectedAnchor = new Vector3(0, 0, -segmentSpacing / 2);

                // Configurar límite
                SoftJointLimit limit = new SoftJointLimit { limit = segmentSpacing };
                joint.linearLimit = limit;
            }

            previousSegment = rb;
        }
    }



}
