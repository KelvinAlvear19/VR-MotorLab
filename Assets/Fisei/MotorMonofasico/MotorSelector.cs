using UnityEngine;

public class MotorSelector : MonoBehaviour
{
    public GameObject currentMotor;  // Motor actualmente visible
    public GameObject[] motors;      // Los motores alternativos (desactivados inicialmente)

    // Este método se llama cuando un botón es presionado
    public void ReplaceMotor(int motorIndex)
    {
        // Desactivar el motor actual
        currentMotor.SetActive(false);

        // Activar el motor seleccionado por el índice
        currentMotor = motors[motorIndex];
        currentMotor.SetActive(true);
    }
}
