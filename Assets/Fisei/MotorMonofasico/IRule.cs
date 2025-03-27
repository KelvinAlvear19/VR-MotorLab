using System.Collections.Generic;

public interface IRule
{
    /// <summary>
    /// Nombre de la regla.
    /// </summary>
    string RuleName { get; }

    /// <summary>
    /// Valida la regla basada en las conexiones actuales.
    /// </summary>
    /// <param name="connections">Diccionario de conexiones actuales.</param>
    /// <returns>True si la regla está cumplida, de lo contrario, false.</returns>
    bool IsValid(Dictionary<SocketType, List<SocketType>> connections);
}
