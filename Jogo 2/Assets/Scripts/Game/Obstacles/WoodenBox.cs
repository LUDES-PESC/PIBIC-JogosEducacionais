using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : Obstacle, IBulletTarget
{
    public void OnBulletTouch()
    {
        ConsoleLine.WriteLine(name + " WAS TOUCHED BY BULLET");
    }
}