using UnityEngine;
using System.Collections;

public class MouseClickData
{
    public MouseEventType EventType;
    public MouseButton Button;
    public Vector2 ScreenPosition;

    public MouseClickData(MouseEventType eventType, MouseButton button, Vector2 screenPosition)
    {
        EventType = eventType;
        Button = button;
        ScreenPosition = screenPosition;
    }
}
