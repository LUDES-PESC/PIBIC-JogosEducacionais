using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour {
    [SerializeField] private Image fillAmount;
    [SerializeField] private Text timeNumber;
    private float maxTime;
    private float currentTime;
    private Coroutine timer;

    public delegate void OnTimerEnd();
    public event OnTimerEnd TimerEnd;

    public void InitializeTimer(float time)
    {
        currentTime = time;
        maxTime = time;
        timeNumber.text = ((int)currentTime).ToString();
        fillAmount.DOFillAmount(currentTime / maxTime, Time.deltaTime);
    }
	public void StartTimer()
    {
        timer = StartCoroutine(c_StartTimer());
    }
    public void StopTimer()
    {
        StopCoroutine(timer);
    }
    private IEnumerator c_StartTimer()
    {
        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeNumber.text = ((int) currentTime).ToString();
            fillAmount.DOFillAmount(currentTime / maxTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        TimerEnd();
    }
}
