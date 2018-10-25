using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Vector2 position;
    public Vector2 lookDirection;
    public bool flag;

    public void Walk()
    {
        position += lookDirection;
        print("New Position: " + position);
    }
    public void Wait()
    {
        print("Waiting one turn");
    }
    public void Shoot()
    {
        print("Shooting bullet in " + lookDirection + " direction from " + position);
    }
    public void Look(Vector2 direction)
    {
        lookDirection = direction;
        print("Look Direction: " + lookDirection);
    }
    public void RiseFlag()
    {
        flag = true;
        print("Flag rised: " + flag);
    }
}

