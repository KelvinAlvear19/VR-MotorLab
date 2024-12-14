using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchToggleDepend : MonoBehaviour
{
    public GameObject cubeOn;  // Cubo en la posición "ON"
    public GameObject cubeOff; // Cubo en la posición "OFF"
    public AudioSource switchSound; // Sonido del interruptor
    public Light pointLight;  // Luz que se enciende/apaga
    public Light externalLight;  // Luz externa que habilita el interruptor

    private bool isOn = false; // Estado del interruptor

    // Este método se llamará cuando el Poke Interactor toque el interruptor
    public void OnPokeActivate(XRBaseInteractor interactor)
    {
        // Verificamos si la luz externa está activada
        if (externalLight != null && externalLight.enabled)
        {
            // Llamamos al método ToggleSwitch cuando el interactor hace "poke"
            ToggleSwitch();
        }
        else
        {
            Debug.Log("No se puede cambiar el estado del interruptor. La luz externa no está activada.");
        }
    }

    // Método que se ejecuta cuando se activa el interruptor
    public void ToggleSwitch()
    {
        // Solo cambiar el estado del interruptor si la luz externa está activada
        if (externalLight != null && externalLight.enabled)
        {
            // Cambiar el estado del interruptor
            isOn = !isOn;

            // Activar/desactivar cubos según el estado
            cubeOn.SetActive(isOn);
            cubeOff.SetActive(!isOn);

            // Reproducir sonido
            if (switchSound != null)
            {
                switchSound.Play();
            }

            // Activar/desactivar luz interna
            if (pointLight != null)
            {
                pointLight.enabled = isOn;
            }
        }
        else
        {
            // Si la luz externa no está activada, solo lo mostramos en el log
            Debug.Log("La luz externa debe estar activada para cambiar el estado del interruptor.");
        }
    }

    // Método adicional que permite apagar la luz interna si la luz externa se apaga
    public void CheckLightState()
    {
        // Si la luz externa está apagada, desactivamos la luz interna
        if (externalLight != null && !externalLight.enabled && pointLight != null)
        {
            pointLight.enabled = false;
            cubeOn.SetActive(false);
            cubeOff.SetActive(true);  // Asegúrate de que se muestra el cubo "OFF"
            isOn = false;  // Cambiamos el estado del interruptor a OFF
        }
    }
}
