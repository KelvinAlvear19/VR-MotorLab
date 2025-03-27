using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Lista de nombres de escenas
    public string[] sceneNames;

    // M�todo para cambiar de escena
    public void SwitchScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[sceneIndex]);
        }
        else
        {
            Debug.LogError("El �ndice de la escena est� fuera de rango.");
        }
    }
}
