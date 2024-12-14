// RuleSO.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRule", menuName = "Rules/Rule")]
public abstract class RuleSO : ScriptableObject
{
    [TextArea]
    public string description;

    /// <summary>
    /// Valida la regla basada en las conexiones actuales.
    /// </summary>
    /// <param name="connections">Diccionario de conexiones actuales.</param>
    /// <returns>True si la regla está cumplida, de lo contrario, false.</returns>
    public abstract bool IsValid(Dictionary<SocketType, List<SocketType>> connections);
}
