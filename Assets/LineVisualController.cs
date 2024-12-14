using UnityEngine;

public class LineVisualController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        // Obtiene el componente LineRenderer en el objeto
        lineRenderer = GetComponent<LineRenderer>();

        // Opcional: Asegúrate de que la línea esté desactivada al inicio
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    // Método para activar la línea visual
    public void ActivateLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    // Método para desactivar la línea visual
    public void DeactivateLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    // Alterna la visibilidad de la línea visual
    public void ToggleLineVisual()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = !lineRenderer.enabled;
        }
    }
}
