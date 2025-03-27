using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Rule7", menuName = "Rules/Rule7")]
public class Rule7SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados en las reglas 3 y 4
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByTAndN();

        // Obtener los pares de los NO utilizados
        List<SocketType> pairs = CircuitValidator.Instance.GetPairsForUsedNOs(usedNOs);

        // Verificar cuál MO ya está utilizado
        bool isMO1Used = connections.Values.Any(list => list.Contains(SocketType.MO1));
        bool isMO2Used = connections.Values.Any(list => list.Contains(SocketType.MO2));

        // Determinar el MO restante
        SocketType remainingMO = isMO1Used ? SocketType.MO2 : SocketType.MO1;

        // Validar si el par restante está conectado al MO restante
        foreach (var pair in pairs)
        {
            if (connections.ContainsKey(pair) && connections[pair].Contains(remainingMO))
            {
                Debug.Log($"Rule7SO - {pair} está conectado al MO restante ({remainingMO}). Regla cumplida.");
                return true;
            }
        }

        Debug.Log($"Rule7SO - Ningún par restante está conectado al MO restante ({remainingMO}). Regla no cumplida.");
        return false;
    }
}
