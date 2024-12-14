// CircuitValidator.cs
using UnityEngine;
using System.Collections.Generic;

public class CircuitValidator : MonoBehaviour
{
    // Singleton Instance
    public static CircuitValidator Instance { get; private set; }

    [SerializeField]
    private UIManager uiManager;

    [Header("ScriptableObjects de Reglas")]
    [SerializeField]
    private List<RuleSO> firstFiveRulesSO; // Asigna las primeras 5 reglas en el Inspector
    [SerializeField]
    private List<RuleSO> additionalRulesSO; // Asigna las reglas 6 y 7 en el Inspector

    // Diccionario de conexiones: connections[socket] = List de otroSocket
    // CircuitValidator.cs
    private Dictionary<SocketType, SocketType> connections = new Dictionary<SocketType, SocketType>();


    // Mapeo de parejas de NO
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

    // Listas de reglas activas
    private List<IRule> firstFiveRules = new List<IRule>();
    private List<IRule> additionalRules = new List<IRule>();

    // Flags para verificar el estado de las reglas
    private bool areFirstFiveMet = false;
    private bool areAllSevenMet = false;

    [Header("Indicadores de UI")]
    [SerializeField]
    private GameObject firstFiveMetIndicator; // Asigna en el Inspector
    [SerializeField]
    private GameObject allSevenMetIndicator; // Asigna en el Inspector

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple instances of CircuitValidator detected. Destroying duplicate.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (uiManager == null)
        {
            Debug.LogError("UIManager no está asignado en CircuitValidator.");
        }

        // Inicializar reglas desde ScriptableObjects
        InitializeRules();
    }

    /// <summary>
    /// Inicializa las reglas activas desde los ScriptableObjects asignados.
    /// </summary>
    private void InitializeRules()
    {
        // Inicializar las primeras 5 reglas
        foreach (var ruleSO in firstFiveRulesSO)
        {
            firstFiveRules.Add(new RuleFromSO(ruleSO));
        }

        // Inicializar las reglas adicionales (6 y 7)
        foreach (var ruleSO in additionalRulesSO)
        {
            additionalRules.Add(new RuleFromSO(ruleSO));
        }

        Debug.Log("CircuitValidator - Reglas inicializadas correctamente.");
        ValidateCircuit();
    }

    /// <summary>
    /// Clase adaptadora para utilizar ScriptableObjects como IRule.
    /// </summary>
    private class RuleFromSO : IRule
    {
        private RuleSO ruleSO;

        public string RuleName => ruleSO.name;

        public RuleFromSO(RuleSO ruleSO)
        {
            this.ruleSO = ruleSO;
        }

        public bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
        {
            return ruleSO.IsValid(connections);
        }
    }


    /// <summary>
    /// Valida todas las reglas activas y actualiza la UI.
    /// Además, actualiza los flags para las reglas cumplidas.
    /// </summary>
    public void ValidateCircuit()
    {
        Debug.Log("CircuitValidator - Inicio de validación del circuito.");

        // Convertir connections a Dictionary<SocketType, List<SocketType>>
        var connectionsAsLists = ConvertConnectionsToList();

        // Validar las primeras 5 reglas
        areFirstFiveMet = true;
        foreach (var rule in firstFiveRules)
        {
            bool isValid = rule.IsValid(connectionsAsLists);
            areFirstFiveMet &= isValid;
            Debug.Log($"CircuitValidator - {rule.RuleName} - {(isValid ? "Cumplida" : "No cumplida")}");
        }

        // Validar las reglas adicionales (6 y 7)
        areAllSevenMet = areFirstFiveMet;
        foreach (var rule in additionalRules)
        {
            bool isValid = rule.IsValid(connectionsAsLists);
            areAllSevenMet &= isValid;
            Debug.Log($"CircuitValidator - {rule.RuleName} - {(isValid ? "Cumplida" : "No cumplida")}");
        }

        // Actualizar la UI
        bool[] allRuleStatuses = new bool[firstFiveRules.Count + additionalRules.Count];
        for (int i = 0; i < firstFiveRules.Count; i++)
        {
            allRuleStatuses[i] = firstFiveRules[i].IsValid(connectionsAsLists);
        }
        for (int i = 0; i < additionalRules.Count; i++)
        {
            allRuleStatuses[firstFiveRules.Count + i] = additionalRules[i].IsValid(connectionsAsLists);
        }
        uiManager.UpdateRuleColors(allRuleStatuses);

        // Ejecutar acciones basadas en las reglas cumplidas
        if (areFirstFiveMet)
        {
            OnFirstFiveRulesMet();
        }
        else
        {
            OnFirstFiveRulesNotMet();
        }

        if (areAllSevenMet)
        {
            OnAllSevenRulesMet();
        }
        else
        {
            OnAllSevenRulesNotMet();
        }
    }

    /// <summary>
    /// Convierte el diccionario de conexiones a un formato compatible con las reglas.
    /// </summary>
    private Dictionary<SocketType, List<SocketType>> ConvertConnectionsToList()
    {
        var connectionsAsLists = new Dictionary<SocketType, List<SocketType>>();

        foreach (var kvp in connections)
        {
            if (!connectionsAsLists.ContainsKey(kvp.Key))
            {
                connectionsAsLists[kvp.Key] = new List<SocketType>();
            }
            connectionsAsLists[kvp.Key].Add(kvp.Value);
        }

        return connectionsAsLists;
    }


    /// <summary>
    /// Acción a ejecutar cuando se cumplen las primeras 5 reglas.
    /// </summary>
    private void OnFirstFiveRulesMet()
    {
        Debug.Log("CircuitValidator - Se han cumplido las primeras 5 reglas.");
        if (firstFiveMetIndicator != null)
        {
            firstFiveMetIndicator.SetActive(true);
        }
    }

    /// <summary>
    /// Acción a ejecutar cuando NO se cumplen las primeras 5 reglas.
    /// </summary>
    private void OnFirstFiveRulesNotMet()
    {
        Debug.Log("CircuitValidator - NO se han cumplido las primeras 5 reglas.");
        if (firstFiveMetIndicator != null)
        {
            firstFiveMetIndicator.SetActive(false);
        }
    }

    /// <summary>
    /// Acción a ejecutar cuando se cumplen todas las 7 reglas.
    /// </summary>
    private void OnAllSevenRulesMet()
    {
        Debug.Log("CircuitValidator - Se han cumplido todas las 7 reglas.");
        if (allSevenMetIndicator != null)
        {
            allSevenMetIndicator.SetActive(true);
        }
    }

    /// <summary>
    /// Acción a ejecutar cuando NO se cumplen todas las 7 reglas.
    /// </summary>
    private void OnAllSevenRulesNotMet()
    {
        Debug.Log("CircuitValidator - NO se han cumplido todas las 7 reglas.");
        if (allSevenMetIndicator != null)
        {
            allSevenMetIndicator.SetActive(false);
        }
    }

    /// <summary>
    /// Actualiza una conexión entre dos sockets.
    /// </summary>
    /// <param name="s1">Primer socket.</param>
    /// <param name="s2">Segundo socket.</param>
    /// <param name="isConnected">True para conectar, False para desconectar.</param>
    public void UpdateConnection(SocketType s1, SocketType s2, bool isConnected)
    {
        // Validación inicial: evitar que un socket se conecte a sí mismo
        if (s1 == s2)
        {
            Debug.LogError("UpdateConnection - Un socket no puede conectarse consigo mismo.");
            return;
        }

        // Conectar o desconectar
        if (isConnected)
        {
            // Conectar s1 con s2
            if (!connections.ContainsKey(s1))
            {
                connections[s1] = s2;
            }
            if (!connections.ContainsKey(s2))
            {
                connections[s2] = s1;
            }
            Debug.Log($"UpdateConnection - Conectado: {s1} <--> {s2}");
        }
        else
        {
            // Desconectar s1 de s2
            if (connections.ContainsKey(s1) && connections[s1] == s2)
            {
                connections.Remove(s1);
                Debug.Log($"UpdateConnection - Desconectado: {s1}");
            }

            if (connections.ContainsKey(s2) && connections[s2] == s1)
            {
                connections.Remove(s2);
                Debug.Log($"UpdateConnection - Desconectado: {s2}");
            }
        }

        // Validar el circuito después de cada cambio
        ValidateCircuit();
    }

    /// <summary>
    /// Verifica si dos sockets están conectados.
    /// </summary>
    /// <param name="s1">Primer socket.</param>
    /// <param name="s2">Segundo socket.</param>
    /// <returns>True si están conectados, de lo contrario, false.</returns>
    private bool IsConnected(SocketType s1, SocketType s2)
    {
        return connections.ContainsKey(s1) && connections[s1] == s2;
    }


    /// <summary>
    /// Obtiene el par de un socket NO.
    /// </summary>
    /// <param name="no">Socket NO.</param>
    /// <returns>Par de socket NO.</returns>
    private SocketType GetPair(SocketType no)
    {
        if (noPairs.ContainsKey(no))
        {
            return noPairs[no];
        }
        else
        {
            Debug.LogError($"CircuitValidator - No se encontró pareja para el socket: {no}");
            return no;
        }
    }

    /// <summary>
    /// Métodos públicos para verificar el estado de las reglas
    /// </summary>
    public bool AreFiveRulesMet()
    {
        return areFirstFiveMet;
    }

    public bool AreSevenRulesMet()
    {
        return areAllSevenMet;
    }

    /// <summary>
    /// Verifica si un SocketType es un MO.
    /// </summary>
    private bool IsMO(SocketType socket)
    {
        return socket == SocketType.MO1 || socket == SocketType.MO2;
    }

    /// <summary>
    /// Verifica si un SocketType es un NO.
    /// </summary>
    private bool IsNO(SocketType socket)
    {
        return socket.ToString().StartsWith("NO");
    }
}
