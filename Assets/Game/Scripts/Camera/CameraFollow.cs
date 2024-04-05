using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Following")]
    public Transform followTarget;
    public Vector3 offset;

    [Header("Movement")]
    [Range(0f, 1f)]
    public float speed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position + offset, speed);
    }
}
