using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ContactorTest : MonoBehaviour
{
    [SerializeField] private Light targetLight;
    [SerializeField] private AudioSource buttonAudioSourceDefault;
    [SerializeField] private AudioSource buttonAudioSourceConexion;
    [SerializeField] private XRGrabInteractable parentGrabInteractable;

    [SerializeField] private GameObject sphereOnPress;
    [SerializeField] private GameObject sphereOnRelease;

    [SerializeField] private CircuitValidator circuitValidator; // Ahora usamos CircuitValidator

    private void Start()
    {
        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(true);
        }
    }

    public void OnButtonSelectEntered()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("No se puede interactuar porque la luz targetLight no está activa.");
            return;
        }

        DisableParentGrab();

        bool cincoReglasValidas = circuitValidator.AreFiveRulesMet();  // Reglas 1 a 5
        bool sieteReglasValidas = circuitValidator.AreSevenRulesMet(); // Reglas 1 a 7

        if (sieteReglasValidas)
        {
            // 7 reglas cumplidas
            Debug.Log("7 reglas cumplidas. Reproduciendo sonido completo.");
            if (!buttonAudioSourceConexion.isPlaying)
            {
                buttonAudioSourceConexion.Play();
            }
            if (sphereOnPress != null)
            {
                sphereOnPress.SetActive(true);
            }
        }
        else if (cincoReglasValidas)
        {
            // 5 reglas cumplidas
            Debug.Log("5 reglas cumplidas. Reproduciendo sonido por defecto.");
            if (!buttonAudioSourceDefault.isPlaying)
            {
                buttonAudioSourceDefault.Play();
            }
            if (sphereOnPress != null)
            {
                sphereOnPress.SetActive(true);
            }
        }
        else
        {
            // Menos de 4 reglas cumplidas, nada se cumple.
            Debug.Log("Conexiones no válidas. No se reproduce ningún sonido ni se enciende nada.");
            if (sphereOnPress != null)
            {
                sphereOnPress.SetActive(false);
            }
        }

        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(false);
        }
    }

    public void OnButtonSelectExited()
    {
        if (!targetLight.isActiveAndEnabled)
        {
            Debug.Log("No se puede interactuar porque la luz targetLight no está activa.");
            return;
        }

        EnableParentGrab();

        // Detener los sonidos al soltar el botón
        buttonAudioSourceConexion.Stop();
        buttonAudioSourceDefault.Stop();

        if (sphereOnRelease != null)
        {
            sphereOnRelease.SetActive(true);
        }

        if (sphereOnPress != null)
        {
            sphereOnPress.SetActive(false);
        }
    }

    private void DisableParentGrab()
    {
        if (parentGrabInteractable != null)
        {
            parentGrabInteractable.enabled = false;
        }
    }

    private void EnableParentGrab()
    {
        if (parentGrabInteractable != null)
        {
            parentGrabInteractable.enabled = true;
        }
    }
}
