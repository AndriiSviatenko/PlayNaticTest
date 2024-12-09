using UnityEngine;

public class InputProvider
{
    public bool MouseClickDown() =>
        Input.GetMouseButtonDown(0);
    public bool MouseClick() =>
       Input.GetMouseButton(0);
    public bool MouseClickUp() =>
       Input.GetMouseButtonUp(0);
}
