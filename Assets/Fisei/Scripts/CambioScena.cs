using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para acceder al Toggle

public class CambioScena : MonoBehaviour
{
    public int sceneIndex; // El índice de la escena en los Build Settings.

    // Este método será llamado cuando el Toggle cambie de valor
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // Cambia de escena cuando el Toggle esté activado
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
