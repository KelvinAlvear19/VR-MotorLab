// Rule4SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule4", menuName = "Rules/Rule4")]
public class Rule4SO : RuleSO
{
    private SocketType[] allValidNOs = { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 };

    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (!connections.ContainsKey(SocketType.N))
        {
            Debug.Log("Rule4SO - N no está conectado a ningún NO - No cumplida");
            return false;
        }

        bool isValid = true;

        foreach (var no in connections[SocketType.N])
        {
            // Verificar si N está conectado a un NO válido
            if (!System.Array.Exists(allValidNOs, element => element == no))
            {
                Debug.Log($"Rule4SO - N está conectado a {no}, que no es un NO válido - No cumplida");
                isValid = false;
                continue;
            }

            // Verificar si N está conectado al mismo NO que T
            if (connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(no))
            {
                Debug.Log($"Rule4SO - N está conectado al mismo NO que T ({no}) - No cumplida");
                isValid = false;
                continue;
            }

            Debug.Log($"Rule4SO - N está conectado a {no} - Cumplida");
        }

        if (isValid)
        {
            Debug.Log("Rule4SO - Todas las conexiones de N son válidas - Cumplida");
        }
        else
        {
            Debug.Log("Rule4SO - Algunas conexiones de N no son válidas - No cumplida");
        }

        return isValid;
    }
}
