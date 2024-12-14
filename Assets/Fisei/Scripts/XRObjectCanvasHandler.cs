using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRObjectCanvasHandler : MonoBehaviour
{
    public GameObject canvas;
    private XRGrabInteractable grabInteractable;
    private Vector3 lastReleasedPosition;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError($"El objeto {gameObject.name} no tiene un componente XRGrabInteractable.");
            return;
        }

        // Escuchar el evento de CanvasSwitcher
        CanvasSwitcher canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
        if (canvasSwitcher != null)
        {
            canvasSwitcher.OnCanvasSwitchComplete += ActivateCanvasForObject;
        }
        else
        {
            Debug.LogError("No se ha encontrado CanvasSwitcher en la escena.");
        }

        // Asegurarse de que el Canvas esté desactivado al principio
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        // Subscribirse a los eventos de interacción
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDestroy()
    {
        CanvasSwitcher canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
        if (canvasSwitcher != null)
        {
            canvasSwitcher.OnCanvasSwitchComplete -= ActivateCanvasForObject;
        }

        // Desuscribirse de los eventos para evitar errores
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }

    // Activar Canvas cuando termine CanvasSwitcher
    private void ActivateCanvasForObject()
    {
        if (canvas != null)
        {
            canvas.SetActive(true); // Hacer visible el Canvas
            Debug.Log($"Canvas de {gameObject.name} activado.");
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (canvas != null)
        {
            // Desactivar Canvas y hacer que siga al objeto sin rotarlo
            canvas.SetActive(false);
            canvas.transform.position = transform.position; // Asegurarse de que sigue la posición
            Debug.Log($"Canvas de {gameObject.name} desactivado y ahora sigue al objeto.");
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (canvas != null)
        {
            // Reactivar Canvas y colocarlo en la nueva posición del objeto
            canvas.SetActive(true);
            lastReleasedPosition = transform.position; // Guardar la última posición del objeto
            canvas.transform.position = lastReleasedPosition; // Colocar el Canvas en la nueva posición
            Debug.Log($"Canvas de {gameObject.name} reactivado y colocado en la nueva posición.");
        }
    }
}
