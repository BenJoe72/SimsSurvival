using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float ZoomSpeed = 1f;
    public float MinZoom = 1f;
    public float MaxZoom = 10f;

    public void Move(Vector2 moveVector)
    {
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y).normalized;
        transform.position += movement * MoveSpeed * Time.deltaTime;
    }

    public void Zoom(float amount)
    {
        Vector3 newPos = transform.position + transform.forward * amount * ZoomSpeed * Time.deltaTime;
        if (newPos.y > MinZoom && newPos.y < MaxZoom)
            transform.position = newPos;
    }
}
