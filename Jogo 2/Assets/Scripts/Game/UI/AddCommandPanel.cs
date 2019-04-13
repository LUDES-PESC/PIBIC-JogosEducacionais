using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AddCommandPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private const float OPENED_POS = 520;
    private const float CLOSED_POS = 250;
    private const float DURATION = 0.25f;

    private bool opened = false;
    private bool mouseInside = false;

    private void Start()
    {
        ClosePanel(0);
    }
    public void Toggle()
    {
        if (opened)
            ClosePanel();
        else
            OpenPanel();
    }
    public void OpenPanel(float duration = DURATION)
    {
		var group = GetComponent<CanvasGroup>();
        group.DOFade(1, duration);
        group.GetComponent<RectTransform>().DOAnchorPosX(OPENED_POS, duration);
		group.interactable = true;
        group.blocksRaycasts = true;
        opened = true;
	}
	public void ClosePanel(float duration = DURATION){
		var group = GetComponent<CanvasGroup>();
        group.DOFade(0, duration);
        group.GetComponent<RectTransform>().DOAnchorPosX(CLOSED_POS, duration);
        group.interactable = false;
		group.blocksRaycasts = false;
        opened = false;
	}
	public void AddCommand(string commandName){
		var commandPanel = FindObjectOfType<CommandPanel>();
        switch(commandName){
            case "Wait":
                commandPanel.AddCommand<WaitCommand>();
                break;
            case "Walk":
                commandPanel.AddCommand<WalkCommand>();
                break;
            case "Shoot":
                commandPanel.AddCommand<ShootCommand>();
                break;
            case "LookUp":
                commandPanel.AddCommand<LookUpCommand>();
                break;
            case "LookDown":
                commandPanel.AddCommand<LookDownCommand>();
                break;
            case "LookRight":
                commandPanel.AddCommand<LookRightCommand>();
                break;
            case "LookLeft":
                commandPanel.AddCommand<LookLeftCommand>();
                break;
            case "Dig":
                commandPanel.AddCommand<DigCommand>();
                break;
        }
    }
    /*
    private void Update(){
        if(opened && !mouseInside && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))){
            ClosePanel();
        }
    }
    */
    public void OnPointerEnter(PointerEventData eventData){mouseInside = true;}
    public void OnPointerExit(PointerEventData eventData){mouseInside = false;}
}
