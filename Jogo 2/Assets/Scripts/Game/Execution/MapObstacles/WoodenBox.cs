using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : GridElement, IBulletTarget
{
    public WoodenBox(int x, int y)
    {
        position.x = x;
        position.y = y;
    }
    public void OnBulletTouch()
    {
        ConsoleLine.WriteLine("ON BULLET TOUCH");
    }
}
