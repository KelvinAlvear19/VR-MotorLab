using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialCanvases;  // Array de Canvas para cada paso
    private int currentStep = 0;           // Paso actual

    void Start()
    {
        // Mostrar el primer Canvas (primer paso)
        ShowCurrentStep();
    }

    // Método para avanzar al siguiente paso
    public void NextStep()
    {
        if (currentStep < tutorialCanvases.Length - 1)
        {
            // Desactivar el Canvas del paso actual
            tutorialCanvases[currentStep].SetActive(false);

            // Avanzar al siguiente paso
            currentStep++;

            // Mostrar el Canvas del siguiente paso
            ShowCurrentStep();
        }
        else
        {
            // Finalizar el tutorial
            Debug.Log("Tutorial completado.");
            tutorialCanvases[currentStep].SetActive(false); // Desactivar el último Canvas
        }
    }

    // Mostrar el Canvas correspondiente al paso actual
    private void ShowCurrentStep()
    {
        if (tutorialCanvases.Length > currentStep)
        {
            tutorialCanvases[currentStep].SetActive(true);
        }
    }
}
