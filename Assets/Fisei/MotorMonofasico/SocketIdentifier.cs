// SocketIdentifier.cs
using UnityEngine;

public class SocketIdentifier : MonoBehaviour
{
    [SerializeField]
    private SocketType socketType;

    public SocketType SocketType => socketType;
}
