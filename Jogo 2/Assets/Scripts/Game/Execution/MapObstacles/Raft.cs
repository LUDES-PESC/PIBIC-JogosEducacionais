using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : GridElement, ITurnBased, IMovable
{
    public Vector2 direction = Vector2.up;

    public Raft(int x, int y)
    {
        position.x = x;
        position.y = y;
    }
    public void TurnUpdate()
    {
        if (HasWallOn(position, direction, 1))
            direction *= -1;
        if (CommandExecutor.executor.player.IsAbove(position))
            Move(direction);
    }
    public void Move(Vector2 direction)
    {
        if (CommandExecutor.executor.player.isWaiting){
            ConsoleLine.WriteLine("MOVING RAFT");
            ConsoleLine.WriteLine("PLAYER IS WAITING. MOVING PLAYER ALONG");
            CommandExecutor.executor.player.Look(direction);
            CommandExecutor.executor.player.Walk();
            position += direction;
        }
    }
    private bool HasWallOn(Vector2 position, Vector2 direction, int distance)
    {
        for(int i = 0; i < distance; i++)
        {
            position += direction;
            Debug.Log(position);
            if (CommandExecutor.executor.groundMap[(int)position.x, (int)position.y] != GroundType.WATER)
            {
                ConsoleLine.WriteLine("WALL IN FRONT OF RAFT PATH. TURNING BACK.");
                return true;
            }
        }
        return false;
    }
}
