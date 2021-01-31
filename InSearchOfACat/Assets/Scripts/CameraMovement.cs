using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform leftPivot, rightPivot, player;
    public bool camMovementEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (camMovementEnabled)
        {
            if (player.position.x > rightPivot.position.x)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (player.position.x < leftPivot.position.x)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            }
        }
    }
}
