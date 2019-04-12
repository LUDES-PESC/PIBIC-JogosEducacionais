using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelData")]
public class LevelData : ScriptableObject {
    public string title;
    public Sprite thumbnail;
    public LevelMap map;
    public int maxSteps;
}
