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
    public void Move()
    {
        position += lookDirection;
        var targetPosition = new Vector3(position.x, position.y, 0);
        transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
    }
    public IEnumerator Walk()
    {
        isWaiting = false;
        if (FindObjectOfType<GroundMap>().IsBorderTile(position + lookDirection))
            yield return null;
        if(ObstacleMap.ObstacleIn(position + lookDirection) == null)
        {
            if(FindObjectOfType<GroundMap>().IsSandTile(position + lookDirection))
            {
                position += lookDirection;
                var targetPosition = new Vector3(position.x, position.y, 0);
                transform.DOMove(targetPosition + new Vector3(0.5f, 0.5f, 0) * Globals.TILE_SIZE, Globals.TIME_BETWEEN_TURNS);
            }
            else
            {
                ErrorHandling.AddError(new Error(CommandExecutor.currentLine, "WALKING ON WATER"));
                print("ERROR: WALKING ON WATER");
            }
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
                ErrorHandling.AddError(new Error(CommandExecutor.currentLine, "PUSHING OBSTACLE"));
                print("ERROR: PUSHING OBSTACLE");
            }
        }
    }
    public IEnumerator Wait()
    {
        ConsoleLine.WriteLine("WAIT");
        isWaiting = true;
        yield return null;
    }
    public IEnumerator Shoot()
    {
        isWaiting = false;
        Vector2Int bulletPosition = position;
        for(int i = 0; i < Globals.BULLET_RANGE; i++)
        {
            bulletPosition += lookDirection;
            var obstacle = ObstacleMap.ObstacleIn(bulletPosition);
            if (obstacle != null && obstacle.GetComponent<IBulletTarget>() != null)
            {
                print("Bullet Start at <" + position.x + "," + position.y +
                    "> until reaches <" + bulletPosition.x + "," + bulletPosition.y + ">");
                break;
            }
        }
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return bullet.GetComponent<Bullet>().Shoot(position, bulletPosition);
    }
    public IEnumerator Look(Vector2Int direction)
    {
        isWaiting = false;
        lookDirection = direction;
        GetComponent<PlayerAnimation>().ChangeAnimation(direction);
        yield return null;
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
    public IEnumerator Dig()
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
            ErrorHandling.AddError(new Error(CommandExecutor.currentLine, "DIGGING IN WRONG PLACE"));
            print("ERROR: DIGGING IN WRONG PLACE");
        }
        yield return null;
    }
}