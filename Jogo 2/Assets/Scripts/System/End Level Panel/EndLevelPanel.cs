using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EndLevelPanel : MonoBehaviour {
    [SerializeField] private TMP_Text bigTreasure;
    [SerializeField] private TMP_Text smallTreasures;
    [SerializeField] private TMP_Text steps;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private RectTransform panel;

    public void OpenPanel(TreasureInfo info, int steps)
    {
        CameraMovement.canBeMoved++;
        group.DOFade(1, 0.5f);
        group.interactable = true;
        group.blocksRaycasts = true;
        panel.DOAnchorPosY(-680, 0);
        panel.DOAnchorPosY(40, 0.5f);

        var level = LevelManager.me.levels.levels[MemoryCard.GetSelectedLevel()];
        bigTreasure.text = "1/1";
        smallTreasures.text = info.littleTreasures.Count + "/" + level.map.treasures.Count;
        this.steps.text = steps + "/" + level.maxSteps;

        SaveProgress(info, steps);
    }
    public void TryAgain()
    {
        CameraMovement.canBeMoved--;
        group.DOFade(0, 0.5f);
        group.interactable = false;
        group.blocksRaycasts = false;
        panel.DOAnchorPosY(-680, 0.5f);
    }
    public void OpenMenu()
    {
        LoadingScreen.LoadScreen("MainMenu");
        CameraMovement.canBeMoved = 0;
    }
    public void SaveProgress(TreasureInfo info, int steps)
    {
        var memory = MemoryCard.Load();
        if (!memory.levels[MemoryCard.GetSelectedLevel()].bigTreasure)
            memory.levels.Add(new LevelProgress());
        memory.levels[MemoryCard.GetSelectedLevel()].bigTreasure = info.bigTreasure;
        memory.levels[MemoryCard.GetSelectedLevel()].steps = steps < memory.levels[MemoryCard.GetSelectedLevel()].steps ?
            steps : memory.levels[MemoryCard.GetSelectedLevel()].steps;
        memory.levels[MemoryCard.GetSelectedLevel()].treasures = info.littleTreasures.Count > memory.levels[MemoryCard.GetSelectedLevel()].treasures ?
            info.littleTreasures.Count : memory.levels[MemoryCard.GetSelectedLevel()].treasures;
        memory.Save();
    }
}
