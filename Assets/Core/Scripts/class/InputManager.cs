using UnityEngine;

public class InputManager
{
    // Hide Cursor
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Move
    public Vector3 GetMoveVector()
    {
        float hv = Input.GetAxisRaw(Parameter.Input.Horizontal);
        float vv = Input.GetAxisRaw(Parameter.Input.Vertical);
        Vector3 v3 = new Vector3(hv, 0, vv);

        return Vector3.ClampMagnitude(v3, 1);
    }

    // Rotate
    public Vector2 GetRotateVecter()
    {
        float hv = Input.GetAxisRaw(Parameter.Input.MouseX);
        float vv = Input.GetAxisRaw(Parameter.Input.MouseY);

        return new Vector2(hv, vv);
    }

    // Fire
    public bool GetFireInput()
    {
        return Input.GetKey(Parameter.Input.Fire);
    }
}
