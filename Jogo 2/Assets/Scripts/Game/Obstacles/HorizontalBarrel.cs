using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBarrel : Obstacle, IBulletTarget {

    public override bool OnPush(Vector2Int direction)
    {
        if (direction.y == 0 && Mathf.Abs(direction.x) == 1)
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
            CommandExecutor.AddBarrelOnWater(false, position);
        }
    }
}
