using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Referencias a las imágenes del "visto verde"
    public Image mandilCheckmark;
    public Image guantesCheckmark;
    public Image gafasCheckmark;

    // Referencia al collider de la puerta
    public Collider puertaCollider;

    private int completedSteps = 0; // Contador de pasos completados
    private int totalSteps = 3; // Total de pasos requeridos

    /// <summary>
    /// Activa el visto verde correspondiente y verifica si se completaron todos los pasos.
    /// </summary>
    public void ActivateCheckmark(string item)
    {
        switch (item.ToLower())
        {
            case "mandil":
                mandilCheckmark.gameObject.SetActive(true); // Activa el visto verde del mandil
                Debug.Log("Mandil completado.");
                break;

            case "guantes":
                guantesCheckmark.gameObject.SetActive(true); // Activa el visto verde de los guantes
                Debug.Log("Guantes completados.");
                break;

            case "gafas":
                gafasCheckmark.gameObject.SetActive(true); // Activa el visto verde de las gafas
                Debug.Log("Gafas colocadas.");
                break;

            default:
                Debug.LogWarning($"Elemento desconocido: {item}");
                return;
        }

        // Incrementar los pasos completados
        completedSteps++;

        // Verificar si todos los pasos están completados
        if (completedSteps >= totalSteps)
        {
            UnlockDoor();
        }
    }

    /// <summary>
    /// Desactiva el collider de la puerta para permitir el acceso.
    /// </summary>
    private void UnlockDoor()
    {
        if (puertaCollider != null)
        {
            puertaCollider.enabled = false; // Desactiva el collider
            Debug.Log("Puerta desbloqueada.");
        }
        else
        {
            Debug.LogError("PuertaCollider no está asignado en el UIController.");
        }
    }
}
