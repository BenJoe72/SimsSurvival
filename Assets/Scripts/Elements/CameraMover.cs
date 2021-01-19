using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float ZoomSpeed = 1f;
    public float RoateSpeed = 1f;
    public float MinZoom = 1f;
    public float MaxZoom = 10f;

    private Vector2 _prevMousePos;
    private float _zoomLevel;

    private void Start()
    {
        _zoomLevel = transform.localScale.x;
    }

    public void Move(Vector2 moveVector)
    {
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y).normalized;
        movement = transform.TransformVector(movement);
        movement.y = 0;
        transform.position += movement * MoveSpeed;
    }

    public void Zoom(float amount)
    {
        _zoomLevel = Mathf.Clamp(_zoomLevel - (amount * ZoomSpeed), MinZoom, MaxZoom);
        transform.localScale = Vector3.one * _zoomLevel;
    }

    public void StartRotateCamera(MouseClickData mouse)
    {
        _prevMousePos = mouse.ScreenPosition;
    }

    public void RotateCamera(MouseClickData mouse)
    {
        float rotation = _prevMousePos.x - mouse.ScreenPosition.x;
        transform.Rotate(Vector3.up, -rotation * RoateSpeed, Space.World);
        _prevMousePos = mouse.ScreenPosition;
    }
}
