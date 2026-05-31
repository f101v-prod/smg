using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputScript))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class CameraMovementScript : MonoBehaviour
{
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    private PlayerInputScript _input;
    
    private bool _isMoving = false;
    private Vector3 _origin = Vector3.zero;
    private Vector3 _diff = Vector3.zero;

    void Start()
    {
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
        _input = GetComponent<PlayerInputScript>();
    }

    private void LateUpdate()
    {
        if (_input.IsPressed())
        {
            _diff = GetMouseWorldPos - Camera.main.transform.position;
            if (!_isMoving)
            {
                _isMoving = true;
                _origin = GetMouseWorldPos;
            }
        }
        else
        {
            _isMoving = false;
        }

        if (!_isMoving)
            return;

        Camera.main.transform.position = _origin - _diff;
    }

    private Vector3 GetMouseWorldPos => 
        _playerInput.camera.ScreenToWorldPoint(
                _input.ScreenPosition());
}
