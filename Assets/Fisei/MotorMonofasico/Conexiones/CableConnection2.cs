using UnityEngine;

public class CableConnection2 : MonoBehaviour
{
    public GameObject plug; // El plug del cable
    public GameObject copper; // El cobre del cable
    public Transform plugSocketA1; // Primer socket donde puede ir el plug
    public Transform plugSocketA2; // Segundo socket donde puede ir el plug
    public Transform copperSocketB1; // Primer socket donde puede ir el cobre
    public Transform copperSocketB2; // Segundo socket donde puede ir el cobre
    public GameObject currentCanvas; // Canvas del paso actual
    public GameObject nextCanvas;    // Canvas del siguiente paso (puede ser un final o el próximo paso del tutorial)

    private bool isPlugConnected = false;
    private bool isCopperConnected = false;

    void Update()
    {
        // Comprobar si el plug está conectado a uno de los dos sockets posibles
        if (!isPlugConnected && plug != null)
        {
            if ((plugSocketA1 != null && Vector3.Distance(plug.transform.position, plugSocketA1.position) < 0.1f) ||
                (plugSocketA2 != null && Vector3.Distance(plug.transform.position, plugSocketA2.position) < 0.1f))
            {
                isPlugConnected = true;
                Debug.Log("Plug conectado correctamente.");
            }
        }

        // Comprobar si el cobre está conectado a uno de los dos sockets posibles
        if (!isCopperConnected && copper != null)
        {
            if ((copperSocketB1 != null && Vector3.Distance(copper.transform.position, copperSocketB1.position) < 0.1f) ||
                (copperSocketB2 != null && Vector3.Distance(copper.transform.position, copperSocketB2.position) < 0.1f))
            {
                isCopperConnected = true;
                Debug.Log("Cobre conectado correctamente.");
            }
        }

        // Avanzar al siguiente paso solo si ambos están conectados
        if (isPlugConnected && isCopperConnected)
        {
            // Desactivar el Canvas actual
            if (currentCanvas != null)
            {
                currentCanvas.SetActive(false);
            }

            // Activar el siguiente Canvas (si existe)
            if (nextCanvas != null)
            {
                nextCanvas.SetActive(true);
            }
        }
    }
}
