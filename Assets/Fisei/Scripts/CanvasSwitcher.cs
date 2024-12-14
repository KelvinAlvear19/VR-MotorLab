using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con los Botones

public class CanvasSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct CanvasInfo
    {
        public GameObject canvas;
        public Button nextButton; // Asociamos un botón para avanzar al siguiente Canvas
    }

    public CanvasInfo[] canvases;

    // Evento para notificar cuando el proceso termine
    public delegate void CanvasSwitchComplete();
    public event CanvasSwitchComplete OnCanvasSwitchComplete;

    void Start()
    {
        Debug.Log("CanvasSwitcher script has started.");

        if (canvases == null || canvases.Length == 0)
        {
            Debug.LogWarning("Canvas array is empty. Please assign canvases.");
        }
        else
        {
            Debug.Log($"Canvas array has {canvases.Length} elements.");
            foreach (var canvasInfo in canvases)
            {
                // Se asegura de que cada botón tenga un listener para cambiar el canvas cuando se presione
                if (canvasInfo.nextButton != null)
                {
                    canvasInfo.nextButton.onClick.AddListener(() => OnNextButtonPressed(canvasInfo));
                }
            }

            // Activa el primer canvas por defecto
            ActivateCanvas(0);
        }
    }

    void OnNextButtonPressed(CanvasInfo currentCanvasInfo)
    {
        int currentIndex = GetCanvasIndex(currentCanvasInfo);

        if (currentIndex != -1 && currentIndex < canvases.Length - 1)
        {
            // Activa el siguiente canvas
            ActivateCanvas(currentIndex + 1);
        }
        else
        {
            // Si estamos en el último canvas, desactivamos todos
            DeactivateAllCanvases();
            OnCanvasSwitchComplete?.Invoke(); // Dispara el evento al terminar el proceso
        }
    }

    // Obtiene el índice del canvas basado en el objeto Canvas
    int GetCanvasIndex(CanvasInfo canvasInfo)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            if (canvases[i].canvas == canvasInfo.canvas)
            {
                return i;
            }
        }
        return -1; // Si no se encuentra, retorna -1
    }

    void ActivateCanvas(int index)
    {
        // Activa el canvas correspondiente y desactiva los demás
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].canvas.SetActive(i == index);
            Debug.Log(i == index ? $"Canvas {index} activated." : $"Canvas {i} deactivated.");
        }
    }

    void DeactivateAllCanvases()
    {
        // Desactiva todos los canvas
        foreach (var canvasInfo in canvases)
        {
            canvasInfo.canvas.SetActive(false);
        }
    }
}
