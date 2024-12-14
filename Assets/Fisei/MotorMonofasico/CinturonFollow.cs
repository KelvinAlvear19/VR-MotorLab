using UnityEngine;

public class CinturonFollow : MonoBehaviour
{
    public Transform cameraTransform; // Transform de la c�mara.
    public Vector3 offsetPosition = new Vector3(0, 0, 0); // Desplazamiento respecto a la c�mara.

    void Start()
    {
        // Si no se asign� el transform de la c�mara, lo hacemos autom�ticamente.
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Verifica si el cintur�n tiene un Mesh Renderer para asegurarse de que es visible
        if (GetComponent<MeshRenderer>() == null)
        {
            Debug.LogWarning("El cintur�n no tiene un MeshRenderer. Aseg�rate de que el cintur�n tenga un modelo 3D.");
        }
    }

    void Update()
    {
        // Aseg�rate de que la c�mara est� asignada
        if (cameraTransform != null)
        {
            // Aplica solo la posici�n (sin rotaci�n) para que el cintur�n siga la c�mara
            transform.position = cameraTransform.position + offsetPosition;

            // Si prefieres mantener una rotaci�n fija, puedes usar esta l�nea:
            transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0); // Solo rota en el eje Y, sin afectar la inclinaci�n o giro
        }
        else
        {
            Debug.LogError("No se ha asignado la c�mara al script CinturonFollow.");
        }
    }
}
