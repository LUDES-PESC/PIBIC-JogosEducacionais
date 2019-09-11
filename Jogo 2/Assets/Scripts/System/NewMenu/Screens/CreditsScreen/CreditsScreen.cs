using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditsScreen : Screen
{
    private const float OPENED_SIZE = 600;
    private const float CLOSED_SIZE = 220;

    [SerializeField] private RectTransform title;
    [SerializeField] private RectTransform backButton;
    [SerializeField] private RectTransform scroll;
    [SerializeField] private CanvasGroup canvasGroup;

    public override IEnumerator Open(float duration)
    {
        title.DOAnchorPosY(-80, duration);
        backButton.DOAnchorPosY(95, duration);
        var size = scroll.sizeDelta;
        scroll.DOSizeDelta(new Vector2(size.x, OPENED_SIZE), duration);
        canvasGroup.DOFade(1, duration);
        yield return null;
    }
    public override IEnumerator Close(float duration)
    {
        title.DOAnchorPosY(80, duration);
        backButton.DOAnchorPosY(-95, duration);
        var size = scroll.sizeDelta;
        scroll.DOSizeDelta(new Vector2(size.x, CLOSED_SIZE), duration);
        canvasGroup.DOFade(0, duration);
        yield return null;
    }
    public override void OnOpen()
    {
    }
    public override void OnClose()
    {
    }
}
