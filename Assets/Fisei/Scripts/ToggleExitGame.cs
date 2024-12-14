using UnityEngine;
using UnityEngine.UI;

public class ToggleExitGame : MonoBehaviour
{
    // Este campo no es necesario si usas el Inspector para asignar el Toggle directamente
    public void OnToggleValueChanged(bool isOn)

    {
        Debug.Log("Método ejecutado. Valor del Toggle: " + isOn);
        // Si el Toggle está activado (isOn == true), salimos del juego
        if (isOn)
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }
        else
        {
            Debug.Log("El Toggle está desactivado.");
        }
    }
}
