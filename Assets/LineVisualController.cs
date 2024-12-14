using UnityEngine;

public class LineVisualController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // Obtiene el componente LineRenderer en el objeto
        lineRenderer = GetComponent<LineRenderer>();

        // Opcional: Aseg�rate de que la l�nea est� desactivada al inicio
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    // M�todo para activar la l�nea visual
    public void ActivateLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    // M�todo para desactivar la l�nea visual
    public void DeactivateLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    // Alterna la visibilidad de la l�nea visual
    public void ToggleLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = !lineRenderer.enabled;
        }
    }
}
