using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CircuitValidator : MonoBehaviour
{
    public static CircuitValidator Instance { get; private set; }

    [SerializeField]
    private UIManager uiManager; // Referencia al UIManager

    [Header("Lista de Reglas")]
    [Tooltip("Lista de todas las reglas asociadas a la UI")]
    [SerializeField]
    private List<RuleUI> ruleUIs; // Lista única de todas las reglas asociadas a la UI

    private Dictionary<SocketType, List<SocketType>> connections = new Dictionary<SocketType, List<SocketType>>();
    private HashSet<SocketType> blockedSockets = new HashSet<SocketType>(); // Lista de sockets bloqueados



    // Diccionario de pares NO
    private Dictionary<SocketType, SocketType> noPairs = new Dictionary<SocketType, SocketType>()
    {
        { SocketType.NO1, SocketType.NO5 },
        { SocketType.NO2, SocketType.NO6 },
        { SocketType.NO3, SocketType.NO7 },
        { SocketType.NO4, SocketType.NO8 },
        { SocketType.NO5, SocketType.NO1 },
        { SocketType.NO6, SocketType.NO2 },
        { SocketType.NO7, SocketType.NO3 },
        { SocketType.NO8, SocketType.NO4 }
    };
    // Diccionario de pares Pulsador
    private Dictionary<SocketType, SocketType> pPairs = new Dictionary<SocketType, SocketType>()
    {
        { SocketType.P1, SocketType.P3 },
        { SocketType.P2, SocketType.P4},
        { SocketType.P3, SocketType.P1 },
        { SocketType.P4, SocketType.P2 }
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of CircuitValidator detected. Destroying duplicate.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        ValidateCircuit(); // Validar el circuito al inicio
    }

    /// <summary>
    /// Valida todas las reglas activas y actualiza la UI.
    /// </summary>
    public void ValidateCircuit()
    {
        Debug.Log("CircuitValidator - Validando el circuito...");

        List<bool> ruleStatuses = new List<bool>();

        foreach (var ruleUI in ruleUIs)
        {
            if (ruleUI.ruleSO == null)
            {
                Debug.LogWarning("CircuitValidator - Un RuleSO no está asignado en una regla.");
                ruleStatuses.Add(false);
                continue;
            }

            bool isValid = ruleUI.ruleSO.IsValid(connections);
            ruleStatuses.Add(isValid);

            Debug.Log($"{ruleUI.ruleSO.RuleName}: {(isValid ? "Cumplida" : "No Cumplida")}");
        }

        // Actualizar los textos de la UI
        uiManager.UpdateRuleTexts(ruleStatuses);
    }

    /// <summary>
    /// Verifica si un conjunto específico de reglas está cumplido.
    /// </summary>
    public bool AreRulesMet(List<int> ruleIndices)
    {
        foreach (int index in ruleIndices)
        {
            if (index < 0 || index >= ruleUIs.Count)
            {
                Debug.LogError($"CircuitValidator - Índice de regla inválido: {index}");
                return false;
            }

            if (!ruleUIs[index].ruleSO.IsValid(connections))
            {
                Debug.Log($"{ruleUIs[index].ruleSO.RuleName} no cumplida.");
                return false;
            }
        }

        Debug.Log("Todas las reglas especificadas están cumplidas.");
        return true;
    }

    /// <summary>
    /// Valida las reglas de un grupo específico por identificador.
    /// </summary>
    public bool AreGroupRulesMet(string groupId)
    {
        foreach (var ruleUI in ruleUIs)
        {
            if (ruleUI.ruleSO.GroupId == groupId && !ruleUI.ruleSO.IsValid(connections))
            {
                Debug.Log($"{ruleUI.ruleSO.RuleName} no cumplida en el grupo {groupId}.");
                return false;
            }
        }

        Debug.Log($"Todas las reglas del grupo {groupId} están cumplidas.");
        return true;
    }

    /// <summary>
    /// Actualiza una conexión entre dos sockets.
    /// </summary>
    public void UpdateConnection(SocketType s1, SocketType s2, bool isConnected)
    {
        if (connections.ContainsKey(s1) && connections[s1].Count > 0 && !connections[s1].Contains(s2))
        {
            Debug.LogWarning($"UpdateConnection - {s1} ya está conectado a otro socket.");
            return;
        }

        if (isConnected)
        {
            if (!connections.ContainsKey(s1)) connections[s1] = new List<SocketType>();
            if (!connections.ContainsKey(s2)) connections[s2] = new List<SocketType>();

            if (!connections[s1].Contains(s2)) connections[s1].Add(s2);
            if (!connections[s2].Contains(s1)) connections[s2].Add(s1);

            Debug.Log($"UpdateConnection - Conexión añadida: {s1} <--> {s2}");
        }
        else
        {
            if (connections.ContainsKey(s1)) connections[s1].Remove(s2);
            if (connections.ContainsKey(s2)) connections[s2].Remove(s1);

            Debug.Log($"UpdateConnection - Conexión eliminada: {s1} <--> {s2}");
        }

        ValidateCircuit();
    }

    /// <summary>
    /// Obtiene el par correspondiente de un socket NO.
    /// </summary>
    public SocketType GetPair(SocketType socket)
    {
        if (noPairs.ContainsKey(socket))
        {
            return noPairs[socket];
        }

        Debug.LogWarning($"GetPair - No se encontró pareja para el socket NO: {socket}");
        return socket; // Devuelve el mismo socket si no se encuentra un par
    }
    /// <summary>
    /// Obtiene el par correspondiente de un socket NC.
    /// </summary>
    public SocketType GetPairP(SocketType socket)
    {
        if (pPairs.ContainsKey(socket))
        {
            return pPairs[socket];
        }

        Debug.LogWarning($"GetPair - No se encontró pareja para el socket Pulsador: {socket}");
        return socket; // Devuelve el mismo socket si no se encuentra un par
    }

    /// <summary>
    /// Obtiene los NO utilizados por T y N en las reglas 3 y 4.
    /// </summary>
    public List<SocketType> GetUsedNOsByTAndN()
    {
        List<SocketType> usedNOs = new List<SocketType>();

        // NO utilizados por T
        if (connections.ContainsKey(SocketType.T))
        {
            foreach (var no in connections[SocketType.T])
            {
                if (no.ToString().StartsWith("NO"))
                {
                    usedNOs.Add(no);
                }
            }
        }

        // NO utilizados por N
        if (connections.ContainsKey(SocketType.N))
        {
            foreach (var no in connections[SocketType.N])
            {
                if (no.ToString().StartsWith("NO"))
                {
                    usedNOs.Add(no);
                }
            }
        }

        return usedNOs;
    }
    public List<SocketType> GetUsedNOsByCrossRandCrossSandT()
    {
        List<SocketType> usedNOs = new List<SocketType>();

        // NO utilizados por CrossR
        if (connections.ContainsKey(SocketType.CrossR))
        {
            foreach (var no in connections[SocketType.CrossR])
            {
                if (no.ToString().StartsWith("NO"))
                {
                    usedNOs.Add(no);
                }
            }
        }

        // NO utilizados por CrossS
        if (connections.ContainsKey(SocketType.CrossS))
        {
            foreach (var no in connections[SocketType.CrossS])
            {
                if (no.ToString().StartsWith("NO"))
                {
                    usedNOs.Add(no);
                }
            }
        }

        // NO utilizados por T
        if (connections.ContainsKey(SocketType.T))
        {
            foreach (var no in connections[SocketType.T])
            {
                if (no.ToString().StartsWith("NO"))
                {
                    usedNOs.Add(no);
                }
            }
        }

        Debug.Log($"GetUsedNOsByCrossRandCrossSandT - NO utilizados: {string.Join(", ", usedNOs)}");
        return usedNOs;
    }

    public List<SocketType> GetUsedNOsByCrossR()
    {
        List<SocketType> usedNOs = new List<SocketType>();

        // Buscar conexiones de T a NO1, NO2, NO3, NO4
        if (connections.ContainsKey(SocketType.CrossR))
        {
            foreach (var no in connections[SocketType.CrossR])
            {
                if (no.ToString().StartsWith("NO")) // Verifica que el socket es un NO
                {
                    usedNOs.Add(no);
                }
            }
        }

        Debug.Log($"GetUsedNOsByT - NOs utilizados por CroossR: {string.Join(", ", usedNOs)}");
        return usedNOs;
    }

    /// <summary>
    /// Obtiene los pares de los NO utilizados.
    /// </summary>
    public List<SocketType> GetPairsForUsedNOs(List<SocketType> usedNOs)
    {
        List<SocketType> pairs = new List<SocketType>();
        foreach (var no in usedNOs)
        {
            pairs.Add(GetPair(no));
        }
        return pairs;
    }
    /// <summary>
    /// Obtiene el pulsador restante no conectado (P1R o P2R).
    /// </summary>
    public SocketType? GetRemainingP1RorP2R()
    {
        if (connections.ContainsKey(SocketType.R))
        {
            if (connections[SocketType.R].Contains(SocketType.P1R)) return SocketType.P2R;
            if (connections[SocketType.R].Contains(SocketType.P2R)) return SocketType.P1R;
        }
        return null;
    }

    /// <summary>
    /// Obtiene el pulsador no conectado (P1, P2, P3, P4).
    /// </summary>
    public SocketType? GetRemainingP()
    {
        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            bool isUsed = connections.ContainsKey(p) && connections[p].Count > 0;
            if (!isUsed) return p;
        }
        return null;
    }

    /// <summary>
    /// Obtiene los NO restantes no conectados.
    /// </summary>
    public SocketType? GetRemainingNO()
    {
        foreach (var no in new[] { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 })
        {
            bool isUsed = connections.ContainsKey(no) && connections[no].Count > 0;
            if (!isUsed) return no;
        }
        return null;
    }

    /// <summary>
    /// Obtiene el MO restante no conectado.
    /// </summary>
    public SocketType? GetRemainingMO()
    {
        foreach (var mo in new[] { SocketType.MO1, SocketType.MO2 })
        {
            bool isUsed = connections.ContainsKey(mo) && connections[mo].Count > 0;
            if (!isUsed) return mo;
        }
        return null;
    }
    public SocketType? GetRemainingNOA()
    {
        foreach (var noA in new[] { SocketType.NO1A, SocketType.NO2A })
        {
            bool isUsed = connections.ContainsKey(noA) && connections[noA].Count > 0;
            if (!isUsed) return noA;
        }
        return null; // Si no hay NO restante
    }

    public List<SocketType> GetUsedPulsadores()
    {
        List<SocketType> usedPulsadores = new List<SocketType>();
        foreach (var key in connections.Keys)
        {
            if (key.ToString().StartsWith("P"))
            {
                usedPulsadores.Add(key);
            }
        }
        return usedPulsadores;
    }
    /// <summary>
    /// Obtiene la pareja restante de pulsadores que no están conectados.
    /// </summary>
    /// <param name="usedPulsadores">Lista de pulsadores ya utilizados.</param>
    /// <returns>Lista de SocketType que representan la pareja restante o null si no hay pareja restante.</returns>
    public List<SocketType[]> GetRemainingPulsadorPairs()
    {
        var allPairs = new List<SocketType[]>
    {
        new[] { SocketType.P1, SocketType.P3 },
        new[] { SocketType.P2, SocketType.P4 }
    };

        List<SocketType[]> remainingPairs = new List<SocketType[]>();

        foreach (var pair in allPairs)
        {
            if ((!connections.ContainsKey(pair[0]) || connections[pair[0]].Count == 0) &&
                (!connections.ContainsKey(pair[1]) || connections[pair[1]].Count == 0))
            {
                remainingPairs.Add(pair);
            }
        }

        Debug.Log($"CircuitValidator - Parejas de pulsadores restantes: {string.Join(" | ", remainingPairs.Select(p => $"{p[0]}-{p[1]}"))}");
        return remainingPairs;
    }

    public List<SocketType> GetUnusedPulsadores()
    {
        List<SocketType> allPulsadores = new List<SocketType> { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 };
        List<SocketType> unusedPulsadores = new List<SocketType>();

        foreach (var pulsador in allPulsadores)
        {
            if (!connections.ContainsKey(pulsador) || connections[pulsador].Count == 0)
            {
                unusedPulsadores.Add(pulsador);
            }
        }

        Debug.Log($"CircuitValidator - Pulsadores no utilizados: {string.Join(", ", unusedPulsadores)}");
        return unusedPulsadores;
    }
    /// <summary>
    /// Obtiene el NO utilizado por un socket específico.
    /// </summary>
    /// <param name="socket">El socket del cual se busca el NO conectado.</param>
    /// <returns>El NO utilizado o null si no hay conexión válida.</returns>
    public SocketType? GetUsedNO(SocketType socket)
    {
        if (connections.ContainsKey(socket))
        {
            foreach (var connectedSocket in connections[socket])
            {
                if (connectedSocket.ToString().StartsWith("NO"))
                {
                    return connectedSocket;
                }
            }
        }

        Debug.Log($"GetUsedNO - No se encontró un NO conectado al socket {socket}");
        return null;
    }
    /// <summary>
    /// Obtiene las parejas de NO que han sido utilizadas en las conexiones actuales.
    /// </summary>
    public List<SocketType> GetUsedNOPairs()
    {
        List<SocketType> usedNOPairs = new List<SocketType>();

        foreach (var connection in connections)
        {
            // Buscar conexiones entre NOs y agregar sus pares a la lista
            if (connection.Key.ToString().StartsWith("NO"))
            {
                foreach (var connectedSocket in connection.Value)
                {
                    if (connectedSocket.ToString().StartsWith("NO"))
                    {
                        // Agregar ambos sockets a la lista de usados
                        usedNOPairs.Add(GetPair(connection.Key));
                    }
                }
            }
        }

        Debug.Log($"GetUsedNOPairs - Pares de NO utilizados: {string.Join(", ", usedNOPairs)}");
        return usedNOPairs;
    }
    /// <summary>
    /// Obtiene las parejas de NO que no han sido utilizadas, excluyendo las parejas ya usadas por los sockets especificados.
    /// </summary>
    /// <param name="excludedSockets">Sockets que se deben excluir al buscar las parejas no utilizadas.</param>
    /// <returns>Lista de parejas NO no utilizadas.</returns>
    public List<SocketType> GetUnusedNOPairs(IEnumerable<SocketType> excludedSockets)
    {
        // Lista de todas las parejas NO
        List<SocketType> allNOPairs = new List<SocketType>(noPairs.Values);

        // Lista de parejas NO ya utilizadas
        List<SocketType> usedNOPairs = new List<SocketType>();

        // Verificar las conexiones existentes y agregar las parejas utilizadas
        foreach (var socket in excludedSockets)
        {
            if (connections.ContainsKey(socket))
            {
                foreach (var connectedSocket in connections[socket])
                {
                    if (noPairs.ContainsKey(connectedSocket))
                    {
                        usedNOPairs.Add(noPairs[connectedSocket]);
                    }
                }
            }
        }

        // Filtrar las parejas no utilizadas
        List<SocketType> unusedNOPairs = allNOPairs.FindAll(pair => !usedNOPairs.Contains(pair));

        Debug.Log($"GetUnusedNOPairs - Pares NO no utilizados: {string.Join(", ", unusedNOPairs)}");

        return unusedNOPairs;
    }
    public List<SocketType> GetAllUsedNOPairs()
    {
        List<SocketType> usedPairs = new List<SocketType>();

        foreach (var entry in connections)
        {
            foreach (var connectedSocket in entry.Value)
            {
                if (entry.Key.ToString().StartsWith("NO") && noPairs.ContainsKey(entry.Key))
                {
                    SocketType pair = GetPair(entry.Key);
                    if (connections.ContainsKey(pair) && connections[pair].Contains(entry.Key))
                    {
                        if (!usedPairs.Contains(entry.Key) && !usedPairs.Contains(pair))
                        {
                            usedPairs.Add(entry.Key);
                            usedPairs.Add(pair);
                        }
                    }
                }
            }
        }

        Debug.Log($"GetAllUsedNOPairs - Pares usados: {string.Join(", ", usedPairs)}");
        return usedPairs;
    }

    public List<SocketType> GetRemainingNOPairs(List<SocketType> usedNOPairs, SocketType[] excludedSockets)
    {
        List<SocketType> remainingPairs = new List<SocketType>();
        foreach (var pair in noPairs.Values)
        {
            if (!usedNOPairs.Contains(pair) && !excludedSockets.Contains(pair))
            {
                remainingPairs.Add(pair);
            }
        }
        return remainingPairs;
    }
    public List<SocketType> GetUnusedNOPairs()
    {
        var allNOPairs = new Dictionary<SocketType, SocketType>
    {
        { SocketType.NO1, SocketType.NO5 },
        { SocketType.NO2, SocketType.NO6 },
        { SocketType.NO3, SocketType.NO7 },
        { SocketType.NO4, SocketType.NO8 },
        { SocketType.NO5, SocketType.NO1 },
        { SocketType.NO6, SocketType.NO2 },
        { SocketType.NO7, SocketType.NO3 },
        { SocketType.NO8, SocketType.NO4 }
    };

        List<SocketType> unusedPairs = new List<SocketType>();

        foreach (var pair in allNOPairs)
        {
            if (!connections.ContainsKey(pair.Key) && !connections.ContainsKey(pair.Value))
            {
                unusedPairs.Add(pair.Key);
                unusedPairs.Add(pair.Value);
            }
        }

        return unusedPairs;
    }








}
