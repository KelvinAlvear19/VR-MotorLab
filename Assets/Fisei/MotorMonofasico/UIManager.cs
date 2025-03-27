using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<RuleUI> ruleUIs; // Lista de todos los RuleUI asignados en el Inspector

    /// <summary>
    /// Actualiza los textos de las reglas en la UI.
    /// </summary>
    public void UpdateRuleTexts(List<bool> ruleStatuses)
    {
        if (ruleUIs.Count != ruleStatuses.Count)
        {
            Debug.LogError("UIManager - La cantidad de RuleUIs no coincide con los estados de las reglas.");
            return;
        }

        for (int i = 0; i < ruleUIs.Count; i++)
        {
            RuleUI ruleUI = ruleUIs[i];
            bool isValid = ruleStatuses[i];

            if (ruleUI.ruleText != null)
            {
                ruleUI.ruleText.text = $"{(isValid ? "Cumplida" : "No Cumplida")}";
                ruleUI.ruleText.color = isValid ? Color.green : Color.red;
            }
        }
    }
}
