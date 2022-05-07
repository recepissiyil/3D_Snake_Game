using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached the Camera at scene 1 to 10
public class FollowCamera : MonoBehaviour
{
    #region Camera follow the Player
    public Vector3 cameraOffset;
    public Transform targetObject;
    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        if (lookAtTarget)
        {
            transform.LookAt(targetObject);
        }

    } 
    #endregion


}
