using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleEP4", menuName = "Rules/RuleEP4")]
public class RuleEP4SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Identificar las bobinas utilizadas
        List<SocketType> usedA = new List<SocketType>();
        foreach (SocketType a in new[] { SocketType.A1, SocketType.A2 })
        {
            if (connections.ContainsKey(SocketType.S) && connections[SocketType.S].Contains(a))
            {
                usedA.Add(a);
            }
        }

        // Determinar la bobina restante
        SocketType? remainingA = null;
        foreach (SocketType a in new[] { SocketType.A1, SocketType.A2 })
        {
            if (!usedA.Contains(a))
            {
                remainingA = a;
                break;
            }
        }

        if (!remainingA.HasValue)
        {
            Debug.Log("RuleEP4 - No hay bobinas restantes disponibles. No cumplida");
            return false;
        }

        // Verificar las conexiones de los pulsadores y sus parejas
        foreach (SocketType pR in new[] { SocketType.P1R, SocketType.P2R })
        {
            foreach (SocketType p in new[] { SocketType.P1, SocketType.P2, SocketType.P3, SocketType.P4 })
            {
                if (connections.ContainsKey(pR) && connections[pR].Contains(p))
                {
                    // Determinar la pareja del pulsador conectado
                    SocketType pairP = CircuitValidator.Instance.GetPairP(p);

                    // Verificar que la pareja del pulsador se conecta a la bobina restante
                    if (connections.ContainsKey(pairP) && connections[pairP].Contains(remainingA.Value))
                    {
                        Debug.Log($"RuleEP4 - Pulsador {pairP} conectado a bobina restante {remainingA.Value} - Cumplida");
                        return true;
                    }
                }
            }
        }

        Debug.Log("RuleEP4 - No cumplida");
        return false;
    }
}
