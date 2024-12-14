using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Componente de texto al que se le aplicará el efecto
    public float typingSpeed = 0.05f;  // Velocidad de escritura (en segundos por letra)

    private string fullText;  // Texto completo a mostrar
    private Coroutine typingCoroutine;  // Para gestionar la corrutina

    private void Start()
    {
        // Guardamos el texto completo y lo limpiamos para empezar
        fullText = textComponent.text;
        textComponent.text = "";

        // Iniciar la animación de escritura
        StartTyping();
    }

    public void StartTyping()
    {
        // Si ya hay una corrutina en ejecución, la detenemos
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Iniciar la corrutina de escritura
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textComponent.text = "";  // Asegurarse de que esté vacío al comenzar

        foreach (char letter in fullText)
        {
            textComponent.text += letter;  // Agregar letra por letra
            yield return new WaitForSeconds(typingSpeed);  // Pausa antes de la siguiente letra
        }
    }
}
