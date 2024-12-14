using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwitchToggle : MonoBehaviour
{
    public GameObject cubeOn;  // Cubo en la posición "ON"
    public GameObject cubeOff; // Cubo en la posición "OFF"
    public AudioSource switchSound; // Sonido del interruptor
    public Light pointLight;  // Luz que se enciende/apaga

    private bool isOn = false; // Estado del interruptor

    // Este método se llamará cuando el Poke Interactor toque el interruptor
    public void OnPokeActivate(XRBaseInteractor interactor)
    {
        // Llamamos al método ToggleSwitch cuando el interactor hace "poke"
        ToggleSwitch();
    }

    // Método que se ejecuta cuando se activa el interruptor
    public void ToggleSwitch()
    {
        isOn = !isOn;

        // Activar/desactivar cubos según el estado
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
