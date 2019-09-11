using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartScreen : Screen
{
    [SerializeField] private List<MenuButton> buttons;
    [SerializeField] private RectTransform title;
    [SerializeField] private CanvasGroup canvasGroup;

    public override IEnumerator Open(float duration)
    {
        title.DOAnchorPosY(-210, duration);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Show(duration/2 + 0.1f * i);
            yield return new WaitForSeconds(duration/2);
        }
    }
    public override IEnumerator Close(float duration)
    {
        title.DOAnchorPosY(210, duration);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Hide(duration/2 + 0.1f * i);
            yield return new WaitForSeconds(duration/2);
        }
    }
    public override void OnOpen()
    {
    }
    public override void OnClose()
    {
    }
}
