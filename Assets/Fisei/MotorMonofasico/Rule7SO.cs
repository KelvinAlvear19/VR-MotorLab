// Rule7SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule7", menuName = "Rules/Rule7")]
public class Rule7SO : RuleSO
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
        Debug.Log("Rule7SO - Inicio de validación.");

        // Obtener los NO utilizados en reglas 1 a 5
        List<SocketType> usedNOs = GetUsedNOs(connections); // NO1 a NO4 utilizados

        // Verificar que exactamente dos NO se hayan utilizado (conectados a T y N)
        if (usedNOs.Count != 2)
        {
            Debug.Log("Rule7SO - No se han utilizado exactamente dos NO en reglas 1 a 5. Regla 7 no cumplida.");
            return false;
        }

        // Obtener las parejas de los NO utilizados
        List<SocketType> pairedNOs = new List<SocketType>();
        foreach (var no in usedNOs)
        {
            pairedNOs.Add(GetPair(no));
        }
        Debug.Log($"Rule7SO - Parejas de NOs utilizados: {string.Join(", ", pairedNOs)}");

        // Identificar los MOs ya conectados por las parejas (Regla 6)
        HashSet<SocketType> connectedMOs = new HashSet<SocketType>();
        foreach (SocketType pairedNO in pairedNOs)
        {
            if (IsConnected(connections, pairedNO, SocketType.MO1))
            {
                connectedMOs.Add(SocketType.MO1);
            }
            if (IsConnected(connections, pairedNO, SocketType.MO2))
            {
                connectedMOs.Add(SocketType.MO2);
            }
        }

        Debug.Log($"Rule7SO - MOs ya conectados por Rule6: {string.Join(", ", connectedMOs)}");

        // Determinar qué MOs quedan disponibles
        List<SocketType> allMOs = new List<SocketType> { SocketType.MO1, SocketType.MO2 };
        List<SocketType> remainingMOs = allMOs.FindAll(mo => !connectedMOs.Contains(mo));

        Debug.Log($"Rule7SO - MOs restantes para conectar: {string.Join(", ", remainingMOs)}");

        // Verificar que haya exactamente un MO restante
        if (remainingMOs.Count != 1)
        {
            Debug.Log("Rule7SO - No hay exactamente un MO restante para conectar. Regla 7 no cumplida.");
            return false;
        }

        // Identificar la pareja restante que debe conectarse al MO restante
        SocketType remainingMO = remainingMOs[0];
        SocketType remainingPairNO = GetRemainingPair(pairedNOs);

        Debug.Log($"Rule7SO - Pareja restante para conectar: {remainingPairNO} a {remainingMO}");

        // Verificar si la pareja restante está conectada al MO restante
        bool isConnected = IsConnected(connections, remainingPairNO, remainingMO);
        Debug.Log($"Rule7SO - {remainingPairNO} {(isConnected ? "está" : "no está")} conectado a {remainingMO}.");

        if (isConnected)
        {
            Debug.Log("Rule7SO - Se cumple la Regla 7.");
            return true;
        }
        else
        {
            Debug.Log("Rule7SO - No se cumple la Regla 7.");
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
                    Debug.Log($"Rule7SO - NO {no} conectado a T.");
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
                    Debug.Log($"Rule7SO - NO {no} conectado a N.");
                }
            }
        }

        // Eliminar duplicados
        List<SocketType> result = new List<SocketType>(new HashSet<SocketType>(usedNOs));
        Debug.Log($"Rule7SO - NOs utilizados en reglas 1 a 5: {string.Join(", ", result)}");
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
            Debug.Log($"Rule7SO - La pareja de {no} es {pair}.");
            return pair;
        }
        else
        {
            Debug.LogError($"Rule7SO - No se encontró pareja para el socket: {no}");
            throw new System.Exception($"No se encontró pareja para el socket: {no}");
        }
    }

    /// <summary>
    /// Obtiene la pareja restante que no ha sido verificada.
    /// </summary>
    private SocketType GetRemainingPair(List<SocketType> pairedNOs)
    {
        foreach (var pair in noPairs)
        {
            if (!pairedNOs.Contains(pair.Key))
            {
                return pair.Key;
            }
        }
        Debug.LogError("Rule7SO - No se encontró una pareja restante.");
        throw new System.Exception("No se encontró una pareja restante.");
    }

    /// <summary>
    /// Verifica si un NO está conectado a un MO específico.
    /// </summary>
    private bool IsConnected(Dictionary<SocketType, List<SocketType>> connections, SocketType no, SocketType mo)
    {
        bool connected = connections.ContainsKey(no) && connections[no].Contains(mo);
        Debug.Log($"Rule7SO - {no} {(connected ? "está" : "no está")} conectado a {mo}.");
        return connected;
    }
}
