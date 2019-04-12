using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectionScreen : Screen {
    [SerializeField] private List<LevelData> data;
    [SerializeField] private Transform buttonRoot;
    [SerializeField] private GameObject levelButtonPrefab;

    public override void OnShow()
    {
        StartCoroutine(ShowLevelButtons());
    }
    public override void OnHide()
    {
        for (int i = buttonRoot.childCount-1; i >= 0; i--)
        {
            Destroy(buttonRoot.GetChild(i).gameObject);
        }
    }
    private IEnumerator ShowLevelButtons()
    {
        var memory = MemoryCard.Load();
        if (memory.levels.Count == 0)
        {
            memory.levels.Add(new LevelProgress());
            memory.Save();
        }
        for (int i = LastUnlockedLevel(memory.levels)-1; i >= 0; i--)
        {
            var go = Instantiate(levelButtonPrefab, buttonRoot);
            go.GetComponent<LevelButton>().SetData(data[i], memory.levels[i], i);
            go.transform.localScale = new Vector3(0, 0, 0);
            go.transform.DOScale(1, 0.25f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private int LastUnlockedLevel(List<LevelProgress> progress)
    {
        return Mathf.Clamp(progress.Count, 1, data.Count);
    }
}
