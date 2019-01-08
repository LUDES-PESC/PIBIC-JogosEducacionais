using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {
    public Vector2Int position;
    public Vector2Int lookDirection;

    public GameObject bulletPrefab;

    public bool isWaiting;
    public bool flag;

    public void SetPosition(int x, int y)
    {
        position.x = x;
        position.y = y;
        var targetPosition = new Vector3(position.x, position.y, 0);
        transform.position = (targetPosition + new Vector3(0.5f, 0.5f, 0)) * Globals.TILE_SIZE;
    }
    public void SetLook(int x, int y)
    {
        lookDirection.x = x;
        lookDirection.y = y;
        GetComponent<PlayerAnimation>().ChangeAnimation(lookDirection);
    }
    public void Walk()
    {
        isWaiting = false;
        if (FindObjectOfType<GroundMap>().IsBorderTile(position + lookDirection))
            return;
        if(ObstacleMap.ObstacleIn(position + lookDirection) == null)
        {
            position += lookDirection;
            var targetPosition = new Vector3(position.x, position.y, 0);
            transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
        }
        else
        {
            if(ObstacleMap.ObstacleIn(position + lookDirection).OnPush(lookDirection))
            {
                position += lookDirection;
                var targetPosition = new Vector3(position.x, position.y, 0);
                transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
            }
            else
            {
                ConsoleLine.WriteLine("ERROR: PUSHING OBSTACLE");
            }
        }
    }
    public void Wait()
    {
        ConsoleLine.WriteLine("WAIT");
        isWaiting = true;
    }
    public void Shoot()
    {
        isWaiting = false;
        Vector2Int bulletPosition = position;
        for(int i = 0; i < Globals.BULLET_RANGE; i++)
        {
            bulletPosition += lookDirection;
            var obstacle = ObstacleMap.ObstacleIn(bulletPosition);
            if (obstacle != null && obstacle.GetComponent<IBulletTarget>() != null)
            {
                ConsoleLine.WriteLine("Bullet Start at <" + position.x + "," + position.y +
                    "> until reaches <" + bulletPosition.x + "," + bulletPosition.y + ">");
                break;
            }
        }
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        StartCoroutine(bullet.GetComponent<Bullet>().Shoot(position, bulletPosition));
    }
    public void Look(Vector2Int direction)
    {
        isWaiting = false;
        lookDirection = direction;
        GetComponent<PlayerAnimation>().ChangeAnimation(direction);
    }
    public void RiseFlag()
    {
        isWaiting = false;
        flag = true;
        ConsoleLine.WriteLine("FLAG: " + flag);
    }
    public bool IsAbove(Vector2Int position)
    {
        print(this.position + "/" + position);
        return this.position == position;
    }
    public void Dig()
    {
        ConsoleLine.WriteLine("DIG: " + position);
        GetComponent<PlayerAnimation>().DigAnimation();
        var treasure = TreasureMap.TreasureIn(position);
        if (treasure != null && treasure.found == false)
        {
            ConsoleLine.WriteLine("FOUND SOMETHING IN " + position);
            treasure.Find();
        }
        else
        {
            ConsoleLine.WriteLine("DIDN'T FOUND ANYTHING IN " + position);
        }
    }
}

