using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule5", menuName = "Rules/Rule5")]
public class Rule5SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        SocketType? usedP = null;
        SocketType? usedA = null;

        if (connections.ContainsKey(SocketType.R))
        {
            usedP = connections[SocketType.R].Find(p => p == SocketType.P1 || p == SocketType.P2);
        }

        if (connections.ContainsKey(SocketType.S))
        {
            usedA = connections[SocketType.S].Find(a => a == SocketType.A1 || a == SocketType.A2);
        }

        if (!usedP.HasValue || !usedA.HasValue)
        {
            Debug.Log("Rule5SO - Faltan conexiones básicas de R y S. Regla no cumplida.");
            return false;
        }

        SocketType freeP = (usedP == SocketType.P1) ? SocketType.P2 : SocketType.P1;
        SocketType freeA = (usedA == SocketType.A1) ? SocketType.A2 : SocketType.A1;

        bool result = connections.ContainsKey(freeA) && connections[freeA].Contains(freeP);

        Debug.Log($"Rule5SO - A libre ({freeA}) conectado a P libre ({freeP}): {(result ? "Cumplida" : "No cumplida")}");

        return result;
    }
}
