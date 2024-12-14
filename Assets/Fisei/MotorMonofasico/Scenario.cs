// Scenario.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "Scenarios/Scenario")]
public class Scenario : ScriptableObject
{
    [Tooltip("Nombre del escenario, por ejemplo, 'Scenario1'")]
    public string scenarioName;

    [Tooltip("Lista de Identificadores de Dispositivos involucrados en este escenario, por ejemplo, 'Fuente', 'Pulsador', etc.")]
    public List<string> deviceIDs;

    [Tooltip("Lista de reglas aplicables a este escenario")]
    public List<Rule> rules;
}
