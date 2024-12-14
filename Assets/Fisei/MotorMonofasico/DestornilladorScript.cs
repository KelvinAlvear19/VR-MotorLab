using UnityEngine;

public class DestornilladorScript : MonoBehaviour
{
    public Animator destornilladorAnimator;  // Animator del destornillador
    public GameObject tornillo;             // El tornillo que va a seguir al destornillador
    public Transform puntaDestornillador;   // La punta del destornillador (para hacer que el tornillo la siga)

    private bool animacionActiva = false;

    private void OnTriggerEnter(Collider other)
    {
        // Detecta la colisi�n con la cabeza del tornillo
        if (other.CompareTag("Tornillo"))
        {
            // Activa la animaci�n del destornillador
            destornilladorAnimator.SetTrigger("Destornillar");

            // Hacer que el tornillo siga la punta del destornillador
            animacionActiva = true;
        }
    }

    private void Update()
    {
        if (animacionActiva)
        {
            // Mueve el tornillo hacia la punta del destornillador (puedes hacer esto en funci�n de la animaci�n)
            tornillo.transform.localPosition = puntaDestornillador.localPosition;

        }
    }
}
