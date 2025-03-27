using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrifasicTest : MonoBehaviour
{
    [SerializeField] private Light targetLight; // Luz objetivo que indica si se puede interactuar
    [SerializeField] private AudioSource buttonAudioSourceDefault; // Sonido para el grupo de 5 reglas cumplidas
    [SerializeField] private AudioSource buttonAudioSourceConexion; // Sonido para el grupo de 7 reglas cumplidas
    [SerializeField] private XRGrabInteractable parentGrabInteractable; // Objeto que debe desactivarse temporalmente

    [SerializeField] private GameObject sphereOnPress; // Esfera que se activa cuando se presiona el botón
    [SerializeField] private GameObject sphereOnRelease; // Esfera que se activa cuando se suelta el botón

    [SerializeField] private CircuitValidator circuitValidator; // Referencia al CircuitValidator
    [SerializeField] private string groupIdForSixRules = "PrimerasSeisTrifasico"; // GroupId para el grupo de 5 reglas
    [SerializeField] private string groupIdForNineRules = "PrimerasNueveTrifasico"; // GroupId para el grupo de 7 reglas

    private void Start()
    {
        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(true); // Inicializar la esfera de liberación activa
        }
    }

    /// <summary>
    /// Método llamado cuando se presiona el botón.
    /// </summary>
    public void OnButtonSelectEntered()
    {
        // Verificar si la luz está encendida antes de continuar
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("No se puede interactuar porque la luz targetLight no está activa.");
            return;
        }

        DisableParentGrab(); // Deshabilitar la interacción con el objeto padre

        // Validar las reglas
        bool cincoReglasCumplidas = circuitValidator.AreGroupRulesMet(groupIdForSixRules); // Validar el grupo de 5 reglas
        bool sieteReglasCumplidas = circuitValidator.AreGroupRulesMet(groupIdForNineRules); // Validar el grupo de 7 reglas

        // Determinar el comportamiento según el estado de las reglas
        if (sieteReglasCumplidas && cincoReglasCumplidas)
        {
            // Si se cumplen las 7 reglas
            Debug.Log("7 reglas cumplidas. Reproduciendo sonido completo.");
            PlaySound(buttonAudioSourceConexion); // Reproducir sonido completo
            ActivateSphere(true); // Encender la esfera de "presionado"
        }
        else if (cincoReglasCumplidas)
        {
            // Si se cumplen las primeras 5 reglas
            Debug.Log("5 reglas cumplidas. Reproduciendo sonido por defecto.");
            PlaySound(buttonAudioSourceDefault); // Reproducir sonido por defecto
            ActivateSphere(true); // Encender la esfera de "presionado"
        }
        else
        {
            // Si no se cumplen al menos 5 reglas
            Debug.Log("Conexiones no válidas. No se reproduce ningún sonido ni se enciende nada.");
            ActivateSphere(false); // Asegurarse de apagar la esfera de "presionado"
        }

        // Apagar la esfera de "liberado"
        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(false);
        }
    }

    /// <summary>
    /// Método llamado cuando se suelta el botón.
    /// </summary>
    public void OnButtonSelectExited()
    {
        // Verificar si la luz está encendida antes de continuar
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("No se puede interactuar porque la luz targetLight no está activa.");
            return;
        }

        EnableParentGrab(); // Rehabilitar la interacción con el objeto padre

        StopAllSounds(); // Detener todos los sonidos en reproducción

        // Activar la esfera de "liberado"
        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(true);
        }

        // Apagar la esfera de "presionado"
        if (sphereOnPress != null)
        {
            sphereOnPress.SetActive(false);
        }
    }

    /// <summary>
    /// Reproduce un sonido si no está ya en reproducción.
    /// </summary>
    private void PlaySound(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// Detiene todos los sonidos en reproducción.
    /// </summary>
    private void StopAllSounds()
    {
        buttonAudioSourceConexion.Stop();
        buttonAudioSourceDefault.Stop();
    }

    /// <summary>
    /// Activa o desactiva la esfera de "presionado".
    /// </summary>
    private void ActivateSphere(bool isActive)
    {
        if (sphereOnPress != null)
        {
            sphereOnPress.SetActive(isActive);
        }
    }

    /// <summary>
    /// Deshabilita la interacción con el objeto padre.
    /// </summary>
    private void DisableParentGrab()
    {
        if (parentGrabInteractable != null)
        {
            parentGrabInteractable.enabled = false;
        }
    }

    /// <summary>
    /// Habilita la interacción con el objeto padre.
    /// </summary>
    private void EnableParentGrab()
    {
        if (parentGrabInteractable != null)
        {
            parentGrabInteractable.enabled = true;
        }
    }
}
