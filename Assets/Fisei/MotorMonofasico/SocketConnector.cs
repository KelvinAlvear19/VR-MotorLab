// SocketConnector.cs
using UnityEngine;

public class SocketConnector : MonoBehaviour
{
    [SerializeField]
    private SocketIdentifier socketId;

    private void Awake()
    {
        if (socketId == null)
        {
            socketId = GetComponent<SocketIdentifier>();
            if (socketId == null)
            {
                Debug.LogError("SocketIdentifier no está asignado en SocketConnector.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Objeto detectadoSO: {other.gameObject.name}, Etiqueta: {other.tag}");
        // Verificar si el objeto que entra es un cable con la tag CablePlug o CableCopper
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper"))
        {
            CableEnd cableEnd = other.GetComponent<CableEnd>();
            if (cableEnd != null)
            {
                // Conectar el extremo del cable a este socket
                if (cableEnd.CableConnection != null)
                {
                    cableEnd.CableConnection.ConnectEnd(other.transform, socketId.SocketType);
                }
                else
                {
                    Debug.LogWarning($"CableConnection no está asignado en {other.gameObject.name}.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale es un cable con la tag CablePlug o CableCopper
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper"))
        {
            CableEnd cableEnd = other.GetComponent<CableEnd>();
            if (cableEnd != null)
            {
                // Desconectar el extremo del cable de este socket
                if (cableEnd.CableConnection != null)
                {
                    cableEnd.CableConnection.DisconnectEnd(other.transform, socketId.SocketType);
                }
                else
                {
                    Debug.LogWarning($"CableConnection no está asignado en {other.gameObject.name}.");
                }
            }
        }
    }

    /// <summary>
    /// Verifica si la etiqueta del objeto es válida para una conexión de cable.
    /// </summary>
    /// <param name="tag">Etiqueta del objeto a verificar.</param>
    /// <returns>True si es una etiqueta válida, de lo contrario, false.</returns>
    private bool IsValidCableTag(string tag)
    {
        return tag.Equals("CablePlug") || tag.Equals("CableCopper");
    }
}
