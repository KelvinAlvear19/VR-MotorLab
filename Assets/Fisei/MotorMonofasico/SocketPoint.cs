using UnityEngine;

public class SocketPoint : MonoBehaviour
{
    public Renderer socketRenderer; // Renderer para cambiar el color del socket.
    public Color defaultColor = Color.white; // Color por defecto del socket.
    public Color hoverColor = Color.yellow;  // Color cuando el usuario apunta al socket.
    public Color validConnectionColor = Color.green; // Color cuando hay conexión válida.
    public Color invalidConnectionColor = Color.red; // Color cuando hay conexión inválida.

    private GameObject connectedPlug; // Parte del plug conectada.
    private GameObject connectedCopper; // Parte de cobre conectada.

    void Start()
    {
        // Asegurarse de que el socket comienza con el color por defecto.
        if (socketRenderer != null)
        {
            socketRenderer.material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cambiar el color al hover cuando el cable entra.
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper"))
        {
            SetSocketColor(hoverColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Restaurar el color cuando el cable sale, si no hay conexión.
        if (other.CompareTag("CablePlug") && connectedPlug == null)
        {
            SetSocketColor(defaultColor);
        }
        else if (other.CompareTag("CableCopper") && connectedCopper == null)
        {
            SetSocketColor(defaultColor);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CablePlug"))
        {
            connectedPlug = collision.gameObject; // Guardar el plug conectado.

            if (IsConnectionValid(collision.gameObject.tag))
            {
                SetSocketColor(validConnectionColor);
                Debug.Log($"Plug {collision.gameObject.name} conectado correctamente al socket {gameObject.name}");
            }
            else
            {
                SetSocketColor(invalidConnectionColor);
                Debug.Log($"Conexión inválida: Plug {collision.gameObject.name} al socket {gameObject.name}");
            }
        }
        else if (collision.gameObject.CompareTag("CableCopper"))
        {
            connectedCopper = collision.gameObject; // Guardar la parte de cobre conectada.

            if (IsConnectionValid(collision.gameObject.tag))
            {
                SetSocketColor(validConnectionColor);
                Debug.Log($"Copper {collision.gameObject.name} conectado correctamente al socket {gameObject.name}");
            }
            else
            {
                SetSocketColor(invalidConnectionColor);
                Debug.Log($"Conexión inválida: Copper {collision.gameObject.name} al socket {gameObject.name}");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == connectedPlug)
        {
            connectedPlug = null; // Eliminar la referencia al plug conectado.
            SetSocketColor(defaultColor); // Restaurar el color por defecto.
        }
        else if (collision.gameObject == connectedCopper)
        {
            connectedCopper = null; // Eliminar la referencia a la parte de cobre conectada.
            SetSocketColor(defaultColor); // Restaurar el color por defecto.
        }
    }

    // Cambiar el color del socket.
    private void SetSocketColor(Color color)
    {
        if (socketRenderer != null)
        {
            socketRenderer.material.color = color;
        }
    }

    // Lógica para validar si el cable conectado es correcto.
    private bool IsConnectionValid(string cableTag)
    {
        // Aquí puedes añadir reglas específicas para cada tipo de cable.
        return cableTag == "RequiredCable"; // Cambia esto según tu lógica.
    }

    // Verificar si un cable está conectado.
    public bool IsConnected()
    {
        return connectedPlug != null || connectedCopper != null; // Devuelve verdadero si hay un cable conectado.
    }

    // Obtener el socket conectado.
    public SocketPoint GetConnectedSocket()
    {
        // Devuelve el socket al que se ha conectado este socket, si existe.
        return connectedPlug != null || connectedCopper != null ? this : null;
    }
}
