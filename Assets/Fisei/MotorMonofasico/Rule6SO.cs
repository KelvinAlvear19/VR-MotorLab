using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Rule6", menuName = "Rules/Rule6")]
public class Rule6SO : RuleSO
{
    public override bool IsValid(Dictionary<SocketType, List<SocketType>> connections)
    {
        // Obtener los NO utilizados en las reglas 3 y 4
        List<SocketType> usedNOs = CircuitValidator.Instance.GetUsedNOsByTAndN();

        // Obtener los pares de los NO utilizados
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

        Debug.Log("Rule6SO - Ningún par está conectado a MO1 o MO2. Regla no cumplida.");
        return false;
    }
}
