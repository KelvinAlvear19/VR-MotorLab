using UnityEngine;
using TMPro;

public class Multimeter : MonoBehaviour
{
    public GameObject punta1;
    public GameObject punta2;
    public GameObject socket1;
    public GameObject socket2;
    public TextMeshProUGUI panelMultimetro;

    private bool punta1Conectada = false;
    private bool punta2Conectada = false;

    void Start()
    {
        panelMultimetro.text = "0";
    }

    // Detectar cuando la punta entra en un socket
    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si la punta1 entra en socket1
        if (other.gameObject == socket1 && punta1 != null)
        {
            Debug.Log("Punta 1 conectada");
            punta1Conectada = true;
        }

        // Comprobar si la punta2 entra en socket2
        if (other.gameObject == socket2 && punta2 != null)
        {
            Debug.Log("Punta 2 conectada");
            punta2Conectada = true;
        }

        // Si ambas puntas están conectadas, mostrar lectura
        if (punta1Conectada && punta2Conectada)
        {
            Debug.Log("Ambas puntas conectadas, mostrando lectura");
            MostrarLectura();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprobar si la punta1 sale del socket1
        if (other.gameObject == socket1 && punta1 != null)
        {
            Debug.Log("Punta 1 desconectada");
            punta1Conectada = false;
        }

        // Comprobar si la punta2 sale del socket2
        if (other.gameObject == socket2 && punta2 != null)
        {
            Debug.Log("Punta 2 desconectada");
            punta2Conectada = false;
        }

        // Restablecer la lectura si alguna punta se desconecta
        if (!punta1Conectada || !punta2Conectada)
        {
            Debug.Log("Restableciendo lectura a 0");
            panelMultimetro.text = "0";
        }
    }

    // Mostrar lectura de 220 en el multímetro
    private void MostrarLectura()
    {
        panelMultimetro.text = "220";  // Mostrar 220 cuando ambas puntas estén conectadas
    }
}
