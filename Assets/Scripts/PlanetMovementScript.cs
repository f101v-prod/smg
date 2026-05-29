using Unity.VisualScripting;
using UnityEngine;

public class PlanetMovementScript : MonoBehaviour
{
    [SerializeField]
    int Segments = 360;

    public GameObject Star;

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawCircle();
    }

    void DrawCircle()
    {
        float radius = Vector2.Distance(Star.transform.position , transform.position);
        lineRenderer.positionCount = Segments;

        for (int i = 0; i < Segments; i++)
        {
            float progress = (float)i / Segments;
            float angle = progress * 2 * Mathf.PI;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 pointPosition = new Vector3(x, y, 0) + Star.transform.position;

            lineRenderer.SetPosition(i, pointPosition);
        }
    }
}
