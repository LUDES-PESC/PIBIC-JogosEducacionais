using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimeAttack : MonoBehaviour {
    [Header("Important Objects")]
    [SerializeField] private List<Figure> figures;
    [SerializeField] private Timer timer;
    [SerializeField] private Grid grid;
    [SerializeField] private Text hint;
    [SerializeField] private CanvasGroup beginningText;
    [SerializeField] private CanvasGroup timeOutText;
    [SerializeField] private CanvasGroup endingText;
    [SerializeField] private CanvasGroup gotItText;
    [SerializeField] private CanvasGroup wrongText;
    [SerializeField] private Text turn;
    [SerializeField] private EndPanel endPanel;

    [Header("Game Preferences")]
    [SerializeField] private float totalTime;
    [SerializeField] private int numberOfTurns;

    [Header("Balance Adjustments")]
    [SerializeField] private float bonusTimeForCorrectAnswer;
    [SerializeField] private float lostTimeForWrongAnswer;

    [Header("Animation Values")]
    [SerializeField] private float timeToShowText;

    private const int GRID_SIZE = 8;
    private int currentFigure;
    private int currentTurn;

    private void Start()
    {
        timer.TimerEnd += TimeOut;
        StartCoroutine(c_StartLevel());
    }
    public void SubmitDrawing()
    {
        NextFigure();
    }
    private void InitializeLevel()
    {
        currentTurn = 1;
        currentFigure = Random.Range(0, figures.Count);
        turn.text = currentTurn + "/" + numberOfTurns;
        hint.text = figures[currentFigure].GetTranscription();
        timer.InitializeTimer(totalTime);
    }
    private void TimeOut()
    {
        StartCoroutine(c_ShowText(timeOutText));
        endPanel.Appear(timeToShowText);
    }
    private void Finish()
    {
        timer.StopTimer();
        StartCoroutine(c_ShowText(endingText, timeToShowText/2));
    }
    private void GotIt()
    {
        StartCoroutine(c_ShowText(gotItText));
    }
    private void Wrong()
    {
        StartCoroutine(c_ShowText(wrongText));
    }
    public void NextFigure()
    {
        if(DrawingIsCorrect())
        {
            GotIt();
            grid.EraseAllTiles();
            if (++currentTurn <= numberOfTurns)
            {
                turn.text = currentTurn + "/" + numberOfTurns;
                currentFigure = Random.Range(0, figures.Count);
                hint.text = figures[currentFigure].GetTranscription();
            }
            else
            {
                Finish();
                endPanel.Appear(timeToShowText);
            }
        }
        else
        {
            Wrong();
        }
    }
    private IEnumerator c_StartLevel()
    {
        InitializeLevel();
        yield return c_ShowText(beginningText);
        timer.StartTimer();
    }
    private bool DrawingIsCorrect()
    {
        return grid.CompareFigureAndDrawing(figures[currentFigure]) == 100;
    }
    private IEnumerator c_ShowText(CanvasGroup group, float initialDelay = 0)
    {
        yield return new WaitForSeconds(initialDelay);
        group.DOFade(0, 0);
        group.DOFade(1, timeToShowText / 4);
        group.transform.DOScale(0.5f, 0);
        group.transform.DOScale(1, timeToShowText / 2);
        yield return new WaitForSeconds(timeToShowText / 2);
        group.DOFade(0, timeToShowText / 4);
        group.transform.DOScale(0, timeToShowText / 2);
    }
}
