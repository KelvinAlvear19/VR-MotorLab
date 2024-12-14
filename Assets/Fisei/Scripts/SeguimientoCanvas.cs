using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCanvas : MonoBehaviour
{
    public Transform playerCamera; // La c�mara del jugador (usualmente la c�mara central del XR Rig)
    public float distanceFromCamera = 2f; // Distancia del Canvas respecto a la c�mara

    private void Start()
    {
        // Aseg�rate de que el canvas est� en modo World Space
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.WorldSpace;
        }
    }

    private void Update()
    {
        // Aseg�rate de que playerCamera no sea nula
        if (playerCamera != null)
        {
            // Establece la posici�n del canvas con respecto a la c�mara
            Vector3 newPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
            transform.position = newPosition;

            // Aseg�rate de que el canvas siempre mire hacia la c�mara
            transform.LookAt(playerCamera.position);

            // Gira 180 grados en Y para corregir la direcci�n de la rotaci�n
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0); // Gira 180 grados en Y para corregir el modo espejo
        }
    }
}
