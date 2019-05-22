using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TutorialPanel : MonoBehaviour {
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image image;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private RectTransform panel;

	public void OpenTutorial(List<TutorialInfo> info)
    {
        CameraMovement.canBeMoved = false;
        group.DOFade(1, 0.5f);
        group.interactable = true;
        group.blocksRaycasts = true;
        panel.DOAnchorPosY(-680, 0);
        panel.DOAnchorPosY(40, 0.5f);

        title.text = info[0].title;
        description.text = info[0].description;
        image.sprite = info[0].image;
    }
    public void Hide()
    {
        CameraMovement.canBeMoved = true;
        group.DOFade(0, 0.5f);
        group.interactable = false;
        group.blocksRaycasts = false;
        panel.DOAnchorPosY(panel.anchoredPosition.y - 720, 0.5f);
    }
}
[System.Serializable]
public class TutorialInfo
{
    public string title;
    public string description;
    public Sprite image;
}
