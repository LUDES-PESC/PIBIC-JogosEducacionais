using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public abstract class Obstacle : MonoBehaviour {
    public Vector2Int initialPosition;
    public Vector2Int position;

    public void Start()
    {
        position = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
        initialPosition = position;
    }
    public void ResetPosition()
    {
        position = initialPosition;
        transform.DOMove(new Vector3(initialPosition.x, initialPosition.y, 0) + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, 0);
    }
    public bool Move(Vector2Int direction)
    {
        var target = position + direction;
        if(ObstacleMap.ObstacleIn(target) == null)
        {
            position = target;
            transform.DOMove(new Vector3(target.x, target.y, 0) + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
            ObstacleMap.obstacles.Remove(target - direction);
            ObstacleMap.obstacles.Add(target, this);
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void TurnStart() { }
    public virtual void TurnUpdate() { }
    public virtual bool OnPush(Vector2Int direction) { return false; }
    public virtual void OnPlayerEnter() { }
    public virtual void OnPlayerStay() { }
    public virtual void OnPlayerExit() { }
}
