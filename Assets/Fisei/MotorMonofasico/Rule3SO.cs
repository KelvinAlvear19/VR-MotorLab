// Rule3SO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule3", menuName = "Rules/Rule3")]
public class Rule3SO : RuleSO
{
    private SocketType[] validNOs = { SocketType.NO1, SocketType.NO2, SocketType.NO3, SocketType.NO4 };

    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        if (connections.ContainsKey(SocketType.T))
        {
            foreach (var no in connections[SocketType.T])
            {
                if (System.Array.Exists(validNOs, element => element == no))
                {
                    Debug.Log($"Rule3SO - T est� conectado a {no} - Cumplida");
                    return true;
                }
            }
        }
        Debug.Log("Rule3SO - T no est� conectado a ning�n NO v�lido - No cumplida");
        return false;
    }
}
