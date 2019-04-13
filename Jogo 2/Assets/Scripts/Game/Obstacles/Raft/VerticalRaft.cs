using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRaft : Obstacle {
    public Vector2Int direction = Vector2Int.up;

    public override IEnumerator TurnUpdate()
    {
        if (CanMoveForward())
        {
            if (CommandExecutor.executor.player.isWaiting && position == CommandExecutor.executor.player.position)
            {
                Move(direction);
                yield return CommandExecutor.executor.player.Look(direction);
                yield return CommandExecutor.executor.player.Move();
            }
        }
        else
            yield return null;
    }
    public bool CanMoveForward()
    {
        var pos = position + direction;
        Vector3Int targetPosition = new Vector3Int(pos.x, pos.y, 0);
        var groundMap = FindObjectOfType<GroundMap>();
        bool canMove = !(groundMap.IsSandTile(targetPosition) || groundMap.IsBorderTile(pos));
        if (!canMove)
        {
            direction *= -1;
            targetPosition += new Vector3Int(direction.x,direction.y, 0) * 2;
            canMove = !(groundMap.IsSandTile(targetPosition) || groundMap.IsBorderTile(pos));
        }
        return canMove;
    }
    public override bool OnPush(Vector2Int direction)
    {
        return true;
    }
}
