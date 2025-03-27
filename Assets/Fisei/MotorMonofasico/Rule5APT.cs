using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule5APT", menuName = "Rules/Rule5APT")]
public class Rule5APT : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Verificar si cualquier P está conectado a NO1A
        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (connections.ContainsKey(p) && connections[p].Contains(SocketType.NO1A))
            {
                Debug.Log($"RuleEP5 - Pulsador {p} conectado a NO1A - Cumplida");
                return true;
            }
        }

        // Verificar si cualquier P está conectado a NO2A
        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (connections.ContainsKey(p) && connections[p].Contains(SocketType.NO2A))
            {
                Debug.Log($"RuleEP5 - Pulsador {p} conectado a NO2A - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP5 - Ningún pulsador conectado a NO1A o NO2A - No cumplida");
        return false;
    }
}