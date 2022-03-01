using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryLevel1 : MonoBehaviour
{
    public Vector2 mapWidth;
    public Vector2 mapHeight;
    public Transform playerTransform;

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 pos = playerTransform.position;
            pos.x = Mathf.Clamp(pos.x,mapWidth.x, mapWidth.y);
            pos.y = Mathf.Clamp(pos.y, mapHeight.x, mapHeight.y);
            playerTransform.position = pos;
        }
    }
}
