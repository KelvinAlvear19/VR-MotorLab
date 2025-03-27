using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP1", menuName = "Rules/RuleEP1")]
public class RuleEP1SO : RuleSO
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
