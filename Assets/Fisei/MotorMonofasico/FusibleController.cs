 using UnityEngine;

public class FusibleController : MonoBehaviour
{
    public int socketIndex;             // �ndice del socket
    private MotorSelector fuseManager;    // Referencia al MotorSelector
    private GameObject currentFuse;     // Fusible actualmente colocado
    private bool isInsideSocket = false;  // Para verificar si el fusible est� dentro del socket

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
        // Detectar si el fusible entra en el �rea del socket
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
            // Intentar colocar el fusible solo si no ha sido colocado a�n
            if (!fuseManager.IsSocketOccupied(socketIndex))
            {
                fuseManager.PlaceFuse(currentFuse, socketIndex);
                isInsideSocket = false;  // El fusible ya fue colocado
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectar si el fusible sale del �rea del socket
        if (other.CompareTag("Fusible") && currentFuse != null)
        {
            // Si el fusible sali� sin ser colocado correctamente, se puede "remover" o resetear el estado
            fuseManager.RemoveFuse(socketIndex);
            isInsideSocket = false; // El fusible ya no est� dentro del socket
            currentFuse = null;
        }
    }
}
