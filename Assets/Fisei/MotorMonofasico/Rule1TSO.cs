using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule1TSO", menuName = "Rules/Rule1TSO")]
public class Rule1TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.R))
        {
            if (connections[SocketType.R].Contains(SocketType.P1) || connections[SocketType.R].Contains(SocketType.P2))
            {
                Debug.Log("Rule1TSO - R conectado a P1 o P2 - Cumplida");
                return true;
            }
        }

        Debug.Log("Rule1TSO - No cumplida");
        return false;
    }
}