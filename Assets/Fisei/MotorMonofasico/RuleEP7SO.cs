using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RuleEP7", menuName = "Rules/RuleEP7")]
public class RuleEP7SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Verificar si CrossR está conectado a NO1, NO2, NO3 o NO4
        bool isConnectedNO1 = connections.ContainsKey(SocketType.CrossR) && connections[SocketType.CrossR].Contains(SocketType.NO1);
        bool isConnectedNO2 = connections.ContainsKey(SocketType.CrossR) && connections[SocketType.CrossR].Contains(SocketType.NO2);
        bool isConnectedNO3 = connections.ContainsKey(SocketType.CrossR) && connections[SocketType.CrossR].Contains(SocketType.NO3);
        bool isConnectedNO4 = connections.ContainsKey(SocketType.CrossR) && connections[SocketType.CrossR].Contains(SocketType.NO4);

        bool result = isConnectedNO1 || isConnectedNO2 || isConnectedNO3 || isConnectedNO4;

        Debug.Log($"RuleEP7SO - CrossR conectado a NO1: {isConnectedNO1}, NO2: {isConnectedNO2}, NO3: {isConnectedNO3}, NO4: {isConnectedNO4} - {(result ? "Cumplida" : "No cumplida")}");

        return result;
    }
}
