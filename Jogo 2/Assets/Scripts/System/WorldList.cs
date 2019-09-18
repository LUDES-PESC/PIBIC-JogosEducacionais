using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/WorldList")]
public class WorldList : ScriptableObject {
    public List<WorldData> worlds;

    public LevelData GetLevel(int worldIndex, int levelIndex)
    {
        return worlds[worldIndex].levels[levelIndex];
    }
}
