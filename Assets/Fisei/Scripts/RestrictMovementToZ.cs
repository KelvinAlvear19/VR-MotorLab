using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RestrictMovementToZ : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    public float fixedYPosition = 0.5f; // Altura fija del objeto sobre la mesa
    public float fixedXPosition = 0f; // Posici�n fija en el eje X (aj�stala seg�n tu escena)
    public float minZ = -5f; // L�mite m�nimo en Z
    public float maxZ = 5f;  // L�mite m�ximo en Z

    private bool isBeingGrabbed = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Aseg�rate de que el Rigidbody est� configurado correctamente
        rb.isKinematic = true;

        // Eventos para detectar cuando el objeto es agarrado o soltado
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        // Limpieza de eventos
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isBeingGrabbed = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isBeingGrabbed = false;
    }

    void FixedUpdate()
    {
        if (isBeingGrabbed)
        {
            // Restringe el movimiento en X e Y, y permite solo Z dentro de los l�mites
            Vector3 position = transform.position;
            position.x = fixedXPosition;
            position.y = fixedYPosition;
            position.z = Mathf.Clamp(position.z, minZ, maxZ); // Limita el movimiento en Z
            transform.position = position;
        }
    }
}
