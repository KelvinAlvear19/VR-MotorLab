using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(fileName = "Rule9TSO", menuName = "Rules/Rule9TSO")]
public class Rule9TSO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados por Cross R, Cross S, y T
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByCrossRandCrossSandT();

        // Obtener las parejas de esos NOs
        List<SocketType> pairs = CircuitValidator.Instance.GetPairsForUsedNOs(usedNOs);

        // Excluir las parejas utilizadas por U1 y V1
        SocketType? pairUsedByU1 = null;
        SocketType? pairUsedByV1 = null;

        if (connections.ContainsKey(SocketType.U1))
        {
            pairUsedByU1 = pairs.FirstOrDefault(pair => connections[SocketType.U1].Contains(pair));
        }

        if (connections.ContainsKey(SocketType.V1))
        {
            pairUsedByV1 = pairs.FirstOrDefault(pair => connections[SocketType.V1].Contains(pair));
        }

        if (connections.ContainsKey(SocketType.W1))
        {
            foreach (var pair in pairs)
            {
                if (pair == pairUsedByU1 || pair == pairUsedByV1)
                {
                    Debug.Log($"Rule9TSO - La pareja {pair} ya está ocupada por U1 o V1. No válida.");
                    continue;
                }

                // Verificar si la pareja está conectada a W1
                if (connections[SocketType.W1].Contains(pair))
                {
                    Debug.Log($"Rule9TSO - W1 conectado a {pair} (pareja válida de NOs usados) - Cumplida");
                    return true;
                }
            }
        }

        Debug.Log("Rule9TSO - No cumplida");
        return false;
    }
}
