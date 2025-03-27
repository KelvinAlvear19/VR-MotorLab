using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule7TSO", menuName = "Rules/Rule7TSO")]
public class Rule7TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados por Cross R, Cross S, y T
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByCrossRandCrossSandT();

        // Obtener las parejas de esos NOs
        List<SocketType> pairs = CircuitValidator.Instance.GetPairsForUsedNOs(usedNOs);

        if (connections.ContainsKey(SocketType.U1))
        {
            foreach (var pair in pairs)
            {
                // Verificar si la pareja está conectada a U1
                if (connections[SocketType.U1].Contains(pair))
                {
                    Debug.Log($"Rule7TSO - U1 conectado a {pair} (pareja válida de NOs usados) - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule7TSO - No cumplida");
        return false;
    }
}
