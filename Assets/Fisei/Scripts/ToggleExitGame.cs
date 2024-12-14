using UnityEngine;
using UnityEngine.UI;

public class ToggleExitGame : MonoBehaviour
{
    // Este campo no es necesario si usas el Inspector para asignar el Toggle directamente
    public void OnToggleValueChanged(bool isOn)

    {
        Debug.Log("M�todo ejecutado. Valor del Toggle: " + isOn);
        // Si el Toggle est� activado (isOn == true), salimos del juego
        if (isOn)
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }
        else
        {
            Debug.Log("El Toggle est� desactivado.");
        }
    }
}
