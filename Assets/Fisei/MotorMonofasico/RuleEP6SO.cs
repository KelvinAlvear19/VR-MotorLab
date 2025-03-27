using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP6", menuName = "Rules/RuleEP6")]
public class RuleEP6SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Verificar que Regla 5 esté cumplida con una conexión específica
        bool ruleEP5Completed = false;
        SocketType? connectedPtoNOA = null;

        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (connections.ContainsKey(p) &&
                (connections[p].Contains(SocketType.NO1A) || connections[p].Contains(SocketType.NO2A)))
            {
                ruleEP5Completed = true;
                connectedPtoNOA = p; // Guardar el pulsador conectado a NOA
                break;
            }
        }

        if (!ruleEP5Completed)
        {
            Debug.Log("RuleEP6 - Regla 5 no cumplida. Regla 6 no puede cumplirse.");
            return false;
        }

        // Verificar conexión específica para Regla 6
        SocketType remainingNOA = connectedPtoNOA.HasValue && connections[connectedPtoNOA.Value].Contains(SocketType.NO1A)
            ? SocketType.NO2A
            : SocketType.NO1A;

        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (p != connectedPtoNOA && connections.ContainsKey(p) && connections[p].Contains(remainingNOA))
            {
                Debug.Log($"RuleEP6 - Pulsador {p} conectado al NOA restante ({remainingNOA}) - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP6 - No se encontró conexión válida para el NOA restante. Regla no cumplida.");
        return false;
    }
}
