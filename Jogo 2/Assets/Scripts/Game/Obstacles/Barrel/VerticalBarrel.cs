using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBarrel : Obstacle, IBulletTarget
{
    public override bool OnPush(Vector2Int direction)
    {
        if (direction.x == 0 && Mathf.Abs(direction.y) == 1)
        {
            return Move(direction);
        }
        return false;
    }
    public void OnBulletTouch()
    {
        ConsoleLine.WriteLine(name + " WAS TOUCHED BY BULLET");
    }
    public override void TurnUpdate()
    {
        if (!FindObjectOfType<GroundMap>().IsSandTile(position))
        {
            CommandExecutor.AddBarrelOnWater(true, position);
        }
    }
}