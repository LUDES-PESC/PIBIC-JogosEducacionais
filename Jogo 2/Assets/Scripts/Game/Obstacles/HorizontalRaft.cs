﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRaft : Obstacle{
    public Vector2Int direction = Vector2Int.right;

    public override void TurnUpdate()
    {
        if (CanMoveForward())
        {
            if (CommandExecutor.executor.player.isWaiting && position == CommandExecutor.executor.player.position)
            {
                Move(direction);
                CommandExecutor.executor.player.Look(direction);
                CommandExecutor.executor.player.Walk();
            }
        }
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
            targetPosition += new Vector3Int(direction.x, direction.y, 0) * 2;
            canMove = !(groundMap.IsSandTile(targetPosition) || groundMap.IsBorderTile(pos));
        }
        return canMove;
    }
    public override bool OnPush(Vector2Int direction)
    {
        return true;
    }
}
