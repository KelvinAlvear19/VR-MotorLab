using UnityEngine;
using UnityEngine.XR;

public class MaintainHeight : MonoBehaviour
{
    public Transform cameraTransform; // Referencia a la cámara dentro del XR Origin
    public float fixedCameraHeight = 1.8f; // Altura fija para la cámara (en metros)

    private void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("Debes asignar la cámara al script en el Inspector.");
        }
    }

    private void Update()
    {
        // Mantén la posición de la cámara en el eje Y a una altura fija
        Vector3 cameraPosition = cameraTransform.localPosition;
        cameraPosition.y = fixedCameraHeight; // Fija la altura en Y
        cameraTransform.localPosition = cameraPosition;
    }
}
