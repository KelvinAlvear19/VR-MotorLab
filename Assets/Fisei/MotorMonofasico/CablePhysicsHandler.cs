using System.Net;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CablePhysicsHandler : MonoBehaviour
{
    public Rigidbody[] cableRigidbodies; // Asignar todos los huesos del cable aquí
    public Rigidbody startPoint;        // Rigidbody del punto de inicio
    public Rigidbody endPoint;
    public void OnGrabStart()
    {
        // Desactivar físicas de los huesos al agarrar
        foreach (Rigidbody rb in cableRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    public void OnGrabEnd()
    {
        // Reactivar físicas al soltar
        foreach (Rigidbody rb in cableRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        // Asegurarse de que el cable siga al inicio y fin
        cableRigidbodies[0].MovePosition(startPoint.position);
        cableRigidbodies[cableRigidbodies.Length - 1].MovePosition(endPoint.position);
    }
}

