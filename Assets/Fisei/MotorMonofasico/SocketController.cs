using UnityEngine;

public class SocketController : MonoBehaviour
{
    [SerializeField] private string socketID; // ID �nico para cada socket
    [SerializeField] private bool isConnected = false; // Estado de conexi�n del socket
    private ReglaValidator reglaValidator; // Referencia al script principal

    private void Awake()
    {
        // Obtener la referencia al script ReglaValidator
        reglaValidator = FindObjectOfType<ReglaValidator>();
        if (reglaValidator == null)
        {
            //Debug.LogError("No se encontr� el script ReglaValidator en la escena.");
        }
    }

    // M�todo para cambiar el estado de conexi�n
    public void SetSocketState(bool estado)
    {
        if (isConnected != estado)
        {
            //Debug.Log($"Socket {socketID}: Estado cambiando de {isConnected} a {estado}");
            isConnected = estado;

            if (reglaValidator != null)
            {
                // Actualizar el estado a trav�s de ReglaValidator
                reglaValidator.ActualizarEstadoSocket(this, isConnected);
            }
        }
        else
        {
           // Debug.Log($"Socket {socketID}: Estado ya est� en {isConnected}, no se cambia.");
        }
    }



    // M�todo para obtener el estado del socket
    public bool GetSocketState()
    {
        return isConnected;
    }

    // Mostrar el estado de conexi�n del socket
    public void DebugEstadoConexion()
    {
       // Debug.Log($"Estado del socket {socketID}: {(isConnected ? "Conectado" : "Desconectado")}");
    }

    // Detecta cuando un objeto entra en el �rea del socket
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Objeto detectado: {other.gameObject.name}, Etiqueta: {other.tag}");
        // Verificar si el objeto que entr� tiene la etiqueta o componente correcto
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper") || other.CompareTag("Cobre2") ||
            other.CompareTag("Cobre3") || other.CompareTag("Cobre4") || other.CompareTag("Cobre1") ||
            other.CompareTag("Cobre5") || other.CompareTag("Cobre6")) // Aseg�rate de asignar la etiqueta "Connector" a los objetos que se conectan
        {
           // Debug.Log($"Objeto conectado al socket {socketID}: {other.gameObject.name}");
           // SetSocketState(true); // Cambiar el estado del socket a conectado

            // Aqu� tambi�n puedes agregar cualquier l�gica adicional que quieras para cuando se conecte un objeto
        }
    }

    // Detecta cuando un objeto sale del �rea del socket
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sali� tiene la etiqueta o componente correcto
        if (other.CompareTag("CablePlug") || other.CompareTag("CableCopper") || other.CompareTag("Cobre2") ||
            other.CompareTag("Cobre3") || other.CompareTag("Cobre4") || other.CompareTag("Cobre1") ||
            other.CompareTag("Cobre5") || other.CompareTag("Cobre6")) // Aseg�rate de asignar la etiqueta "Connector"
        {
          //  Debug.Log($"Objeto desconectado del socket {socketID}: {other.gameObject.name}");
            SetSocketState(false); // Cambiar el estado del socket a desconectado

            // Aqu� tambi�n puedes agregar cualquier l�gica adicional para cuando se desconecte un objeto
        }
    }
}
