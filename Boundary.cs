using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private float mapWidth = 1580f;
    private float mapHeight = 560f;
    public Transform playerTransform;

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 pos = playerTransform.position;
            pos.x = Mathf.Max(Mathf.Min(pos.x, mapWidth), -475f);
            pos.y = Mathf.Max(Mathf.Min(pos.y, mapHeight), -95f);
            playerTransform.position = pos;
        }
    }
}
