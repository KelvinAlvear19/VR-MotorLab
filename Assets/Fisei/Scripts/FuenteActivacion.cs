using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuenteActivacion : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
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
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        CanvasSwitcher canvasSwitcher = FindObjectOfType<CanvasSwitcher>();
        if (canvasSwitcher != null)
        {
            canvasSwitcher.OnCanvasSwitchComplete -= ActivateCanvasForObject;
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
}
