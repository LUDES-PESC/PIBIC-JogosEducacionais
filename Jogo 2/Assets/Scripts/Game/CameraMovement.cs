using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public static int canBeMoved;
    private bool startedDrag;

    private Vector3 lastMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 delta;

    private void Start()
    {
        //canBeMoved = 0;
    }
    private void Update()
    {
        if(canBeMoved == 0)
            DragCamera();
    }
    private void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
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
    public static void MoveTo(Vector2 position)
    {
        var pos = (Vector3)position - Vector3.forward * 10;
        Camera.main.transform.DOMove(pos, Globals.TIME_BETWEEN_TURNS);
    }
}
