using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas
using UnityEngine.UI; // Necesario para trabajar con UI (Toggle)

public class SceneResetManager : MonoBehaviour
{
    // Este método será llamado cuando el Toggle cambie su estado
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // Reinicia la escena actual
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // Carga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
