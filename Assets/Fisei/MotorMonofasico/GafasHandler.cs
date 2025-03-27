using UnityEngine;

public class GafasHandler : MonoBehaviour
{
    public UIController uiController; // Referencia al controlador de la UI
    public GameObject newObject; // Objeto que se activará al interactuar

    private bool areGafasInteracted = false; // Indica si se ha interactuado con las gafas

    void Start()
    {
        // Verifica si el objeto nuevo está asignado
        if (newObject == null)
        {
            Debug.LogError("No se ha asignado un objeto para activar.");
        }
    }

    public void OnGafasInteract()
    {
        if (!areGafasInteracted)
        {
            ActivateNewObject(); // Activa el nuevo objeto
            HideGafas(); // Oculta las gafas
            UpdateUI(); // Actualiza la UI
            areGafasInteracted = true;
        }
    }

    private void ActivateNewObject()
    {
        if (newObject != null)
        {
            newObject.SetActive(true);
            Debug.Log("El nuevo objeto ha sido activado.");
        }
        else
        {
            Debug.LogError("No se encontró el objeto para activar.");
        }
    }

    private void HideGafas()
    {
        gameObject.SetActive(false); // Desactiva las gafas
        Debug.Log("Las gafas han sido desactivadas.");
    }

    private void UpdateUI()
    {
        if (uiController != null)
        {
            uiController.ActivateCheckmark("gafas");
            Debug.Log("UI actualizada para reflejar la interacción con las gafas.");
        }
        else
        {
            Debug.LogError("No se encontró un controlador de UI para actualizar.");
        }
    }
}
