using UnityEngine;

public class CanvasDisplayManager : MonoBehaviour
{
    // Referencias a los objetos donde se encuentran los 7 Canvas
    public GameObject[] objectsWithCanvas; // Array de objetos que contienen los canvases

    void Start()
    {
        // Asegurarse de que todos los Canvas estén desactivados al inicio
        foreach (var obj in objectsWithCanvas)
        {
            foreach (Transform child in obj.transform)
            {
                Canvas canvas = child.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(false); // Desactiva los Canvas al inicio
                }
            }
        }
    }

    // Método que activa todos los Canvas de los 7 objetos al mismo tiempo
    public void ActivateAllCanvas()
    {
        foreach (var obj in objectsWithCanvas)
        {
            foreach (Transform child in obj.transform)
            {
                Canvas canvas = child.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true); // Activa todos los Canvas
                }
            }
        }

        Debug.Log("All canvases activated.");
    }
}
