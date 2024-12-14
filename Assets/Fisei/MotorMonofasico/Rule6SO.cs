// Rule6SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule6", menuName = "Rules/Rule6")]
public class Rule6SO : RuleSO
{
    // Define las parejas de NO
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

    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        Debug.Log("Rule6SO - Inicio de validación.");

        // Obtener los NO utilizados en reglas 1 a 5
        List<SocketType> usedNOs = GetUsedNOs(connections); // NO1 a NO4 utilizados

        // Verificar que al menos una conexión se haya realizado para regla6
        if (usedNOs.Count == 0)
        {
            Debug.Log("Rule6SO - No hay NO utilizados en reglas 1 a 5. Regla 6 no cumplida.");
            return false;
        }

        // Identificar solo el primer NO utilizado
        SocketType firstUsedNO = usedNOs[0];
        SocketType pairedNO = GetPair(firstUsedNO);

        bool connectedToMO1 = IsConnected(connections, pairedNO, SocketType.MO1);
        bool connectedToMO2 = IsConnected(connections, pairedNO, SocketType.MO2);

        Debug.Log($"Rule6SO - {pairedNO} conectado a MO1: {connectedToMO1}, conectado a MO2: {connectedToMO2}");

        if (connectedToMO1 || connectedToMO2)
        {
            Debug.Log("Rule6SO - Se cumple la Regla 6.");
            return true;
        }
        else
        {
            Debug.Log("Rule6SO - No se cumple la Regla 6.");
            return false;
        }
    }

    /// <summary>
    /// Obtiene los NO utilizados en las primeras 5 reglas.
    /// </summary>
    private List<SocketType> GetUsedNOs(Dictionary<SocketType, List<SocketType>> connections)
    {
        List<SocketType> usedNOs = new List<SocketType>();

        // Obtener NOs conectados a T
        if (connections.ContainsKey(SocketType.T))
        {
            foreach (var no in connections[SocketType.T])
            {
                if (no == SocketType.NO1 || no == SocketType.NO2 || no == SocketType.NO3 || no == SocketType.NO4)
                {
                    usedNOs.Add(no);
                    Debug.Log($"Rule6SO - NO {no} conectado a T.");
                }
            }
        }

        // Obtener NOs conectados a N
        if (connections.ContainsKey(SocketType.N))
        {
            foreach (var no in connections[SocketType.N])
            {
                if (no == SocketType.NO1 || no == SocketType.NO4)
                {
                    usedNOs.Add(no);
                    Debug.Log($"Rule6SO - NO {no} conectado a N.");
                }
            }
        }

        // Eliminar duplicados
        List<SocketType> result = new List<SocketType>(new HashSet<SocketType>(usedNOs));
        Debug.Log($"Rule6SO - NOs utilizados en reglas 1 a 5: {string.Join(", ", result)}");
        return result;
    }

    /// <summary>
    /// Obtiene la pareja de un NO.
    /// </summary>
    private SocketType GetPair(SocketType no)
    {
        if (noPairs.ContainsKey(no))
        {
            SocketType pair = noPairs[no];
            Debug.Log($"Rule6SO - La pareja de {no} es {pair}.");
            return pair;
        }
        else
        {
            Debug.LogError($"Rule6SO - No se encontró pareja para el socket: {no}");
            throw new System.Exception($"No se encontró pareja para el socket: {no}");
        }
    }

    /// <summary>
    /// Verifica si un NO está conectado a un MO específico.
    /// </summary>
    private bool IsConnected(Dictionary<SocketType, List<SocketType>> connections, SocketType no, SocketType mo)
    {
        bool connected = connections.ContainsKey(no) && connections[no].Contains(mo);
        Debug.Log($"Rule6SO - {no} {(connected ? "está" : "no está")} conectado a {mo}.");
        return connected;
    }
}
