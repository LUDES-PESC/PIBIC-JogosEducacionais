using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compiler : MonoBehaviour {

    public void Run()
    {
        FindObjectOfType<MapBuilder>().BuildMap();
        List<Program> programs = new List<Program>();
        foreach (var b in FindObjectsOfType<BeginBlock>())
        {
            var prog = b.GetProgram();
            if(prog != null)
                programs.Add(prog);
        }
        StartCoroutine(Execution(programs));
    }
    private IEnumerator Execution(List<Program> programs)
    {
        //yield return null;
        for(int i = 0; i < LevelManager.beginBlocks.Count; i++)
        {
            var p = MapBuilder.LoadMap(0).initialPosition[i];
            LevelManager.beginBlocks[i].player.SetPosition((int)p.x, (int)p.y);
            LevelManager.beginBlocks[i].player.SetLook(1, 0);
        }
        while (programs.Count > 0)
        {
            if (AllFlagsRised())
            {
                foreach (var p in FindObjectsOfType<Player>())
                    yield return p.DropFlag();
            }

            foreach (var e in ObstacleMap.obstacles)
                yield return e.Value.TurnStart();

            int playerIndex = GetRandomPlayerWithFlagNotRised(programs);
            CameraMovement.MoveTo(programs[playerIndex].player.position);
            yield return programs[playerIndex].blocks[0].Execute(programs[playerIndex].player);
            programs[playerIndex].blocks.Remove(programs[playerIndex].blocks[0]);
            if (programs[playerIndex].blocks.Count == 0)
                programs.Remove(programs[playerIndex]);

            foreach (var e in ObstacleMap.obstacles)
                yield return e.Value.TurnUpdate();

            yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);
        }
        yield return null;
    }
    private int GetRandomPlayerWithFlagNotRised(List<Program> programs)
    {
        List<int> possible = new List<int>();
        for(int i = 0; i < programs.Count; i++)
        {
            if (!programs[i].player.flag)
                possible.Add(i);
        }
        return possible[Random.Range(0, possible.Count)];
    }
    private bool AllFlagsRised()
    {
        var players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].flag)
                return false;
        }
        return true;
    }
}
public class Program
{
    public Player player;
    public List<Block> blocks;
}
