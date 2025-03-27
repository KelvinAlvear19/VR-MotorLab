using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP2", menuName = "Rules/RuleEP2")]
public class RuleEP2SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.S))
        {
            if (connections[SocketType.S].Contains(SocketType.A1) || connections[SocketType.S].Contains(SocketType.A2))
            {
                Debug.Log("RuleEP2 - S conectado a A1 o A2 - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP2 - No cumplida");
        return false;
    }
}
