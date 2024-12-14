using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;

public class SeguimientoObjeto : MonoBehaviour
{
    public Canvas canvas; // Asigna el canvas creado
    public PointableElement pointableElement;

    void Start()
    {
        pointableElement = GetComponent<PointableElement>();
        pointableElement.WhenPointerEventRaised += HandlePointerEvent;
    }

    void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            // Muestra el canvas
            canvas.enabled = true;
        }
        else if (evt.Type == PointerEventType.Unselect)
        {
            // Oculta el canvas
            canvas.enabled = false;
        }
    }

    void OnDestroy()
    {
        pointableElement.WhenPointerEventRaised -= HandlePointerEvent;
    }
}