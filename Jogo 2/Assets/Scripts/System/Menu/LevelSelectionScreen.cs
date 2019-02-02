using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectionScreen : Screen {
    private const int levelQuantity = 5;
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
        for (int i = 0; i < levelQuantity; i++)
        {
            var go = Instantiate(levelButtonPrefab, buttonRoot);
            go.GetComponent<LevelButton>().SetIndex(i);
            go.transform.localScale = new Vector3(0, 0, 0);
            go.transform.DOScale(1, 0.25f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
