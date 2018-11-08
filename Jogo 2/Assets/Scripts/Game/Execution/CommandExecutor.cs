using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour {
    //temp
    private const int MAP_SIZE = 4;

    public static CommandExecutor executor;
    public Player player;
    public GridElement[,] grid = new GridElement[MAP_SIZE, MAP_SIZE];
    public GroundType[,] groundMap = new GroundType[MAP_SIZE, MAP_SIZE];

    private void Start()
    {
        executor = this;
    }
    public void Execute(List<Command> commands)
    {
        player.position = Vector2.zero;
        LoadMap();
        int turnQuantity = commands.Count;
        for(int i = 0; i < turnQuantity; i++)
        {
            commands[i].Execute(player);
            foreach(var e in grid)
            {
                var tb = e as ITurnBased;
                if (tb != null)
                    tb.TurnUpdate();
            }
        }
    }
    public void LoadMap()
    {
        //INIT GROUND
        for(int i = 0; i < MAP_SIZE; i++)
        {
            for (int j = 0; j < MAP_SIZE; j++)
            {
                if(i != 1)
                    groundMap[i, j] = GroundType.SAND;
                else
                    groundMap[i, j] = GroundType.WATER;
            }
        }
        grid[0, 1] = new WoodenBox(0, 1);
        grid[1, 0] = new Raft(1, 0);
    }
}
