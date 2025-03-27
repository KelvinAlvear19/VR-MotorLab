using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule8", menuName = "Rules/Rule8")]
public class Rule8SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados por Rule7
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByCrossR();
        List<SocketType> pairs = CircuitValidator.Instance.GetPairsForUsedNOs(usedNOs);

        // Validar si al menos un par está conectado a MO1 o MO2
        foreach (var pair in pairs)
        {
            if (connections.ContainsKey(pair) &&
                (connections[pair].Contains(SocketType.MO1) || connections[pair].Contains(SocketType.MO2)))
            {
                Debug.Log($"Rule6SO - {pair} está conectado a MO1 o MO2. Regla cumplida.");
                return true;
            }
        }

        Debug.Log("Rule7SO - Ningún par restante está conectado al MO restante. Regla no cumplida.");
        return false;
    }
}
