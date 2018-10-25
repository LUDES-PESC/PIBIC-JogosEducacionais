using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class Command : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler{
    [SerializeField] private TMP_Text commandName;
    [SerializeField] private bool selected;
    public int index;

    public abstract void Execute(Player player);

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ToggleSelect(); });
    }
    public virtual void SetCommandName(string name)
    {
        transform.GetChild(2).GetComponent<TMP_Text>().text = name;
    }
    public void SetCommandIndex(int commandIndex)
    {
        this.index = commandIndex;
        transform.GetChild(0).GetComponent<TMP_Text>().text = index.ToString();
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
    public virtual void Delete()
    {
        //DELETE
        //ver se esta listada como selected -> pode dar problema?
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("BEGIN DRAG");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("DRAGGING: " + eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("END DRAG");
    }
}
