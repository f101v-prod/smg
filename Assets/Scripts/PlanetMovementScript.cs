using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlanetMovementScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    
    [SerializeField]
    private int segments = 60;

    [SerializeField]
    private GameObject star;

    private LineRenderer _lineRenderer;

    private float _angle = 0;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        var offset = transform.position - star.transform.position;
        _angle = Mathf.Atan2(offset.y, offset.x);

        DrawCircle();
    }

    void Update()
    {
        Rotate();
    }

    void DrawCircle()
    {
        float radius = Vector2.Distance(star.transform.position , transform.position);
        _lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float progress = (float)i / segments;
            float angle = progress * 2 * Mathf.PI;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 pointPosition = new Vector3(x, y, 0) + star.transform.position;

            _lineRenderer.SetPosition(i, pointPosition);
        }
    }

    public void Rotate()
    {
        if (LevelManager.Instance.State != MovementState.Move)
            return;

        _angle += speed * Time.deltaTime;
        
        if (_angle > Mathf.PI * 2)
            _angle -= Mathf.PI * 2;
        if (_angle < 0)
            _angle += Mathf.PI * 2;

        float radius = Vector2.Distance(transform.position, star.transform.position);
        float x = star.transform.position.x + Mathf.Cos(_angle) * radius;
        float y = star.transform.position.y + Mathf.Sin(_angle) * radius;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    public GameObject Star()
    {
        return star;
    }

    public float Speed()
    {
        return speed;
    }

    public float Angle()
    {
        return _angle;
    }
}
