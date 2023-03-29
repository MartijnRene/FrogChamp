using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Rotate(Quaternion.Euler(0,0,0));
            if (transform.position.y != 7) Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Rotate(Quaternion.Euler(0,0,180));
            if (transform.position.y != -7) Move(Vector3.down);

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Quaternion.Euler(0,0,90));
            if (transform.position.x != -6) Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Quaternion.Euler(0,0,270));
            if (transform.position.x != 6) Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    private void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
