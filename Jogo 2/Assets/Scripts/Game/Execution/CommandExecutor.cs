﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour {
    //temp
    private const int MAP_SIZE = 4;

    public static CommandExecutor executor;
    public Player player;
    
    public GroundType[,] groundMap = new GroundType[MAP_SIZE, MAP_SIZE];

    private void Start()
    {
        executor = this;
    }
    public void Execute(List<Command> commands)
    {
        StartCoroutine(ExecuteCoroutine(commands));
    }
    private IEnumerator ExecuteCoroutine(List<Command> commands)
    {
        ObstacleMap.LoadMap();
        TreasureMap.LoadMap();
        player.SetPosition(0, 0);
        player.SetLook(1, 0);
        for (int i = 0; i < commands.Count; i++)
        {
            yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnStart();

            commands[i].Execute(player);

            foreach (var e in ObstacleMap.obstacles)
                e.Value.TurnUpdate();
        }
    }
}
