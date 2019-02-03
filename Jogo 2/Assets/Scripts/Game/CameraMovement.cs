using UnityEngine;
using System.Collections;
 
public class CameraMovement : MonoBehaviour
{
    public Vector3 lastMousePosition;
    public Vector3 currentMousePosition;
    public Vector3 delta;

    private void Update()
    {
        DragCamera();
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
}