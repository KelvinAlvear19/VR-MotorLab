using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule5TSO", menuName = "Rules/Rule5TSO")]
public class Rule5TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        SocketType? usedNOByCrossR = CircuitValidator.Instance.GetUsedNO(SocketType.CrossR);

        if (usedNOByCrossR.HasValue && connections.ContainsKey(SocketType.CrossS))
        {
            foreach (var no in new[] { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 })
            {
                if (no != usedNOByCrossR.Value && connections[SocketType.CrossS].Contains(no))
                {
                    Debug.Log($"Rule5TSO - Cross S conectado a {no}, diferente de {usedNOByCrossR.Value} - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule5TSO - No cumplida");
        return false;
    }
}