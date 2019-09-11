using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelScreen : Screen
{
    [SerializeField] private WorldMap worldMap;
    [SerializeField] private RectTransform title;
    [SerializeField] private RectTransform prevButton;
    [SerializeField] private RectTransform nextButton;
    [SerializeField] private RectTransform backButton;

    public override IEnumerator Open(float duration = 0.25F)
    {
        prevButton.DOAnchorPosX(80, duration);
        nextButton.DOAnchorPosX(-80, duration);
        backButton.DOAnchorPosY(95, duration);
        title.DOAnchorPosY(-80, duration);
        yield return worldMap.Close(0);
        yield return worldMap.Open(0);
    }
    public override IEnumerator Close(float duration = 0.25F)
    {
        prevButton.DOAnchorPosX(-80, duration);
        nextButton.DOAnchorPosX(80, duration);
        backButton.DOAnchorPosY(-95, duration);
        title.DOAnchorPosY(80, duration);
        yield return worldMap.Close(duration);
    }
    public override void OnClose()
    {
    }
    public override void OnOpen()
    {
    }
}
