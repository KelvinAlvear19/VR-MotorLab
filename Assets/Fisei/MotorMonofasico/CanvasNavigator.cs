using UnityEngine;
using UnityEngine.UI;

public class CanvasNavigator : MonoBehaviour
{
    public GameObject[] canvases; // Lista de canvases en orden.
    private int currentIndex = 0; // Índice del canvas actual.

    // Método para el toggle "Siguiente".
    public void OnNextToggleChanged(bool isOn)
    {
        if (isOn)
        {
            NavigateToCanvas(currentIndex + 1);
        }
    }

    // Método para el toggle "Anterior".
    public void OnPreviousToggleChanged(bool isOn)
    {
        if (isOn)
        {
            NavigateToCanvas(currentIndex - 1);
        }
    }

    // Navegar a un canvas por índice.
    private void NavigateToCanvas(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < canvases.Length)
        {
            // Desactiva el canvas actual.
            canvases[currentIndex].SetActive(false);

            // Cambia al nuevo índice.
            currentIndex = targetIndex;

            // Activa el canvas correspondiente.
            canvases[currentIndex].SetActive(true);
        }
    }
}
