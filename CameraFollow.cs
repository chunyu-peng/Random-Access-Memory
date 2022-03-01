using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    private float smoothSpeed = 10f;
    // add offset to change camera's layer
    private Vector3 offset = new Vector3(0, 0, -50);
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 desiredPosition = playerTransform.position + offset;
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, desiredPosition.z), smoothSpeed * Time.deltaTime);
            // consider the case player dies
            if (playerTransform != null)
            {
                transform.position = smoothedPosition;
            }
            //playerTransform.LookAt(playerTransform);
        }
    }
}
