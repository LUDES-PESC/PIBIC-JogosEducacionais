using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : MonoBehaviour {
    public static Dictionary<Vector2Int, Obstacle> obstacles = new Dictionary<Vector2Int,Obstacle>();

    public static void LoadMap()
    {
        obstacles = new Dictionary<Vector2Int, Obstacle>();
        var obs = FindObjectsOfType<Obstacle>();
        foreach(Obstacle o in obs)
        {
            o.ResetPosition();
            obstacles.Add(o.position, o);
        }
    }
    public static Obstacle ObstacleIn(Vector2Int position)
    {
        if (obstacles.ContainsKey(position))
            return obstacles[position];
        return null;
    }
}
