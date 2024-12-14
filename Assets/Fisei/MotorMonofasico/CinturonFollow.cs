using UnityEngine;

public class CinturonFollow : MonoBehaviour
{
    public Transform cameraTransform; // Transform de la cámara.
    public Vector3 offsetPosition = new Vector3(0, 0, 0); // Desplazamiento respecto a la cámara.

    void Start()
    {
        // Si no se asignó el transform de la cámara, lo hacemos automáticamente.
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Verifica si el cinturón tiene un Mesh Renderer para asegurarse de que es visible
        if (GetComponent<MeshRenderer>() == null)
        {
            Debug.LogWarning("El cinturón no tiene un MeshRenderer. Asegúrate de que el cinturón tenga un modelo 3D.");
        }
    }

    void Update()
    {
        // Asegúrate de que la cámara esté asignada
        if (cameraTransform != null)
        {
            // Aplica solo la posición (sin rotación) para que el cinturón siga la cámara
            transform.position = cameraTransform.position + offsetPosition;

            // Si prefieres mantener una rotación fija, puedes usar esta línea:
            transform.rotation = Quaternion.Euler(0, cameraTransform.rotation.eulerAngles.y, 0); // Solo rota en el eje Y, sin afectar la inclinación o giro
        }
        else
        {
            Debug.LogError("No se ha asignado la cámara al script CinturonFollow.");
        }
    }
}
