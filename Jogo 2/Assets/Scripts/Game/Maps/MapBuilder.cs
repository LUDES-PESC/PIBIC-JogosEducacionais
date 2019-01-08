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
    [SerializeField] private GameObject horizontalBarrel;
    [SerializeField] private GameObject verticalBarrel;
    [SerializeField] private GameObject woodenBox;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject verticalRaft;
    [SerializeField] private GameObject horizontalRaft;

    public void BuildMap()
    {
        Clear();
        ObstacleMap.ResetMap();
        LevelMap levelMap = LoadMap(0);
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
    private void CreateObstacle(GameObject objectPrefab, List<Vector2> positions)
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
    private LevelMap LoadMap(int index)
    {
        var lMap = new LevelMap();
        lMap.initialPosition = new Vector2(0, 3);
        lMap.ground.Add(new Vector2(0, 0));
        lMap.ground.Add(new Vector2(0, 1));
        lMap.ground.Add(new Vector2(0, 2));
        lMap.ground.Add(new Vector2(0, 3));
        lMap.ground.Add(new Vector2(1, 0));
        lMap.ground.Add(new Vector2(1, 1));
        lMap.ground.Add(new Vector2(1, 2));
        lMap.ground.Add(new Vector2(1, 3));
        lMap.ground.Add(new Vector2(3, 0));
        lMap.ground.Add(new Vector2(3, 1));
        lMap.ground.Add(new Vector2(3, 2));
        lMap.ground.Add(new Vector2(3, 3));
        lMap.borders.Add(new Vector2(-1, 0));
        lMap.borders.Add(new Vector2(-1, 1));
        lMap.borders.Add(new Vector2(-1, 2));
        lMap.borders.Add(new Vector2(-1, 3));
        lMap.borders.Add(new Vector2(-1, -1));
        lMap.borders.Add(new Vector2(0, -1));
        lMap.borders.Add(new Vector2(1, -1));
        lMap.borders.Add(new Vector2(2, -1));
        lMap.borders.Add(new Vector2(3, -1));
        lMap.borders.Add(new Vector2(-1, 4));
        lMap.borders.Add(new Vector2(0, 4));
        lMap.borders.Add(new Vector2(1, 4));
        lMap.borders.Add(new Vector2(2, 4));
        lMap.borders.Add(new Vector2(3, 4));
        lMap.borders.Add(new Vector2(4, 4));
        lMap.borders.Add(new Vector2(4, 0));
        lMap.borders.Add(new Vector2(4, 1));
        lMap.borders.Add(new Vector2(4, 2));
        lMap.borders.Add(new Vector2(4, 3));
        lMap.borders.Add(new Vector2(4, -1));
        lMap.horizonalBarrel.Add(new Vector2(1, 1));
        lMap.woodenBox.Add(new Vector2(1, 0));
        lMap.verticalRaft.Add(new Vector2(2, 3));
        return lMap;
    }
}
