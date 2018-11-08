using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Vector2 position;
    public Vector2 lookDirection;

    public bool isWaiting;
    public bool flag;

    public void Walk()
    {
        isWaiting = false;
        position += lookDirection;
        ConsoleLine.WriteLine("WALK TO: " + position);
    }
    public void Wait()
    {
        ConsoleLine.WriteLine("WAIT");
        isWaiting = true;
    }
    public void Shoot()
    {
        isWaiting = false;
        ConsoleLine.WriteLine("SHOOT: " + lookDirection + " direction from " + position);
    }
    public void Look(Vector2 direction)
    {
        isWaiting = false;
        lookDirection = direction;
        ConsoleLine.WriteLine("LOOK TO: " + lookDirection);
    }
    public void RiseFlag()
    {
        isWaiting = false;
        flag = true;
        ConsoleLine.WriteLine("FLAG: " + flag);
    }
    public bool IsAbove(Vector2 position)
    {
        print(this.position + "/" + position);
        return this.position == position;
    }
}

