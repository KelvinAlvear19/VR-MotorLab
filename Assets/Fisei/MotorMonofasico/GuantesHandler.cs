using UnityEngine;

public class GuantesHandler : MonoBehaviour
{
    public Material guantesMaterial; // Material que representa el color de los guantes
    public SkinnedMeshRenderer[] handRenderers; // Renderers de las VR Hands (manos virtuales)
    public UIController uiController; // Referencia al controlador de la UI

    public void OnGuantesPicked()
    {
        foreach (var renderer in handRenderers)
        {
            Material[] materials = renderer.materials; // Obtén todos los materiales del renderer

            if (materials.Length > 1) // Verifica que haya al menos dos elementos
            {
                Material materialToChange = materials[1]; // Obtén el material del elemento 1
                if (materialToChange.HasProperty("_Color")) // Verifica si tiene la propiedad _Color
                {
                    materialToChange.color = guantesMaterial.color; // Cambia el color al de los guantes
                    Debug.Log("Color cambiado en el material del elemento 1.");
                }
                else
                {
                    Debug.LogWarning($"El material {materialToChange.name} no tiene la propiedad '_Color'.");
                }
            }
            else
            {
                Debug.LogWarning("El SkinnedMeshRenderer no tiene suficientes materiales.");
            }

            // Reasigna los materiales actualizados al renderer
            renderer.materials = materials;
        }

        // Actualizar la UI
        uiController.ActivateCheckmark("guantes");

        // Desactivar el objeto de los guantes
        gameObject.SetActive(false);

        Debug.Log("Guantes recogidos, color del material cambiado y objeto desactivado.");
    }
}
