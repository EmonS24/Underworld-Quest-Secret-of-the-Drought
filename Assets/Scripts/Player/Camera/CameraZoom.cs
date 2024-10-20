using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoom;
    private float zoomMultiplier = 4f;
    [SerializeField] private float minZoom = 2.4f;
    [SerializeField] private float maxZoom = 5.4f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField] private Camera cam;

    private void Start()
    {
        zoom = cam.orthographicSize;
    }

    private void Update()
    {
        HandleZoom();
        MaxZoomControl();
    }

    private void HandleZoom()
    {
        // Zoom in and out using the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }

    private void MaxZoomControl()
    {
        // Max zoom out when pressing 'Z' and max zoom in when pressing 'X'
        if (Input.GetKeyDown(KeyCode.Z))
        {
            zoom = maxZoom; // Zoom out fully
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            zoom = minZoom; // Zoom in fully
        }
    }
}
