using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule3", menuName = "Rules/Rule3")]
public class Rule3SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        bool isConnectedNO1 = connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(SocketType.NO1);
        bool isConnectedNO2 = connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(SocketType.NO2);
        bool isConnectedNO3 = connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(SocketType.NO3);
        bool isConnectedNO4 = connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(SocketType.NO4);

        bool result = isConnectedNO1 || isConnectedNO2 || isConnectedNO3 || isConnectedNO4;

        Debug.Log($"Rule3SO - T conectado a NO1: {isConnectedNO1}, NO2: {isConnectedNO2}, NO3: {isConnectedNO3}, NO4: {isConnectedNO4} - {(result ? "Cumplida" : "No cumplida")}");

        return result;
    }
}
