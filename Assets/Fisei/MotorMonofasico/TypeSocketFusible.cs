using UnityEngine;

public enum SocketFusibleType
{
    F8A1,
    F8A2,
    F8A3,
    F5A1,
    F5A2,
    F5A3,
    Entrada1,
    Entrada2,
    Entrada3
}

public class TypeSocketFusible : MonoBehaviour
{
    public SocketFusibleType socketType; // Tipo de fusible que se espera para este socket
}
