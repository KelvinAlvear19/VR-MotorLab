// RuleUI.cs
using UnityEngine;
using TMPro;

public class RuleUI : MonoBehaviour
{
    [Tooltip("Referencia al TextMeshProUGUI que mostrará la descripción y estado de la regla.")]
    public TextMeshProUGUI ruleText;

    /// <summary>
    /// Asigna la descripción y el estado de la regla en la UI.
    /// </summary>
    /// <param name="description">Descripción de la regla.</param>
    /// <param name="isValid">Estado de la regla.</param>
    public void SetRule(string description, bool isValid)
    {
        if (ruleText != null)
        {
            ruleText.text = $"{description} - {(isValid ? "Cumplida" : "No cumplida")}";
            ruleText.color = isValid ? Color.green : Color.red;
        }
        else
        {
            Debug.LogWarning("RuleUI - ruleText no está asignado.");
        }
    }
}
