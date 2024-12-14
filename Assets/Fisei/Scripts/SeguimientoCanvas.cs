using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCanvas : MonoBehaviour
{
    public Transform playerCamera; // La cámara del jugador (usualmente la cámara central del XR Rig)
    public float distanceFromCamera = 2f; // Distancia del Canvas respecto a la cámara

    private void Start()
    {
        // Asegúrate de que el canvas está en modo World Space
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
        }
    }

    private void Update()
    {
        // Asegúrate de que playerCamera no sea nula
        if (playerCamera != null)
        {
            // Establece la posición del canvas con respecto a la cámara
            Vector3 newPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
            transform.position = newPosition;

            // Asegúrate de que el canvas siempre mire hacia la cámara
            transform.LookAt(playerCamera.position);

            // Gira 180 grados en Y para corregir la dirección de la rotación
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0); // Gira 180 grados en Y para corregir el modo espejo
        }
    }
}
