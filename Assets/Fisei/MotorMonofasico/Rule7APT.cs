using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule7APT", menuName = "Rules/Rule7APT")]
public class Rule7APT : RuleSO
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
