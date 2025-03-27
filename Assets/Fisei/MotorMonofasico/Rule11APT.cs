using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule11APT", menuName = "Rules/Rule11APT")]
public class Rule11APT : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados por Cross R, Cross S, y T
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByCrossRandCrossSandT();

        // Obtener las parejas de esos NOs
        List<SocketType> pairs = CircuitValidator.Instance.GetPairsForUsedNOs(usedNOs);

        // Excluir la pareja utilizada por U1
        SocketType? pairUsedByU1 = null;
        if (connections.ContainsKey(SocketType.U1))
        {
            pairUsedByU1 = pairs.FirstOrDefault(pair => connections[SocketType.U1].Contains(pair));
        }

        if (connections.ContainsKey(SocketType.V1))
        {
            foreach (var pair in pairs)
            {
                if (pair == pairUsedByU1)
                {
                    Debug.Log($"Rule8TSO - La pareja {pair} ya está ocupada por U1. No válida.");
                    continue;
                }

                // Verificar si la pareja está conectada a V1
                if (connections[SocketType.V1].Contains(pair))
                {
                    Debug.Log($"Rule8TSO - V1 conectado a {pair} (pareja válida de NOs usados) - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule8TSO - No cumplida");
        return false;
    }
}
