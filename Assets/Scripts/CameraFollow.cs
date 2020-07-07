using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform car;
    public float maxZoom, minZoom,zoomBorder;
    private Vector3 positionRef;
    public float smoothTime;
    public float zoomLimiter;
    public float maxYOffset;
    private Camera cam;
    [Range(0,1)]
    public float XBias = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Bounds bounds = GetBounds();
        Vector3 center = GetCenter(bounds);
        float distance = GetDistance(bounds);
        Zoom(distance);
        Move(center);
    }

    Bounds GetBounds() {
        Bounds bounds = new Bounds(player.position,Vector3.zero);
        bounds.Encapsulate(player.position);
        bounds.Encapsulate(car.position);
        return bounds;
    }

    Vector3 GetCenter(Bounds bounds) {
        float biasedX = Mathf.Lerp(bounds.min.x, bounds.max.x, XBias);
        Vector3 center = bounds.center;
        center.x = biasedX;
        return center;
    }

    float GetDistance(Bounds bounds) {
        return bounds.size.x;
    }

    void Move(Vector3 center) {
        float yOffset = adjustYOffset();
        Vector3 newPosition = center + Vector3.forward * -10 + Vector3.up* yOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionRef, smoothTime);
    }

    void Zoom(float distance) {
        float newZoom = getZoomLevel(distance);
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime*5);
    }

    float getZoomLevel(float distance) {
        return distance / 2.5f + zoomBorder;
    }

    float adjustYOffset() {
        float ratio = cam.orthographicSize / maxZoom;
        float yOffset = maxYOffset * ratio;
        return yOffset;
    }
}
