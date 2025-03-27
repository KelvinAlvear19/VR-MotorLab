
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule2TSO", menuName = "Rules/Rule2TSO")]
public class Rule2TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.S))
        {
            if (connections[SocketType.S].Contains(SocketType.A1) || connections[SocketType.S].Contains(SocketType.A2))
            {
                Debug.Log("Rule2TSO - S conectado a A1 o A2 - Cumplida");
                return true;
            }
        }

        Debug.Log("Rule2TSO - No cumplida");
        return false;
    }
}