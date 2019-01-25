using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour {
    //temp
    private const int MAP_SIZE = 4;

    public static CommandExecutor executor;
    public Player player;
    public MapBuilder mapBuilder;
    
    public GroundType[,] groundMap = new GroundType[MAP_SIZE, MAP_SIZE];

    private static List<Vector2> horizontalBarrelsOnWater;
    private static List<Vector2> verticalBarrelsOnWater;

    private void Start()
    {
        executor = this;
        mapBuilder.BuildMap();
        player.SetLook(1, 0);
    }
    public void Execute(List<Command> commands)
    {
        StartCoroutine(ExecuteCoroutine(commands));
    }
    private IEnumerator ExecuteCoroutine(List<Command> commands)
    {
        mapBuilder.BuildMap();
        TreasureMap.LoadMap();
        player.SetLook(1, 0);
        for (int i = 0; i < commands.Count; i++)
        {
            horizontalBarrelsOnWater = new List<Vector2>();
            verticalBarrelsOnWater = new List<Vector2>();

            yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnStart();

            commands[i].Execute(player);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnUpdate();

            ObstacleMap.FixDictionary();
            SwapBarrelForRaft();
        }
    }
    public static void AddBarrelOnWater(bool vertical, Vector2 position)
    {
        if (vertical)
            verticalBarrelsOnWater.Add(position);
        else
            horizontalBarrelsOnWater.Add(position);
    }
    private void SwapBarrelForRaft()
    {
        var mapBuilder = FindObjectOfType<MapBuilder>();

        foreach (var o in horizontalBarrelsOnWater)
        {
            GameObject obj = ObstacleMap.ObstacleIn(new Vector2Int((int)o.x, (int)o.y)).gameObject;
            ObstacleMap.obstacles.Remove(new Vector2Int((int)o.x, (int)o.y));
            Destroy(obj);
        }
        foreach (var o in verticalBarrelsOnWater)
        {
            GameObject obj = ObstacleMap.ObstacleIn(new Vector2Int((int)o.x, (int)o.y)).gameObject;
            ObstacleMap.obstacles.Remove(new Vector2Int((int)o.x, (int)o.y));
            Destroy(obj);
        }

        mapBuilder.CreateObstacle(mapBuilder.horizontalRaft, horizontalBarrelsOnWater);
        mapBuilder.CreateObstacle(mapBuilder.verticalRaft, verticalBarrelsOnWater);
        //ObstacleMap.FixDictionary();
    }
}
