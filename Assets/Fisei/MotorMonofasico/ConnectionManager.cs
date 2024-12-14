using UnityEngine;
using System.Collections.Generic;

public class ConnectionManager : MonoBehaviour
{
    public SocketPoint[] sourceSockets; // Sockets de la fuente (R, S, T, N)
    public SocketPoint[] contactorSockets; // Sockets de los contactores (A1, A2, etc.)
    public SocketPoint[] pulsadorSockets; // Sockets de los pulsadores (si existen)
    public TutorialManager tutorialManager; // Referencia al TutorialManager.

    public ConnectionRule[] stepRules; // Reglas para completar este paso.
    public GameObject[] ruleCanvases; // Array de Canvases para cada regla de conexión.

    private bool stepCompleted = false; // Controla si el paso ya se completó.

    void Update()
    {
        // Si el paso no está completo, verificar las conexiones.
        if (!stepCompleted)
        {
            bool allRulesMet = true;

            // Verificar cada regla y activar/desactivar el canvas correspondiente.
            for (int i = 0; i < stepRules.Length; i++)
            {
                if (!AreConnectionsCorrect(i))
                {
                    allRulesMet = false;
                    ruleCanvases[i].SetActive(true); // Activar el Canvas correspondiente si la regla no se cumple.
                }
                else
                {
                    ruleCanvases[i].SetActive(false); // Desactivar el Canvas si la regla está cumplida.
                }
            }

            // Si todas las reglas están correctas, avanzar al siguiente paso.
            if (allRulesMet)
            {
                Debug.Log("Paso completado: todas las conexiones correctas.");
                stepCompleted = true;
                tutorialManager.NextStep(); // Avanza al siguiente paso.
            }
        }
    }

    // Verifica si todas las reglas de conexión están completas.
    public bool AreConnectionsCorrect(int ruleIndex)
    {
        if (ruleIndex < 0 || ruleIndex >= stepRules.Length)
        {
            Debug.LogError("El índice de la regla está fuera de los límites.");
            return false;
        }

        ConnectionRule rule = stepRules[ruleIndex];

        // Verificar si el socket de la fuente está conectado.
        if (!rule.IsSourceSocketConnected())
        {
            return false;
        }

        // Obtener el socket de destino conectado.
        SocketPoint connectedSocket = rule.GetConnectedSocket();

        // Verificar si la conexión es válida para este socket de destino
        if (!rule.IsValidConnection(connectedSocket))
        {
            return false;
        }

        return true;
    }

    [System.Serializable]
    public class ConnectionRule
    {
        public List<SocketPoint> sourceSockets; // Lista de sockets fuente (ej. A1, S)
        public List<SocketPoint> targetSockets; // Lista de sockets destino (ej. Pulsador1, Pulsador2)

        // Verificar si alguno de los sockets fuente está conectado.
        public bool IsSourceSocketConnected()
        {
            foreach (SocketPoint source in sourceSockets)
            {
                if (source.IsConnected())
                {
                    return true;
                }
            }
            return false; // Si ninguno de los sockets fuente está conectado, devuelve false.
        }

        // Obtener el socket de destino que está conectado.
        public SocketPoint GetConnectedSocket()
        {
            foreach (SocketPoint target in targetSockets)
            {
                if (target.IsConnected())
                {
                    return target;
                }
            }
            return null; // Si no hay ningún socket de destino conectado, devuelve null.
        }

        // Verificar si la conexión es válida.
        public bool IsValidConnection(SocketPoint connectedSocket)
        {
            // Verificar si el socket de destino está en la lista de sockets válidos.
            return targetSockets.Contains(connectedSocket);
        }
    }
}
