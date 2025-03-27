using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule6TSO", menuName = "Rules/Rule6TSO")]
public class Rule6TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        SocketType? usedNOByCrossR = CircuitValidator.Instance.GetUsedNO(SocketType.CrossR);
        SocketType? usedNOByCrossS = CircuitValidator.Instance.GetUsedNO(SocketType.CrossS);

        if (usedNOByCrossR.HasValue && usedNOByCrossS.HasValue && connections.ContainsKey(SocketType.T))
        {
            foreach (var no in new[] { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 })
            {
                if (no != usedNOByCrossR.Value && no != usedNOByCrossS.Value && connections[SocketType.T].Contains(no))
                {
                    Debug.Log($"Rule6TSO - T conectado a {no}, diferente de {usedNOByCrossR.Value} y {usedNOByCrossS.Value} - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule6TSO - No cumplida");
        return false;
    }
}