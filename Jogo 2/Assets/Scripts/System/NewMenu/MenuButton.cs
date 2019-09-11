using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButton : MonoBehaviour {
    private const float NORMAL_Y = 150;
    private const float HIDE_Y = -170;
    private const float MOVE_TIME = 0.25f;

    [SerializeField] private int screenIndex;
    
    public void Action()
    {
        StartCoroutine(FindObjectOfType<MainMenu>().OpenScreen(screenIndex));
    }
    public void Show(float duration = MOVE_TIME)
    {
        GetComponent<RectTransform>().DOAnchorPosY(NORMAL_Y, duration);
    }
    public void Hide(float duration = MOVE_TIME)
    {
        GetComponent<RectTransform>().DOAnchorPosY(HIDE_Y, duration);
    }
}
