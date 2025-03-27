using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule3APT", menuName = "Rules/Rule3APT")]
public class Rule3APT : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        SocketType? remainingP1RorP2R = CircuitValidator.Instance.GetRemainingP1RorP2R();
        if (remainingP1RorP2R.HasValue)
        {
            foreach (var p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
            {
                if (connections.ContainsKey(remainingP1RorP2R.Value) && connections[remainingP1RorP2R.Value].Contains(p))
                {
                    Debug.Log($"RuleEP3 - Pulsador restante {remainingP1RorP2R} conectado a {p} - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("RuleEP3 - No cumplida");
        return false;
    }
}
