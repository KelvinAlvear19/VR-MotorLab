using UnityEngine;

public class CableScript : MonoBehaviour
{
    [Header("Configuración de conexiones")]
    public GameObject plug;              // Objeto del plug del cable.
    public GameObject copper;            // Objeto del cobre del cable.
    public GameObject plugSocket;        // Socket donde debe conectarse el plug.
    public GameObject copperSocket;      // Socket donde debe conectarse el cobre.

    private bool plugConnected = false;  // Indica si el plug está conectado correctamente.
    private bool copperConnected = false; // Indica si el cobre está conectado correctamente.
    private bool connectionCompleted = false; // Indica si la conexión completa ya se notificó.

    [Header("Opciones visuales")]
    public Material connectedMaterial;   // Material para mostrar el estado conectado.
    public Material hoverMaterial;       // Material para hover.
    private Material plugSocketOriginalMaterial;
    private Material copperSocketOriginalMaterial;

    public TutorialManager tutorialManager; // Referencia al TutorialManager.

    private Renderer plugSocketRenderer;
    private Renderer copperSocketRenderer;

    private void Start()
    {
        if (plugSocket != null)
        {
            plugSocketRenderer = plugSocket.GetComponent<Renderer>();
            plugSocketOriginalMaterial = plugSocketRenderer.material;
        }

        if (copperSocket != null)
        {
            copperSocketRenderer = copperSocket.GetComponent<Renderer>();
            copperSocketOriginalMaterial = copperSocketRenderer.material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == plugSocket && !plugConnected)
        {
            plugConnected = true;
            plugSocketRenderer.material = connectedMaterial;
            CheckConnectionComplete();
        }

        if (other.gameObject == copperSocket && !copperConnected)
        {
            copperConnected = true;
            copperSocketRenderer.material = connectedMaterial;
            CheckConnectionComplete();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == plugSocket && plugConnected)
        {
            plugConnected = false;
            plugSocketRenderer.material = plugSocketOriginalMaterial;
        }

        if (other.gameObject == copperSocket && copperConnected)
        {
            copperConnected = false;
            copperSocketRenderer.material = copperSocketOriginalMaterial;
        }
    }

    // Verifica si la conexión está completa y notifica al TutorialManager.
    private void CheckConnectionComplete()
    {
        if (plugConnected && copperConnected && !connectionCompleted)
        {
            connectionCompleted = true;
            Debug.Log("Conexión completa.");

            // Notificar al TutorialManager para avanzar al siguiente paso.
            if (tutorialManager != null)
            {
                tutorialManager.NextStep();
            }
        }
    }
}
