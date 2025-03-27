using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewRule", menuName = "Rules/Rule")]
public abstract class RuleSO : ScriptableObject
{
    [Tooltip("Nombre de la regla para identificarla")]
    public string RuleName;

    [Tooltip("Identificador del grupo al que pertenece esta regla")]
    public string GroupId;

    [TextArea]
    public string description;

    /// <summary>
    /// Valida la regla basada en las conexiones actuales.
    /// </summary>
    public abstract bool IsValid(Dictionary<SocketType, List<SocketType>> connections);
}
