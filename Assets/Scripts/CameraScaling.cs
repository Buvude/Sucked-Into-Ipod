using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    private BoxCollider2D boundCollider;
    public float _buffer;
    Camera cam;

    private void Awake()
    {
        boundCollider = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider2D>();
        cam = GetComponent<Camera>();
    }
    void Start()
    {

        var (center, size) = CalculateOrthoSize();

        cam.transform.position = center;
        cam.orthographicSize = size;

    }
    private (Vector3 center, float size) CalculateOrthoSize()
    {
        var bounds = new Bounds();
        bounds.Encapsulate(boundCollider.bounds);
        bounds.Expand(_buffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * cam.pixelHeight / cam.pixelWidth; // pixel height / pixel width = aspect ratio
        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}