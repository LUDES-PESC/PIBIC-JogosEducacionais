using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/WorldData")]
public class WorldData : ScriptableObject {
    public string worldName;
    public Sprite map;
    public List<LevelData> levels;
    public List<Vector2> levelPositions;
}
