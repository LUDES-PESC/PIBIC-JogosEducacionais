using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MemoryCard : MonoBehaviour {
    private static string baseName = "/data_";
    private static string selectedLevelName = "SELECTEDLEVEL.txt";

    public static int GetSelectedLevel()
    {
        string path = Application.persistentDataPath + baseName + selectedLevelName;
        return JsonUtility.FromJson<SelectedLevel>(File.ReadAllText(path)).selectedLevel;
    }
    public static void SetSelectedLevel(int index)
    {
        string path = Application.persistentDataPath + baseName + selectedLevelName;
        string content = JsonUtility.ToJson(new SelectedLevel(index));
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
    class SelectedLevel
    {
        public int selectedLevel;
        public SelectedLevel(int level)
        {
            selectedLevel = level;
        }
    }
}
