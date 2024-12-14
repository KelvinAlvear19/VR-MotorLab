using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchToggle : MonoBehaviour
{
    public GameObject cubeOn;  // Cubo en la posici�n "ON"
    public GameObject cubeOff; // Cubo en la posici�n "OFF"
    public AudioSource switchSound; // Sonido del interruptor
    public Light pointLight;  // Luz que se enciende/apaga

    private bool isOn = false; // Estado del interruptor

    // Este m�todo se llamar� cuando el Poke Interactor toque el interruptor
    public void OnPokeActivate(XRBaseInteractor interactor)
    {
        // Llamamos al m�todo ToggleSwitch cuando el interactor hace "poke"
        ToggleSwitch();
    }

    // M�todo que se ejecuta cuando se activa el interruptor
    public void ToggleSwitch()
    {
        isOn = !isOn;

        // Activar/desactivar cubos seg�n el estado
        cubeOn.SetActive(isOn);
        cubeOff.SetActive(!isOn);

        // Reproducir sonido
        if (switchSound != null)
        {
            switchSound.Play();
        }

        // Activar/desactivar luz
        if (pointLight != null)
        {
            pointLight.enabled = isOn;
        }
    }
}
