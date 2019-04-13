using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class Command : MonoBehaviour, IBeginDragHandler, 
IDropHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, 
IPointerExitHandler
{
    private bool selected;
    public int index;
    public Type type;

    public abstract IEnumerator Execute(Player player);

    public virtual void InitializeCommand(int index, string name = null, Type type = null)
    {
        GetComponent<Button>().onClick.AddListener(delegate { ToggleSelect(); });
        transform.GetChild(0).GetComponent<TMP_Text>().text = index.ToString();
        transform.GetChild(2).GetComponent<TMP_Text>().text = name;
        this.type = type;
        this.index = index;
    }
    public void ToggleSelect()
    {
        CommandSelectionManager.SelectCommand(this, Input.GetKey(KeyCode.LeftControl));
    }
    public void Select()
    {
        selected = true;
        GetComponent<Image>().color = Color.gray;
    }
    public void Unselect()
    {
        selected = false;
        GetComponent<Image>().color = Color.white;
    }
    public void SetErrorColor()
    {
        GetComponent<Image>().color = Color.red;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        CommandSelectionManager.BeginSelectionMove(this);
    }
    public void OnDrop(PointerEventData eventData)
    {
        CommandSelectionManager.EndSelectionMove(this);
    }
    public void OnDrag(PointerEventData eventData){}
    public void OnEndDrag(PointerEventData eventData){}
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CommandSelectionManager.isMoving)
            transform.GetChild(3).gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (CommandSelectionManager.isMoving)
            transform.GetChild(3).gameObject.SetActive(false);
    }
}
