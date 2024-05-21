using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [Header("Main Settings")]
    [Tooltip("Объект за которым следует камера")]
    public Transform targetTransform;
    public float smoothSpeed = 0.125f;
    private Vector3 cameraPositionOffset;
    private Vector3 cameraRotationOffset;
    
    void Start()
    {
        cameraPositionOffset = transform.position;
        cameraRotationOffset = transform.rotation.eulerAngles;
    }

    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {

        Vector3 desiredPosition = targetTransform.position + targetTransform.rotation * cameraPositionOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredrotation = targetTransform.rotation * Quaternion.Euler(cameraRotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }
}
