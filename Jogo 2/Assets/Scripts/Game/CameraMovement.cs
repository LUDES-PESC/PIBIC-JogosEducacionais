using UnityEngine;
using System.Collections;
using DG.Tweening;
 
public class CameraMovement : MonoBehaviour
{
    public Vector3 lastMousePosition;
    public Vector3 currentMousePosition;
    public Vector3 delta;

    private void Update()
    {
        DragCamera();
        ZoomInAndOut();
    }
    private void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            return;
        }if (Input.GetMouseButton(0))
        {
            currentMousePosition = Input.mousePosition;
            delta = Camera.main.ScreenToWorldPoint(currentMousePosition) - Camera.main.ScreenToWorldPoint(lastMousePosition);
            delta.z = 0;
            transform.position -= delta;
        }if (Input.GetMouseButtonUp(0))
        {

        }
        lastMousePosition = currentMousePosition;
    }
    private void ZoomInAndOut()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            Camera.main.DOOrthoSize(Camera.main.orthographicSize - 0.5f, 0.2f);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            Camera.main.DOOrthoSize(Camera.main.orthographicSize + 0.5f, 0.2f);
        }
    }
}