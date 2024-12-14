// Rule1SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule1", menuName = "Rules/Rule1")]
public class Rule1SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        bool isConnectedP1 = connections.ContainsKey(SocketType.R) && connections[SocketType.R].Contains(SocketType.P1);
        bool isConnectedP2 = connections.ContainsKey(SocketType.R) && connections[SocketType.R].Contains(SocketType.P2);

        bool result = isConnectedP1 || isConnectedP2;

        Debug.Log($"Rule1SO - R conectado a P1: {isConnectedP1}, P2: {isConnectedP2} - {(result ? "Cumplida" : "No cumplida")}");

        return result;
    }
}
