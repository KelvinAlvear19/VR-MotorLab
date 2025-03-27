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
    [SerializeField] private GameObject sphereOnPress; // Esfera activada al presionar el bot�n
    [SerializeField] private GameObject sphereOnRelease; // Esfera activada al soltar el bot�n

    [Header("Circuit Validator")]
    [SerializeField] private CircuitValidator circuitValidator; // Referencia al CircuitValidator
    [SerializeField] private string groupIdForTwelveRules = "DoceReglas"; // GroupId para el grupo de 9 reglas

    private bool isConnectionActive = false; // Estado de la conexi�n

    /// <summary>
    /// M�todo asignado al bot�n verde: Verifica reglas y activa la conexi�n.
    /// </summary>
    public void SelectEnteredGreen()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("Bot�n verde: Luz no activa. No se puede interactuar.");
            return;
        }

        // Verificar si las reglas se han cumplido
        bool reglasCumplidas = circuitValidator.AreGroupRulesMet(groupIdForTwelveRules);

        if (reglasCumplidas)
        {
            Debug.Log("Bot�n verde: Reglas cumplidas. Activando conexi�n.");
            isConnectionActive = true;

            PlaySound(buttonAudioSourceDefault); // Reproduce sonido por defecto
            ActivateSphere(true); // Activa esfera "presionado"

            // Retraso para reproducir el sonido continuo
            Invoke(nameof(PlayConnectionSound), 1.0f);
        }
        else
        {
            Debug.Log("Bot�n verde: Reglas no cumplidas.");
        }
    }

    /// <summary>
    /// M�todo asignado al bot�n rojo: Detiene la conexi�n.
    /// </summary>
    public void SelectEnteredRed()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("Bot�n rojo: Luz no activa. No se puede interactuar.");
            return;
        }

        if (isConnectionActive)
        {
            Debug.Log("Bot�n rojo: Desactivando conexi�n.");
            isConnectionActive = false;

            StopSound(buttonAudioSourceConexion); // Detener sonido continuo
            PlaySound(buttonAudioSourceDefault);  // Reproducir sonido por defecto

            ActivateSphere(false); // Activa esfera "liberado"
        }
        else
        {
            Debug.Log("Bot�n rojo: No hay conexi�n activa.");
        }
    }

    /// <summary>
    /// Reproduce un sonido si no est� en reproducci�n.
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
    /// Reproduce el sonido continuo de conexi�n.
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
