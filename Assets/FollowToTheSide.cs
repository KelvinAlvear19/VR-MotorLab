using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTheSide : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right,Vector3.up).normalized*offset.x
            + Vector3.ProjectOnPlane(target.forward,Vector3.up).normalized*offset.z;
    }
}
