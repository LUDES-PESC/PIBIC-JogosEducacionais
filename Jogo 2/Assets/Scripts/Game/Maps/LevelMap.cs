using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelMap {
    private static string baseName = "/level_";

    public string levelName;
    public int maxSteps;
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

    public static LevelMap LoadLevel(int index)
    {
        string path = Application.persistentDataPath + baseName + index.ToString() + ".txt";
        Debug.Log(path);
        return JsonUtility.FromJson<LevelMap>(File.ReadAllText(path));
    }
    public void SaveLevel(int index)
    {
        string path = Application.persistentDataPath + baseName + index.ToString() + ".txt";
        string content = JsonUtility.ToJson(this, true);
        CreateFileIfDontExist(path, content);
        File.WriteAllText(path, content);
    }
    public static void CreateFileIfDontExist(string path, string content)
    {
        if (!File.Exists(path))
        {
            Debug.Log(path);
            File.WriteAllText(path, content);
        }
    }
}
