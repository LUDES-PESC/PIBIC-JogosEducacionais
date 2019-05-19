using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static List<BeginBlock> beginBlocks = new List<BeginBlock>();

    [Header("Managers")]
    [SerializeField] private MapBuilder mapBuilder;
    [SerializeField] private ObstacleMap obstacleMap;
    [SerializeField] private GroundMap groundMap;

    [Header("Prefabs")]
    [SerializeField] private GameObject beginBlock;
    [SerializeField] private GameObject player;

    [Header("Misc")]
    [SerializeField] private Transform beginBlockRoot;
    [SerializeField] private List<Color> playerColors;

    private void Start () {
        MemoryCard.SetSelectedLevel(0);
        CreatePlayer();
        mapBuilder.BuildMap();
        TreasureMap.LoadMap();
    }
    private void CreatePlayer()
    {
        beginBlocks = new List<BeginBlock>();
        LevelMap level = MapBuilder.LoadMap(MemoryCard.GetSelectedLevel());
        for(int i = 0; i < level.initialPosition.Count;i++)
        {
            var p = Instantiate(player).GetComponent<Player>();
            p.SetPosition((int)level.initialPosition[i].x, (int)level.initialPosition[i].y);
            p.SetColor(playerColors[i]);
            var b = Instantiate(beginBlock, beginBlockRoot).GetComponent<BeginBlock>();
            b.player = p;
            b.SetColor(playerColors[i]);
            beginBlocks.Add(b);
        }
    }
    public void BackToMenu()
    {
        LoadingScreen.LoadScreen("MainMenu");
    }
}
