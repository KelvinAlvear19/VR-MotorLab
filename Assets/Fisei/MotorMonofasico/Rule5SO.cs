// Rule5SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule5", menuName = "Rules/Rule5")]
public class Rule5SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Saber cuál P usó R
        SocketType? usedP = null;
        if (connections.ContainsKey(SocketType.R))
        {
            if (connections[SocketType.R].Contains(SocketType.P1))
                usedP = SocketType.P1;
            else if (connections[SocketType.R].Contains(SocketType.P2))
                usedP = SocketType.P2;
        }

        // Saber cuál A usó S
        SocketType? usedA = null;
        if (connections.ContainsKey(SocketType.S))
        {
            if (connections[SocketType.S].Contains(SocketType.A1))
                usedA = SocketType.A1;
            else if (connections[SocketType.S].Contains(SocketType.A2))
                usedA = SocketType.A2;
        }

        if (!usedP.HasValue || !usedA.HasValue)
        {
            Debug.Log("Rule5SO - R o S no están conectados correctamente - No cumplida");
            return false;
        }

        // El libre es el otro:
        SocketType freeP = (usedP.Value == SocketType.P1) ? SocketType.P2 : SocketType.P1;
        SocketType freeA = (usedA.Value == SocketType.A1) ? SocketType.A2 : SocketType.A1;

        // Checkear si el libre A está conectado con el libre P
        bool isConnected = connections.ContainsKey(freeA) && connections[freeA].Contains(freeP);
        Debug.Log($"Rule5SO - Libre A: {freeA}, Libre P: {freeP} - {(isConnected ? "Cumplida" : "No cumplida")}");

        return isConnected;
    }
}
