using UnityEngine;
using TMPro;

public class RuleUI : MonoBehaviour
{
    [Tooltip("El ScriptableObject que define la lógica de esta regla")]
    [SerializeField]
    public RuleSO ruleSO;

    [Tooltip("El elemento TextMeshProUGUI donde se muestra el estado de esta regla")]
    [SerializeField]
    public TextMeshProUGUI ruleText;

    /// <summary>
    /// Actualiza el texto de la regla en la UI.
    /// </summary>
    /// <param name="isValid">Estado de la regla (true = Cumplida, false = No Cumplida).</param>
    public void UpdateRuleUI(bool isValid)
    {
        if (ruleText != null && ruleSO != null)
        {
            ruleText.text = $"{(isValid ? "Cumplida" : "No Cumplida")}";
            ruleText.color = isValid ? Color.green : Color.red;
        }
        else
        {
            Debug.LogWarning("RuleUI - Faltan referencias en el componente.");
        }
    }
}
