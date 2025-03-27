using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP9", menuName = "Rules/RuleEP9")]
public class RuleEP9SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        foreach (SocketType mo in new[] { SocketType.MO1, SocketType.MO2 })
        {
            if (connections.ContainsKey(mo) && connections[mo].Contains(SocketType.N))
            {
                Debug.Log($"RuleEP9 - MO {mo} conectado a N - Cumplida");
                return true;
            }
        }

        Debug.Log("RuleEP9 - No cumplida");
        return false;
    }
}
