 using UnityEngine;

public class FusibleController : MonoBehaviour
{
    public int socketIndex;             // Índice del socket
    private MotorSelector fuseManager;    // Referencia al MotorSelector
    private GameObject currentFuse;     // Fusible actualmente colocado
    private bool isInsideSocket = false;  // Para verificar si el fusible está dentro del socket

    private void Start()
    {
        // Obtener la referencia al MotorSelector
        fuseManager = FindObjectOfType<MotorSelector>();

        if (fuseManager == null)
        {
            Debug.LogError("MotorSelector no encontrado en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el fusible entra en el área del socket
        if (other.CompareTag("Fusible") && !isInsideSocket)
        {
            currentFuse = other.gameObject;
            isInsideSocket = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Cuando el fusible permanece dentro del socket
        if (other.CompareTag("Fusible") && isInsideSocket && currentFuse != null)
        {
            // Intentar colocar el fusible solo si no ha sido colocado aún
            if (!fuseManager.IsSocketOccupied(socketIndex))
            {
                fuseManager.PlaceFuse(currentFuse, socketIndex);
                isInsideSocket = false;  // El fusible ya fue colocado
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar si el fusible sale del área del socket
        if (other.CompareTag("Fusible") && currentFuse != null)
        {
            // Si el fusible salió sin ser colocado correctamente, se puede "remover" o resetear el estado
            fuseManager.RemoveFuse(socketIndex);
            isInsideSocket = false; // El fusible ya no está dentro del socket
            currentFuse = null;
        }
    }
}
