using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerResourceScript))]
[RequireComponent(typeof(PlayerInputScript))]
#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private GameObject currentPlanet;

    [SerializeField]
    private int currentFuel = 100;

    private GameObject _selectedPlanet;

    [SerializeField]
    private LayerMask planetsLayersMask;

#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    private PlayerInputScript _input;

    private bool _isMoveHold = false;

    private Vector3 _destination = Vector3.zero;

    [SerializeField]
    private float speed = 10;

    private PlayerResourceScript _resourceCollector;

    void Start()
    {
        if (currentPlanet != null)
            transform.position = currentPlanet.transform.position;

#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
        _input = GetComponent<PlayerInputScript>();

        _resourceCollector = GetComponent<PlayerResourceScript>();
        _resourceCollector.Collect(currentPlanet);
    }

    // Update is called once per frame
    void Update()
    {
        SelectPlanet();
        Move();
    }

    private void Move()
    {
        if (LevelManager.Instance.State != MovementState.Move)
            return;

        if (currentPlanet == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            _destination,
            speed * Time.deltaTime
        );

        Vector2 currentDistance = transform.position - _destination;

        if (currentDistance.sqrMagnitude <= 0.0001)
        {
            _resourceCollector.Collect(currentPlanet);
            LevelManager.Instance.State = MovementState.Wait;
        }
    }

    private void SelectPlanet()
    {
        if (LevelManager.Instance.State != MovementState.Wait)
            return;

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
        worldPosition.z = transform.position.z;

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);
        if (hit == null)
            return;

        if (!LayerChecker.IsInLayerMask(hit.gameObject.layer, planetsLayersMask))
            return;

        if (_selectedPlanet == hit.gameObject)
        {
            int distance = 0;
            
            if (_selectedPlanet != currentPlanet)
            {
                _destination = CalculateFlightDestination();
                distance = Mathf.CeilToInt(Vector3.Distance(
                    transform.position, _destination));
            }

            if (currentFuel - distance < 0)
                return;

            currentFuel -= distance;
            currentPlanet = _selectedPlanet;
            _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.white;
            _selectedPlanet = null;

            LevelManager.Instance.State = MovementState.Move;
        }
        else
        {
            if (_selectedPlanet != null)
                _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.white;

            _selectedPlanet = hit.gameObject;
            _selectedPlanet.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private Vector3 CalculateFlightDestination()
    {
        if (_selectedPlanet == null)
            return transform.position;

        if (_selectedPlanet == currentPlanet)
            return transform.position;

        var planetController =
            _selectedPlanet.GetComponent<PlanetMovementScript>();

        Assert.IsFalse(planetController == null);  

        var tMin = 0f;

        var distanceToCenter = Vector2.Distance(
            transform.position,
            planetController.Star().transform.position
        );

        float timeToCenter = distanceToCenter / speed;
        float planetPeriod = 2f * Mathf.PI / Mathf.Abs(planetController.Speed());
        float tMax = timeToCenter + (planetPeriod * 2f);

        float orbitRadius = Vector2.Distance(
            _selectedPlanet.transform.position,
            planetController.Star().transform.position
        );

        int iterations = 25;
        Vector3 destination = _selectedPlanet.transform.position;
        destination.z = transform.position.z;

        for (int i = 0; i < iterations; ++i)
        {
            float tMid = (tMin + tMax) / 2f;

            float futureAngle =
                planetController.Angle() + (planetController.Speed() * tMid);

            float x = planetController.Star().transform.position.x + Mathf.Cos(futureAngle) * orbitRadius;
            float y = planetController.Star().transform.position.y + Mathf.Sin(futureAngle) * orbitRadius;

            destination = new Vector3(x, y, destination.z);

            float distanceToTarget = Vector2.Distance(transform.position, destination);
            float timeToReach = distanceToTarget / speed;

            if (timeToReach > tMid)
                tMin = tMid;
            else
                tMax = tMid;
        }

        return destination;
    }
}