using UnityEngine;

public class SocketController : MonoBehaviour
{
    [SerializeField] private string socketID; // ID único para cada socket
    [SerializeField] private bool isConnected = false; // Estado de conexión del socket
    private ReglaValidator reglaValidator; // Referencia al script principal

    private void Awake()
    {
        // Obtener la referencia al script ReglaValidator
        reglaValidator = FindObjectOfType<ReglaValidator>();
        if (reglaValidator == null)
        {
            //Debug.LogError("No se encontró el script ReglaValidator en la escena.");
        }
    }

    // Método para cambiar el estado de conexión
    public void SetSocketState(bool estado)
    {
        if (isConnected != estado)
        {
            //Debug.Log($"Socket {socketID}: Estado cambiando de {isConnected} a {estado}");
            isConnected = estado;

            if (reglaValidator != null)
            {
                // Actualizar el estado a través de ReglaValidator
                reglaValidator.ActualizarEstadoSocket(this, isConnected);
            }
        }
        else
        {
           // Debug.Log($"Socket {socketID}: Estado ya está en {isConnected}, no se cambia.");
        }
    }



    // Método para obtener el estado del socket
    public bool GetSocketState()
    {
        return isConnected;
    }

    // Mostrar el estado de conexión del socket
    public void DebugEstadoConexion()
    {
       // Debug.Log($"Estado del socket {socketID}: {(isConnected ? "Conectado" : "Desconectado")}");
    }

    // Detecta cuando un objeto entra en el área del socket
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Objeto detectado: {other.gameObject.name}, Etiqueta: {other.tag}");
        // Verificar si el objeto que entró tiene la etiqueta o componente correcto
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper") || other.CompareTag("Cobre2") ||
            other.CompareTag("Cobre3") || other.CompareTag("Cobre4") || other.CompareTag("Cobre1") ||
            other.CompareTag("Cobre5") || other.CompareTag("Cobre6")) // Asegúrate de asignar la etiqueta "Connector" a los objetos que se conectan
        {
           // Debug.Log($"Objeto conectado al socket {socketID}: {other.gameObject.name}");
           // SetSocketState(true); // Cambiar el estado del socket a conectado

            // Aquí también puedes agregar cualquier lógica adicional que quieras para cuando se conecte un objeto
        }
    }

    // Detecta cuando un objeto sale del área del socket
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que salió tiene la etiqueta o componente correcto
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper") || other.CompareTag("Cobre2") ||
            other.CompareTag("Cobre3") || other.CompareTag("Cobre4") || other.CompareTag("Cobre1") ||
            other.CompareTag("Cobre5") || other.CompareTag("Cobre6")) // Asegúrate de asignar la etiqueta "Connector"
        {
          //  Debug.Log($"Objeto desconectado del socket {socketID}: {other.gameObject.name}");
            SetSocketState(false); // Cambiar el estado del socket a desconectado

            // Aquí también puedes agregar cualquier lógica adicional para cuando se desconecte un objeto
        }
    }
}
