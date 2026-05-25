using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputScript))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject currentPlanet;

    [SerializeField]
    private Int32 currentFuel = 100;

    private GameObject _selectedPlanet;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    private PlayerInputScript _input;

    private bool _isMoveHold = false;

    void Start()
    {
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
        _input = GetComponent<PlayerInputScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_input.IsPressed())
        {
            _isMoveHold = false;    
            return;
        }

        if (_isMoveHold)
            return;

        _isMoveHold = true;

        var pressPosition = _input.ScreenPosition();
        var worldPosition = 
            _playerInput.camera.ScreenToWorldPoint(pressPosition);
        worldPosition.z = this.transform.position.z;

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);
        if (hit == null)
            return;

        if (_selectedPlanet == hit.gameObject)
        {
            int distance = 0;
            if (currentPlanet != null)
                distance = Mathf.CeilToInt(Vector2.Distance(
                    currentPlanet.transform.position, _selectedPlanet.transform.position));

            if (currentFuel - distance < 0)
                return;

            currentFuel -= distance;
            currentPlanet = _selectedPlanet;
            _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.green;
            _selectedPlanet = null;
            this.transform.position = hit.transform.position;
        }
        else
        {
            if (_selectedPlanet != null)
                _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.green;

            _selectedPlanet = hit.gameObject;
            _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.seaGreen;
        }


    }
}