using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private Vector2 _screenPosition = Vector2.zero;
    private bool _isPressed = false;

#if ENABLE_INPUT_SYSTEM

    public void OnPress(InputValue value)
    {
        PressInput(value.isPressed);
    }

    public void OnScreenPosition(InputValue value)
    {
        ScreenPositionInput(value.Get<Vector2>());
    }

#endif

    public void PressInput(bool pressed)
    {
        _isPressed = pressed;
    }

    public void ScreenPositionInput(Vector2 cursorPosition)
    {
        _screenPosition = cursorPosition;
    }

    public bool IsPressed()
    {
        return _isPressed;
    }

    public Vector2 ScreenPosition()
    {
        return _screenPosition;
    }
}
