using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform ship;

	private void Update () {
        Follow();
	}
    private void Follow()
    {
        transform.position = ship.position;
        transform.position -= Vector3.forward*10;
    }
}
