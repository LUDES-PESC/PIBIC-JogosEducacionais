using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelMap {
    public Vector2 initialPosition;
    public List<Vector2> borders = new List<Vector2>();
    public List<Vector2> ground = new List<Vector2>();
    public List<Vector2> woodenBox = new List<Vector2>();
    public List<Vector2> verticalBarrel = new List<Vector2>();
    public List<Vector2> horizonalBarrel = new List<Vector2>();
    public List<Vector2> verticalRaft = new List<Vector2>();
    public List<Vector2> horizontalRaft = new List<Vector2>();
    public List<Vector2> treasures = new List<Vector2>();
    public Vector2 bigTreasure;
}
