// Rule.cs
using UnityEngine;

[System.Serializable]
public class Rule
{
    [Tooltip("Descripción de la regla.")]
    public string RuleDescription;

    [Tooltip("Referencia a la función que evalúa la regla.")]
    public System.Func<string, CircuitValidator, bool> EvaluateFunction;
}
