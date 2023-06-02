using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public float zoom;
    public float zoomMin = -5;
    public float zoomMax = 5;
    public float seekTime = 1.0f;
    public bool smoothZoomIn = false;

    private Vector3 defaultLocalPosition;
    private Transform thisTransform;
    public float currentZoom;
    private float targetZoom;
    private float zoomVelocity;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        defaultLocalPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        OnZoom();
    }
    void OnZoom()
    {
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        Vector3 zoomedPosition = defaultLocalPosition + thisTransform.parent.InverseTransformDirection(thisTransform.forward * zoom);

        targetZoom = zoom;

        targetZoom = Mathf.Clamp(targetZoom, zoomMin, zoomMax);
        if (smoothZoomIn)
        {
            currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, seekTime);
        }
        else
        {
            currentZoom = targetZoom;
        }     
        zoomedPosition = defaultLocalPosition + thisTransform.parent.InverseTransformDirection(thisTransform.forward * currentZoom);

        thisTransform.localPosition = zoomedPosition;
    }
    public void SetZoom(float zoom)
    {
        this.zoom = zoom;
    }
}
