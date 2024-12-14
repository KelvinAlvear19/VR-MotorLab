using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ReglaValidator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoReglaR;
    [SerializeField] private TextMeshProUGUI textoReglaS;
    [SerializeField] private TextMeshProUGUI textoReglaT;
    [SerializeField] private TextMeshProUGUI textoReglaN;
    [SerializeField] private TextMeshProUGUI textoReglaA;
    [SerializeField] private TextMeshProUGUI textoReglaNO3Motor1;
    [SerializeField] private TextMeshProUGUI textoReglaNO4Motor2;
    [SerializeField] private SocketController socketNO3Controller;
    [SerializeField] private SocketController socketMotor1Controller;
    [SerializeField] private SocketController socketNO4Controller;
    [SerializeField] private SocketController socketMotor2Controller;

    [SerializeField] private SocketController socketRController;
    [SerializeField] private SocketController socketSController;
    [SerializeField] private SocketController socketTController;
    [SerializeField] private SocketController socketNController;
    [SerializeField] private SocketController socketA1Controller;
    [SerializeField] private SocketController socketA2Controller;
    [SerializeField] private SocketController socketP1Controller;
    [SerializeField] private SocketController socketP2Controller;
    [SerializeField] private SocketController socketNO1Controller;
    [SerializeField] private SocketController socketNO2Controller;

    private List<SocketController> socketsLibresNO = new List<SocketController>(); // Para almacenar NO libres

    private void Start()
    {
        // Inicializar sockets NO como libres
        socketsLibresNO.Add(socketNO1Controller);
        socketsLibresNO.Add(socketNO2Controller);
        socketsLibresNO.Add(socketNO3Controller);
        socketsLibresNO.Add(socketNO4Controller);
    }

    private void Update()
    {
        VerificarConexiones();
    }

    public bool VerificarConexiones()
    {
        bool reglasInicialesValidas = VerificarReglasIniciales();
        return reglasInicialesValidas;
    }

    public bool VerificarReglasIniciales()
    {
        bool reglasInicialesValidas = true;

        // Regla 1: R se conecta a P1 o P2
        if (socketRController.GetSocketState() && (socketP1Controller.GetSocketState() || socketP2Controller.GetSocketState()))
        {
            textoReglaR.color = Color.green;
        }
        else
        {
            textoReglaR.color = Color.red;
            reglasInicialesValidas = false;
        }

        // Regla 2: S se conecta a A1 o A2
        if (socketSController.GetSocketState() && (socketA1Controller.GetSocketState() || socketA2Controller.GetSocketState()))
        {
            textoReglaS.color = Color.green;
        }
        else
        {
            textoReglaS.color = Color.red;
            reglasInicialesValidas = false;
        }

        // Regla 3: T se conecta dinámicamente a NO1, NO2, NO3, NO4
        if (Regla3())
        {
            textoReglaT.color = Color.green;
        }
        else
        {
            textoReglaT.color = Color.red;
            reglasInicialesValidas = false;
        }

        // Regla 4: N se conecta solo a los NO libres
        if (Regla4())
        {
            textoReglaN.color = Color.green;
        }
        else
        {
            textoReglaN.color = Color.red;
            reglasInicialesValidas = false;
        }

        // Regla 5: Un socket libre (A1 o A2) se conecta a un socket libre (P1 o P2)
        if (Regla5())
        {
            textoReglaA.color = Color.green;
        }
        else
        {
            textoReglaA.color = Color.red;
            reglasInicialesValidas = false;
        }

        return reglasInicialesValidas;
    }

    private bool Regla3()
    {
        foreach (var socket in socketsLibresNO)
        {
            if (!socketTController.GetSocketState() && !socket.GetSocketState())
            {
                ActualizarEstadoSocket(socketTController, true);
                ActualizarEstadoSocket(socket, true);

                // Eliminar el socket de la lista de libres
                socketsLibresNO.Remove(socket);
                Debug.Log($"T conectado a {socket.name}");
                return true;
            }
        }

        return false; // No se pudo realizar la conexión
    }

    private bool Regla4()
    {
        foreach (var socket in socketsLibresNO)
        {
            if (!socketNController.GetSocketState() && !socket.GetSocketState())
            {
                ActualizarEstadoSocket(socketNController, true);
                ActualizarEstadoSocket(socket, true);

                // Eliminar el socket de la lista de libres
                socketsLibresNO.Remove(socket);
                Debug.Log($"N conectado a {socket.name}");
                return true;
            }
        }

        return false; // No se pudo realizar la conexión
    }


    private bool Regla5()
    {
        SocketController libreA = null;
        SocketController libreP = null;

        // Identificar el socket libre en A1 o A2
        if (!socketA1Controller.GetSocketState()) libreA = socketA1Controller;
        else if (!socketA2Controller.GetSocketState()) libreA = socketA2Controller;

        // Identificar el socket libre en P1 o P2
        if (!socketP1Controller.GetSocketState()) libreP = socketP1Controller;
        else if (!socketP2Controller.GetSocketState()) libreP = socketP2Controller;

        // Realizar la conexión si hay sockets libres
        if (libreA != null && libreP != null)
        {
            ActualizarEstadoSocket(libreA, true);
            ActualizarEstadoSocket(libreP, true);

            Debug.Log($"Conectando {libreA.name} con {libreP.name}");
            return true;
        }

        return false; // No hay sockets libres
    }

    public bool VerificarReglasAdicionales()
    {
        bool reglasAdicionalesValidas = true;

        // Verificar las nuevas reglas (NO3 -> Motor1, NO4 -> Motor2)
        if (socketNO3Controller.GetSocketState() && socketMotor1Controller.GetSocketState())
        {
            textoReglaNO3Motor1.color = Color.green;
        }
        else
        {
            textoReglaNO3Motor1.color = Color.red;
            reglasAdicionalesValidas = false;
        }

        if (socketNO4Controller.GetSocketState() && socketMotor2Controller.GetSocketState())
        {
            textoReglaNO4Motor2.color = Color.green;
        }
        else
        {
            textoReglaNO4Motor2.color = Color.red;
            reglasAdicionalesValidas = false;
        }

        return reglasAdicionalesValidas;
    }
    public void ActualizarEstadoSocket(SocketController socket, bool estado)
    {
        socket.SetSocketState(estado);
        Debug.Log($"Estado actualizado del socket {socket.name}: {(estado ? "Conectado" : "Desconectado")}");
    }

}
