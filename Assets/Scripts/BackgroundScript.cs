using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScript : MonoBehaviour
{
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();

        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;

        renderer.size = new Vector2(
            width,
            height
        );
    }
}
