using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP5", menuName = "Rules/RuleEP5")]
public class RuleEP5SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Verificar si cualquier P est� conectado a NO1A
        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (connections.ContainsKey(p) && connections[p].Contains(SocketType.NO1A))
            {
                Debug.Log($"RuleEP5 - Pulsador {p} conectado a NO1A - Cumplida");
                return true;
            }
        }

        // Verificar si cualquier P est� conectado a NO2A
        foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
        {
            if (connections.ContainsKey(p) && connections[p].Contains(SocketType.NO2A))
            {
                Debug.Log($"RuleEP5 - Pulsador {p} conectado a NO2A - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP5 - Ning�n pulsador conectado a NO1A o NO2A - No cumplida");
        return false;
    }
}
