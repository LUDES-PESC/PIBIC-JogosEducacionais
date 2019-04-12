using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour {
    [SerializeField] private Image background;
    [SerializeField] private RectTransform box;
    [SerializeField] private List<RectTransform> sections;
    [SerializeField] private TMP_Text stepsText;
    [SerializeField] private TMP_Text treasureText;

	public void OpenPanel(Result info)
    {
        StartCoroutine(OpenPanelAnimation(info));
    }
    public IEnumerator OpenPanelAnimation(Result info){
        background.DOFade(0.5f, 1f);
        background.raycastTarget = true;
        yield return new WaitForSeconds(2f);
        box.DOAnchorPosY(360, 0.25f);
        sections[0].DOAnchorPosX(-1000f, 0);
        sections[1].DOAnchorPosX(-1000f, 0);
        stepsText.text = info.steps + "/" + info.maxSteps;
        treasureText.text = info.littleTreasures + "/" + info.maxLittleTreasures;
        yield return new WaitForSeconds(0.25f);
        sections[0].DOAnchorPosX(0, 0.25f);
        yield return new WaitForSeconds(0.25f);
        sections[1].DOAnchorPosX(0, 0.35f);
        Save(info);
    }
    public void Continue()
    {
        MemoryCard.SetSelectedLevel(MemoryCard.GetSelectedLevel() + 1);
        LoadingScreen.LoadScreen("SampleScene");
    }
    private void Save(Result info)
    {
        var memory = MemoryCard.Load();
        if (!memory.levels[MemoryCard.GetSelectedLevel()].bigTreasure)
            memory.levels.Add(new LevelProgress());
        memory.levels[MemoryCard.GetSelectedLevel()].bigTreasure = info.bigTreasure;
        memory.levels[MemoryCard.GetSelectedLevel()].steps = info.steps < memory.levels[MemoryCard.GetSelectedLevel()].steps ?
            info.steps : memory.levels[MemoryCard.GetSelectedLevel()].steps;
        memory.levels[MemoryCard.GetSelectedLevel()].treasures = info.littleTreasures > memory.levels[MemoryCard.GetSelectedLevel()].treasures ?
            info.littleTreasures : memory.levels[MemoryCard.GetSelectedLevel()].treasures;
        memory.Save();
    }
}
