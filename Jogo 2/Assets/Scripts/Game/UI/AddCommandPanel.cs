using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCommandPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

    private bool opened = false;
    private bool mouseInside = false;

	public void OpenPanel(){
		var group = GetComponent<CanvasGroup>();
		group.alpha = 1;
		group.interactable = true;
        group.blocksRaycasts = true;
        opened = true;
	}
	public void ClosePanel(){
		var group = GetComponent<CanvasGroup>();
		group.alpha = 0;
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
                print("NOT AVAILABLE");
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
        }
    }
    private void Update(){
        if(opened && !mouseInside && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))){
            ClosePanel();
        }
    }
    public void OnPointerEnter(PointerEventData eventData){mouseInside = true;}
    public void OnPointerExit(PointerEventData eventData){mouseInside = false;}
}
