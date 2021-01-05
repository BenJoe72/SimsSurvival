using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float MoveSpeed = 1f;

    public void Move(Vector2 moveVector)
    {
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);
        transform.position += movement * MoveSpeed * Time.deltaTime;
    }
}
