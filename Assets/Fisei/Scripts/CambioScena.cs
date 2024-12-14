using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para acceder al Toggle

public class CambioScena : MonoBehaviour
{
    public int sceneIndex; // El �ndice de la escena en los Build Settings.

    // Este m�todo ser� llamado cuando el Toggle cambie de valor
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // Cambia de escena cuando el Toggle est� activado
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
