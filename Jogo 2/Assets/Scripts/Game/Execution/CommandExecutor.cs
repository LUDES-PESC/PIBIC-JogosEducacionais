using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour {
    //temp
    private const int MAP_SIZE = 4;

    public static CommandExecutor executor;
    public static int currentLine = 0;

    public Player player;
    public MapBuilder mapBuilder;
    public TreasureInventory inventory;
    public GroundType[,] groundMap = new GroundType[MAP_SIZE, MAP_SIZE];

    private static List<Vector2> horizontalBarrelsOnWater;
    private static List<Vector2> verticalBarrelsOnWater;

    private void Start()
    {
        executor = this;
        mapBuilder.BuildMap();
        player.SetLook(1, 0);

        TreasureMap.LoadMap();
        inventory.InitializeTreasureIndicator();
    }
    public void Execute(List<Command> commands)
    {
        StartCoroutine(ExecuteCoroutine(commands));
    }
    private IEnumerator ExecuteCoroutine(List<Command> commands)
    {
        ErrorHandling.ResetError();
        mapBuilder.BuildMap();
        yield return null;
        TreasureMap.LoadMap();
        inventory.Reset();
        player.SetLook(1, 0);
        for (currentLine = 0; currentLine < commands.Count; currentLine++)
        {
            horizontalBarrelsOnWater = new List<Vector2>();
            verticalBarrelsOnWater = new List<Vector2>();

            yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnStart();

            yield return commands[currentLine].Execute(player);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnUpdate();

            ObstacleMap.FixDictionary();
            SwapBarrelForRaft();

            if (ErrorHandling.errors.Count > 0)
            {
                FindObjectOfType<CommandPanel>().SetErrorLine(ErrorHandling.errors[0].commandLine);
                yield break;
            }
        }
        if (inventory.GetResult().bigTreasure)
        {
            Result r = inventory.GetResult();
            r.steps = commands.Count;
            r.maxSteps = FindObjectOfType<MapBuilder>().levels.levels[MemoryCard.GetSelectedLevel()].maxSteps;
            FindObjectOfType<EndPanel>().OpenPanel(r);
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
    }
}
