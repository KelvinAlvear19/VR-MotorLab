using UnityEngine;
using TMPro;

public class Socket : MonoBehaviour
{
    public string socketID;  // Usamos un ID para diferenciar cada socket (ej. "Socket1" y "Socket2")
    public TextMeshProUGUI panelMultimetro;  // Para mostrar el valor del mult�metro
    public string socketTag;  // Puede ser "Punta1" o "Punta2" para identificar qu� punta se conecta a este socket

    private bool puntaConectada = false;

    // Detectar cuando una punta entra en el socket
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si la punta tiene el tag correspondiente
        if (other.CompareTag(socketTag))
        {
            Debug.Log($"{socketID} - {socketTag} conectada");

            puntaConectada = true;

            // Verificar si ambos sockets tienen sus puntas conectadas
            if (GameObject.Find("Socket1").GetComponent<Socket>().puntaConectada &&
                GameObject.Find("Socket2").GetComponent<Socket>().puntaConectada)
            {
                MostrarLectura();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprobar si la punta que sali� tiene el tag correspondiente
        if (other.CompareTag(socketTag))
        {
            Debug.Log($"{socketID} - {socketTag} desconectada");
            puntaConectada = false;

            // Restablecer la lectura si alguna punta se desconecta
            if (!puntaConectada)
            {
                RestablecerLectura();
            }
        }
    }

    private void MostrarLectura()
    {
        // Mostrar 220 en el panel del mult�metro cuando ambas puntas est�n conectadas
        panelMultimetro.text = "220";
        Debug.Log("Lectura mostrada: 220");
    }

    private void RestablecerLectura()
    {
        // Restablecer el valor del mult�metro a 0 cuando las puntas se desconecten
        panelMultimetro.text = "0";
        Debug.Log("Lectura restablecida a: 0");
    }
}
