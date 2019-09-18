using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MemoryCard {
    private static string baseName = "/memoryCard_test.txt";

    public int selectedWorld = 0;
    public int selectedLevel = 0;
    public List<WorldProgress> levels = new List<WorldProgress>();

    public static MemoryCard CreateInitializedMemoryCard()
    {
        var mc = new MemoryCard();
        mc.levels.Add(new WorldProgress());
        mc.levels[0].levels.Add(new LevelProgress());
        return mc;
    }
    public static MemoryCard Load()
    {
        string path = Application.persistentDataPath + baseName;
        CreateFileIfDontExist(path, JsonUtility.ToJson(CreateInitializedMemoryCard()));
        string content = File.ReadAllText(path);
        return JsonUtility.FromJson<MemoryCard>(content);
    }
    public void Save()
    {
        string path = Application.persistentDataPath + baseName;
        string content = JsonUtility.ToJson(this);
        CreateFileIfDontExist(path, content);
        File.WriteAllText(path, content);
    }
    public static int GetSelectedLevel()
    {
        return Load().selectedLevel;
    }
    public static void SetSelectedLevel(int index)
    {
        var memoryCard = Load();
        memoryCard.selectedLevel = index;
        memoryCard.Save();
    }
    public static int GetSelectedWorld()
    {
        return Load().selectedWorld;
    }
    public static void SetSelectedWorld(int index)
    {
        var memoryCard = Load();
        memoryCard.selectedWorld = index;
        memoryCard.Save();
    }
    public static void CreateFileIfDontExist(string path, string content)
    {
        if (!File.Exists(path))
        {
            Debug.Log(path);
            File.WriteAllText(path, content);
        }
    }
    public int GetLastUnlockedLevel(int world)
    {
        var worldProgress = levels[world];
        for(int i = 0; i < worldProgress.levels.Count; i++)
        {
            LevelProgress lp = worldProgress.levels[i];
            if (lp.treasures == 0)
                return i;
        }
        return -1;
    }
    public int GetLastUnlockedWorld()
    {
        return levels.Count - 1;
    }
}
[System.Serializable]
public class LevelProgress
{
    public int treasures = 0;
    public int gems = 0;
    public int steps = 100;

    public List<bool> GetStars(LevelData data)
    {
        var list = new List<bool>();
        list.Add(treasures == data.map.bigTreasures.Count);
        list.Add(gems == data.map.treasures.Count);
        list.Add(steps <= data.maxSteps);
        return list;
    }
}
[System.Serializable]
public class WorldProgress
{
    public List<LevelProgress> levels = new List<LevelProgress>();
}
