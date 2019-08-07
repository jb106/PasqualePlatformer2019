using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [Header("Basic informations")]
    public Vector3 cameraOffset = new Vector3();
    public float followSpeed = 10f;
    public Transform target = null;

    private void FixedUpdate()
    {
        if (!target)
            return;

        Vector3 desiredPosition = target.position;
        desiredPosition += cameraOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
    }
}
