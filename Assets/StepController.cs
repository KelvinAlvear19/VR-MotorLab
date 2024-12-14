using UnityEngine;
using System.Collections;

public class StepController : MonoBehaviour
{
    public GameObject[] steps; // Arreglo de Canvas para cada paso
    private int currentStep = 0; // Paso actual
    public float stepDuration = 3f; // Duración en segundos para cada paso

    private void Start()
    {
        // Iniciar la corutina para mostrar cada paso con el tiempo de espera
        StartCoroutine(ShowStepsWithDelay());
    }

    private IEnumerator ShowStepsWithDelay()
    {
        while (currentStep < steps.Length)
        {
            // Activar el Canvas actual
            steps[currentStep].SetActive(true);

            // Esperar el tiempo especificado
            yield return new WaitForSeconds(stepDuration);

            // Desactivar el Canvas actual
            steps[currentStep].SetActive(false);

            // Avanzar al siguiente paso
            currentStep++;
        }

        // Cuando todos los pasos se hayan completado
        Debug.Log("Todos los pasos completados.");
    }
}
