using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform playerTransform;
    // add offset to place to botton of player
    public Vector3 offset = new Vector3(0, -75, 0);

    public Joystick joystick;

    private void Start()
    {
        if (playerTransform == null)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
        //}
        //else
        //{
        //    // Delete the shadow follow when the hero dies
        //    Destroy(gameObject);
        //}
    }
}
