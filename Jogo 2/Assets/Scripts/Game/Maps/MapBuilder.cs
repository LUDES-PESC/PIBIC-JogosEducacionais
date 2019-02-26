using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour {
    [SerializeField] private Tilemap map;
    [SerializeField] private Transform obstacleRoot;
    [Header("Tiles")]
    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase groundTile;
    [Header("Prefabs")]
    public GameObject verticalRaft;
    public GameObject horizontalRaft;
    [SerializeField] private GameObject horizontalBarrel;
    [SerializeField] private GameObject verticalBarrel;
    [SerializeField] private GameObject woodenBox;
    [SerializeField] private GameObject cannon;
    [Header("LevelMaps")]
    [SerializeField] private List<TextAsset> levels;

    public void BuildMap()
    {
        Clear();
        ObstacleMap.ResetMap();
        LevelMap levelMap = LoadMap(MemoryCard.GetSelectedLevel());
        CommandExecutor.executor.player.SetPosition((int)levelMap.initialPosition.x, (int)levelMap.initialPosition.y);
        DrawFloor(groundTile, levelMap.ground);
        DrawFloor(waterTile, levelMap.borders);
        CreateObstacle(horizontalBarrel, levelMap.horizonalBarrel);
        CreateObstacle(verticalBarrel, levelMap.verticalBarrel);
        CreateObstacle(woodenBox, levelMap.woodenBox);
        CreateObstacle(verticalRaft, levelMap.verticalRaft);
        CreateObstacle(horizontalRaft, levelMap.horizontalRaft);
    }
    private void DrawFloor(TileBase tile, List<Vector2> positions)
    {
        foreach(var pos in positions)
        {
            var p = new Vector3Int((int)pos.x, (int)pos.y, 0);
            map.SetTile(p, tile);
        }
    }
    public void CreateObstacle(GameObject objectPrefab, List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            var obstacle = Instantiate(objectPrefab, obstacleRoot).GetComponent<Obstacle>();
            obstacle.transform.position = new Vector3(pos.x, pos.y, 0) * Globals.TILE_SIZE + new Vector3(0.5f, 0.5f, 0f);
            obstacle.GetPosition();
            ObstacleMap.AddObstacle(obstacle);
        }
    }
    private void Clear()
    {
        map.ClearAllTiles();
        for(int i = obstacleRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(obstacleRoot.GetChild(i).gameObject);
        }
    }
    public LevelMap LoadMap(int index)
    {
        return JsonUtility.FromJson<LevelMap>(levels[index].text);
    }
}
