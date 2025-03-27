using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule1APT", menuName = "Rules/Rule1APT")]
public class Rule1APT : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.R))
        {
            if (connections[SocketType.R].Contains(SocketType.P1R) || connections[SocketType.R].Contains(SocketType.P2R))
            {
                Debug.Log("RuleEP1 - R conectado a P1R o P2R - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP1 - No cumplida");
        return false;
    }
}