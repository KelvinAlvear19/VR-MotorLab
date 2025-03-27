using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule2", menuName = "Rules/Rule2")]
public class Rule2SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        bool isConnectedA1 = connections.ContainsKey(SocketType.S) && connections[SocketType.S].Contains(SocketType.A1);
        bool isConnectedA2 = connections.ContainsKey(SocketType.S) && connections[SocketType.S].Contains(SocketType.A2);

        bool result = isConnectedA1 || isConnectedA2;

        Debug.Log($"Rule2SO - S conectado a A1: {isConnectedA1}, A2: {isConnectedA2} - {(result ? "Cumplida" : "No cumplida")}");

        return result;
    }
}
