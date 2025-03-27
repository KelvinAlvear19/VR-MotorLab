using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Lista de nombres de escenas
    public string[] sceneNames;

    // Método para cambiar de escena
    public void SwitchScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[sceneIndex]);
        }
        else
        {
            Debug.LogError("El índice de la escena está fuera de rango.");
        }
    }
}
