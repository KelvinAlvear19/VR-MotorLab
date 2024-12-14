// UIManager.cs
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Textos de Reglas")]
    [SerializeField]
    private TextMeshProUGUI[] ruleTexts; // Asigna las referencias en el Inspector

    private void Start()
    {
        SetAllTextsColorRed();
    }

    /// <summary>
    /// Establece todos los textos de las reglas en color rojo y "No cumplida".
    /// </summary>
    public void SetAllTextsColorRed()
    {
        foreach (var ruleText in ruleTexts)
        {
            ruleText.color = Color.red;
            ruleText.text = "No cumplida";
        }
    }

    /// <summary>
    /// Actualiza los colores y textos de las reglas basándose en su estado de cumplimiento.
    /// </summary>
    /// <param name="rulesStatus">Arreglo de estados de las reglas. Cada elemento corresponde a una regla.</param>
    public void UpdateRuleColors(bool[] rulesStatus)
    {
        if (rulesStatus.Length != ruleTexts.Length)
        {
            Debug.LogError("El número de estados proporcionados no coincide con el número de reglas.");
            return;
        }

        for (int i = 0; i < ruleTexts.Length; i++)
        {
            ruleTexts[i].color = rulesStatus[i] ? Color.green : Color.red;
            ruleTexts[i].text = rulesStatus[i] ? "Cumplida" : "No cumplida";
        }
    }
}
