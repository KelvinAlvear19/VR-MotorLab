// CableConnection.cs
using UnityEngine;

public class CableConnection : MonoBehaviour
{
    [SerializeField]
    private GameObject endA;
    [SerializeField]
    private GameObject endB;

    private SocketType? socketA = null;
    private SocketType? socketB = null;

    public void ConnectEnd(Transform cableEndTransform, SocketType socket)
    {
        if (cableEndTransform == endA.transform)
        {
            // Si ambos extremos estaban conectados, primero desconectar para no duplicar
            if (socketA.HasValue && socketB.HasValue)
            {
                CircuitValidator.Instance.UpdateConnection(socketA.Value, socketB.Value, false);
            }
            socketA = socket;
        }
        else if (cableEndTransform == endB.transform)
        {
            if (socketA.HasValue && socketB.HasValue)
            {
                CircuitValidator.Instance.UpdateConnection(socketA.Value, socketB.Value, false);
            }
            socketB = socket;
        }

        if (socketA.HasValue && socketB.HasValue)
        {
            CircuitValidator.Instance.UpdateConnection(socketA.Value, socketB.Value, true);
        }
    }

    public void DisconnectEnd(Transform cableEndTransform, SocketType socket)
    {
        if (cableEndTransform == endA.transform && socketA.HasValue && socketA.Value == socket)
        {
            if (socketB.HasValue)
            {
                CircuitValidator.Instance.UpdateConnection(socketA.Value, socketB.Value, false);
            }
            socketA = null;
        }
        else if (cableEndTransform == endB.transform && socketB.HasValue && socketB.Value == socket)
        {
            if (socketA.HasValue)
            {
                CircuitValidator.Instance.UpdateConnection(socketA.Value, socketB.Value, false);
            }
            socketB = null;
        }
    }
}
