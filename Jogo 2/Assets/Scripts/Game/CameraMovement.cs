using UnityEngine;
using System.Collections;
using DG.Tweening;
 
public class CameraMovement : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 delta;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    private const float zoomTaxOnButton = 1f;
    private const float zoomTaxOnMouseScroll = 0.5f;

    private bool startedDrag;

    private void Update()
    {
        DragCamera();
        ZoomInAndOut();
    }
    private void DragCamera()
    {
        if (CommandPanel.mouseInside)
            startedDrag = false;
        if (Input.GetMouseButtonDown(0) && !CommandPanel.mouseInside)
        {
            lastMousePosition = Input.mousePosition;
            startedDrag = true;
            return;
        }
        if (Input.GetMouseButton(0) && startedDrag)
        {
            currentMousePosition = Input.mousePosition;
            delta = Camera.main.ScreenToWorldPoint(currentMousePosition) - Camera.main.ScreenToWorldPoint(lastMousePosition);
            delta.z = 0;
            transform.position -= delta;
        }
        lastMousePosition = currentMousePosition;
    }
    private void ZoomInAndOut()
    {
        if(Input.mouseScrollDelta.y > 0)
            ZoomInOnScroll();
        if (Input.mouseScrollDelta.y < 0)
            ZoomOutOnScroll();
    }
    private void ZoomInOnScroll()
    {
        Camera.main.DOOrthoSize(Mathf.Clamp(Camera.main.orthographicSize - zoomTaxOnMouseScroll, minZoom, maxZoom), 0.2f);
    }
    private void ZoomOutOnScroll()
    {
        Camera.main.DOOrthoSize(Mathf.Clamp(Camera.main.orthographicSize + zoomTaxOnMouseScroll, minZoom, maxZoom), 0.2f);
    }
    public void ZoomInOnButton()
    {
        Camera.main.DOOrthoSize(Mathf.Clamp(Camera.main.orthographicSize - zoomTaxOnButton, minZoom, maxZoom), 0.2f);
    }
    public void ZoomOutOnButton()
    {
        Camera.main.DOOrthoSize(Mathf.Clamp(Camera.main.orthographicSize + zoomTaxOnButton, minZoom, maxZoom), 0.2f);
    }
}