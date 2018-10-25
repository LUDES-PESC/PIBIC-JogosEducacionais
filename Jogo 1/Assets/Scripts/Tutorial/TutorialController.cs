using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialController : MonoBehaviour {

    [SerializeField] private List<CanvasGroup> slides;
    [SerializeField] private CanvasGroup controller;

    private int currentSlide;

    private void Start()
    {
        HideController();
    }
    public void StartSlides()
    {
        ShowController();
        currentSlide = 0;
        ShowSlide(0);
    }
    public void Next()
    {
        if (currentSlide + 1 < slides.Count)
            ShowSlide(1);
        else
            HideController();
    }
    public void Back()
    {
        if (currentSlide - 1 >= 0)
            ShowSlide(-1);
    }
    public void HideSlides()
    {
        foreach(CanvasGroup group in slides)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
    private void ShowController()
    {
        controller.DOFade(1, 0.5f);
        controller.interactable = true;
        controller.blocksRaycasts = true;
    }
    private void HideController()
    {
        HideSlides();
        controller.DOFade(0, 0.5f);
        controller.interactable = false;
        controller.blocksRaycasts = false;
    }
    private void ShowSlide(int bias)
    {
        slides[currentSlide].DOFade(0, 0.5f);
        slides[currentSlide].interactable = false;
        slides[currentSlide].blocksRaycasts = false;
        currentSlide += bias;
        slides[currentSlide].DOFade(1, 0.5f);
        slides[currentSlide].interactable = true;
        slides[currentSlide].blocksRaycasts = true;
    }
}
