using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndPanel : MonoBehaviour {
    [SerializeField] private float timeToAppear;
    [SerializeField] private Image group;
    [SerializeField] private CanvasGroup menuGroup;

    public void Appear(float delay = 0)
    {
        StartCoroutine(c_Appear(delay));
    }
    private IEnumerator c_Appear(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        group.DOFillAmount(1, timeToAppear);
        group.raycastTarget = true;
        yield return new WaitForSeconds(timeToAppear);
        menuGroup.DOFade(1, timeToAppear);
        menuGroup.blocksRaycasts = true;
        menuGroup.interactable = true;
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OpenMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
