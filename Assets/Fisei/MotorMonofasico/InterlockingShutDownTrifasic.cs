using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InterlockingShutDownTrifasic : MonoBehaviour
{
    [Header("Configuraciones")]
    [SerializeField] private Light targetLight; // Luz objetivo que indica si se puede interactuar
    [SerializeField] private AudioSource buttonAudioSourceDefault;
    [SerializeField] private AudioSource buttonAudioSourceConexion;

    [SerializeField] private XRGrabInteractable parentGrabInteractable; // Objeto padre con XRGrabInteractable

    [Header("Esferas")]
    [SerializeField] private GameObject sphereOnPress; // Esfera activada al presionar el botón
    [SerializeField] private GameObject sphereOnRelease; // Esfera activada al soltar el botón

    [Header("Circuit Validator")]
    [SerializeField] private CircuitValidator circuitValidator; // Referencia al CircuitValidator
    [SerializeField] private string groupIdForTwelveRules = "DoceReglas"; // GroupId para el grupo de 9 reglas

    private bool isConnectionActive = false; // Estado de la conexión

    /// <summary>
    /// Método asignado al botón verde: Verifica reglas y activa la conexión.
    /// </summary>
    public void SelectEnteredGreen()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("Botón verde: Luz no activa. No se puede interactuar.");
            return;
        }

        // Verificar si las reglas se han cumplido
        bool reglasCumplidas = circuitValidator.AreGroupRulesMet(groupIdForTwelveRules);

        if (reglasCumplidas)
        {
            Debug.Log("Botón verde: Reglas cumplidas. Activando conexión.");
            isConnectionActive = true;

            PlaySound(buttonAudioSourceDefault); // Reproduce sonido por defecto
            ActivateSphere(true); // Activa esfera "presionado"

            // Retraso para reproducir el sonido continuo
            Invoke(nameof(PlayConnectionSound), 1.0f);
        }
        else
        {
            Debug.Log("Botón verde: Reglas no cumplidas.");
        }
    }

    /// <summary>
    /// Método asignado al botón rojo: Detiene la conexión.
    /// </summary>
    public void SelectEnteredRed()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("Botón rojo: Luz no activa. No se puede interactuar.");
            return;
        }

        if (isConnectionActive)
        {
            Debug.Log("Botón rojo: Desactivando conexión.");
            isConnectionActive = false;

            StopSound(buttonAudioSourceConexion); // Detener sonido continuo
            PlaySound(buttonAudioSourceDefault);  // Reproducir sonido por defecto

            ActivateSphere(false); // Activa esfera "liberado"
        }
        else
        {
            Debug.Log("Botón rojo: No hay conexión activa.");
        }
    }

    /// <summary>
    /// Reproduce un sonido si no está en reproducción.
    /// </summary>
    private void PlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Detiene un sonido.
    /// </summary>
    private void StopSound(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Reproduce el sonido continuo de conexión.
    /// </summary>
    private void PlayConnectionSound()
    {
        if (isConnectionActive)
        {
            PlaySound(buttonAudioSourceConexion);
        }
    }

    /// <summary>
    /// Activa la esfera "presionado" o "liberado".
    /// </summary>
    private void ActivateSphere(bool isActive)
    {
        if (sphereOnPress != null)
        {
            sphereOnPress.SetActive(isActive);
        }

        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(!isActive);
        }
    }
}
