using UnityEngine;
using UnityEngine.UI;

public class TrifasicSelector : MonoBehaviour
{
    // Imágenes para representar el estado de los fusibles
    public Image vistofusible1;
    public Image vistofusible2;
    public Image vistofusible3;
    public Image xfusible1;
    public Image xfusible2;
    public Image xfusible3;

    public GameObject[] sockets; // Sockets disponibles
    public GameObject[] fuses5A; // Fusibles correctos

    public GameObject fuseCanvas;

    // Referencias a los tres objetos y sus sockets correspondientes
    public GameObject Objeto1;
    public GameObject Objeto2;
    public GameObject Objeto3;

    public GameObject Socket1; // Socket asociado al Objeto1
    public GameObject Socket2; // Socket asociado al Objeto2
    public GameObject Socket3; // Socket asociado al Objeto3

    private int correctFusesPlaced = 0;
    private bool[] socketOccupied;

    void Start()
    {
        fuseCanvas.SetActive(true);
        socketOccupied = new bool[sockets.Length];
    }

    public void EnableFuseCanvas()
    {
        fuseCanvas.SetActive(true);
        ResetFuseStatus();
    }

    private void ResetFuseStatus()
    {
        correctFusesPlaced = 0;
        for (int i = 0; i < socketOccupied.Length; i++)
        {
            socketOccupied[i] = false;
        }

        // Resetear imágenes
        vistofusible1.gameObject.SetActive(false);
        vistofusible2.gameObject.SetActive(false);
        vistofusible3.gameObject.SetActive(false);
        xfusible1.gameObject.SetActive(false);
        xfusible2.gameObject.SetActive(false);
        xfusible3.gameObject.SetActive(false);
    }

    public void PlaceFuse(GameObject fuse, int socketIndex)
    {
        if (socketOccupied[socketIndex])
        {
            Debug.Log($"El socket {socketIndex} ya está ocupado.");
            return;
        }

        bool isCorrect = false;

        foreach (var validFuse in fuses5A)
        {
            if (fuse == validFuse)
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            RemoveIncorrectFuse(socketIndex);
            correctFusesPlaced++;
            socketOccupied[socketIndex] = true;
            ShowCorrectFuse(socketIndex);
        }
        else
        {
            RemoveCorrectFuse(socketIndex);
            ShowIncorrectFuse(socketIndex);
        }

        if (correctFusesPlaced == sockets.Length)
        {
            CompleteTask();
        }
    }

    private void ShowCorrectFuse(int index)
    {
        switch (index)
        {
            case 0: vistofusible1.gameObject.SetActive(true); break;
            case 1: vistofusible2.gameObject.SetActive(true); break;
            case 2: vistofusible3.gameObject.SetActive(true); break;
        }
    }

    private void ShowIncorrectFuse(int index)
    {
        switch (index)
        {
            case 0: xfusible1.gameObject.SetActive(true); break;
            case 1: xfusible2.gameObject.SetActive(true); break;
            case 2: xfusible3.gameObject.SetActive(true); break;
        }
    }

    private void RemoveCorrectFuse(int index)
    {
        switch (index)
        {
            case 0: vistofusible1.gameObject.SetActive(false); break;
            case 1: vistofusible2.gameObject.SetActive(false); break;
            case 2: vistofusible3.gameObject.SetActive(false); break;
        }
    }

    private void RemoveIncorrectFuse(int index)
    {
        switch (index)
        {
            case 0: xfusible1.gameObject.SetActive(false); break;
            case 1: xfusible2.gameObject.SetActive(false); break;
            case 2: xfusible3.gameObject.SetActive(false); break;
        }
    }

    private void CompleteTask()
    {
        fuseCanvas.SetActive(false);

        // Regresar los objetos a sus sockets
        Objeto1.transform.position = Socket1.transform.position;
        Objeto2.transform.position = Socket2.transform.position;
        Objeto3.transform.position = Socket3.transform.position;

        Debug.Log("Todos los fusibles de 5A fueron colocados correctamente y los objetos regresaron a sus sockets.");
    }

    public void RemoveFuse(int socketIndex)
    {
        if (socketOccupied[socketIndex])
        {
            correctFusesPlaced--;
            socketOccupied[socketIndex] = false;
            Debug.Log($"Fusible removido del socket {socketIndex}.");

            // Resetear las imágenes de estado al remover el fusible
            ResetFuseImage(socketIndex);
        }
    }

    private void ResetFuseImage(int index)
    {
        switch (index)
        {
            case 0:
                vistofusible1.gameObject.SetActive(false);
                xfusible1.gameObject.SetActive(false);
                break;
            case 1:
                vistofusible2.gameObject.SetActive(false);
                xfusible2.gameObject.SetActive(false);
                break;
            case 2:
                vistofusible3.gameObject.SetActive(false);
                xfusible3.gameObject.SetActive(false);
                break;
        }
    }

    public bool IsSocketOccupied(int socketIndex)
    {
        return socketOccupied[socketIndex];
    }
}
