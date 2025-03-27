using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule4", menuName = "Rules/Rule4")]
public class Rule4SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        SocketType[] validNOs = { SocketType.NO1, SocketType.NO3, SocketType.NO4, SocketType.NO2 };
        foreach (var no in validNOs)
        {
            if (connections.ContainsKey(SocketType.N) && connections[SocketType.N].Contains(no))
            {
                if (connections.ContainsKey(SocketType.T) && connections[SocketType.T].Contains(no))
                {
                    Debug.Log($"Rule4SO - N conectado a NO {no}, pero NO ya est� conectado a T. Regla no cumplida.");
                    return false;
                }

                Debug.Log($"Rule4SO - N conectado a NO {no}. Regla cumplida.");
                return true;
            }
        }

        Debug.Log("Rule4SO - N no est� conectado a ning�n NO v�lido. Regla no cumplida.");
        return false;
    }
}
