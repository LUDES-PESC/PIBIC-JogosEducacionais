using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PirateShip : MonoBehaviour {
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }
    private void Rotate()
    {
        Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance.z = 0;

        if (distance.magnitude > 1f)
            transform.up = distance.normalized;
    }
    private void Move()
    {
        Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance.z = 0;
        if (Input.GetMouseButton(0) && distance.magnitude > 1f)
        {
            Rotate();
            rigidbody2D.velocity = -transform.up*5f;
        }
        else
        {
            rigidbody2D.velocity *= 0.95f;
        }
    }
}
