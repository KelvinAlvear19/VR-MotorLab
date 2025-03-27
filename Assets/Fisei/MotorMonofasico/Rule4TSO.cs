using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule4TSO", menuName = "Rules/Rule4TSO")]
public class Rule4TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.CrossR))
        {
            foreach (var no in new[] { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 })
            {
                if (connections[SocketType.CrossR].Contains(no))
                {
                    Debug.Log($"Rule4TSO - Cross R conectado a {no} - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule4TSO - No cumplida");
        return false;
    }
}
