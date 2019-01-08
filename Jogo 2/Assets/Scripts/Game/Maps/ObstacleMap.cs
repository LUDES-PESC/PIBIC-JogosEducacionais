using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : MonoBehaviour {
    public static Dictionary<Vector2Int, Obstacle> obstacles = new Dictionary<Vector2Int,Obstacle>();

    public static void AddObstacle(Obstacle obstacle)
    {
        obstacles.Add(obstacle.position, obstacle);
    }
    public static void ResetMap()
    {
        obstacles = new Dictionary<Vector2Int, Obstacle>();
    }
    public static Obstacle ObstacleIn(Vector2Int position)
    {
        if (obstacles.ContainsKey(position))
            return obstacles[position];
        return null;
    }
}
