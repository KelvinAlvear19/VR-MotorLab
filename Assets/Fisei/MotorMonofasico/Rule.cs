// Rule.cs
using UnityEngine;

[System.Serializable]
public class Rule
{
    [Tooltip("Descripci�n de la regla.")]
    public string RuleDescription;

    [Tooltip("Referencia a la funci�n que eval�a la regla.")]
    public System.Func<string, CircuitValidator, bool> EvaluateFunction;
}
