using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [HideInInspector] public int levelIndex;
    [SerializeField] private TMP_Text index;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image thumbnail;

    [SerializeField] private TMP_Text bigTreasureValue;
    [SerializeField] private TMP_Text treasuresValue;
    [SerializeField] private TMP_Text maxStepsValue;

    public void SetData(LevelData levelData, LevelProgress levelProgress, int index)
    {
        this.levelIndex = index;
        this.index.text = (index + 1).ToString();
        this.title.text = levelData.title;
        this.thumbnail.sprite = levelData.thumbnail;

        this.bigTreasureValue.text = levelProgress.bigTreasure ? "1/1" : "-/0";
        this.treasuresValue.text = levelProgress.bigTreasure ?
            levelProgress.treasures + "/" + levelData.map.treasures.Count
            : "-/" + levelData.map.treasures.Count;
        this.maxStepsValue.text = levelProgress.bigTreasure ?
            levelProgress.steps + "/" + levelData.maxSteps
            : "-/" + levelData.maxSteps;
    }
    public void OpenLevel()
    {
        MemoryCard.SetSelectedLevel(levelIndex);
        print("Opening Level: " + levelIndex);
        LoadingScreen.LoadScreen("SampleScene");
    }
}

