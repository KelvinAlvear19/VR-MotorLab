using UnityEngine;

public class MandilHandler : MonoBehaviour
{
    public UIController uiController; // Referencia al controlador de la UI

    private bool isMandilCollected = false; // Indica si el mandil ha sido recogido

    public void OnMandilInteract()
    {
        if (!isMandilCollected)
        {
            CollectMandil();
        }
    }

    private void CollectMandil()
    {
        Debug.Log("Mandil recogido.");

        // Desactiva el objeto mandil
        gameObject.SetActive(false);

        // Marca el mandil como recogido
        isMandilCollected = true;

        // Actualiza la UI
        uiController.ActivateCheckmark("mandil");
    }
}
