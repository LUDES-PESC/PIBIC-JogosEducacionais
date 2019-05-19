using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public abstract class Block : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler{
    public static Block dragged;

    [HideInInspector] public Block previous;
    [HideInInspector] public Block next;
    [HideInInspector] public bool initialBlock;

    private Transform blockRoot;
    private RectTransform rect;

    public abstract IEnumerator Execute(Player player);

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        blockRoot = FindObjectOfType<Canvas>().transform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        CameraMovement.canBeMoved = false;
        if (previous != null)
        {
            transform.SetParent(blockRoot);
            previous.SetNextBlock(null);
            previous = null;
        }
        GetComponent<Image>().raycastTarget = false;
        dragged = this;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!initialBlock)
            rect.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        CameraMovement.canBeMoved = true;
        GetComponent<Image>().raycastTarget = true;
        dragged = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GetComponent<AudioSource>().Play();
        if (next == null)
        {
            dragged.SetPreviousBlock(this);
            SetNextBlock(dragged);
        }
        else
        {
            dragged.SetPreviousBlock(this);
            dragged.SetNextBlock(next);
            next.SetPreviousBlock(dragged);
            SetNextBlock(dragged);
        }
    }
    public void SetNextBlock(Block block)
    {
        next = block;
        if (next != null)
        {
            next.SetPreviousBlock(this);
            next.rect.anchoredPosition = Vector2.down * rect.sizeDelta.y;
        }
    }
    public void SetPreviousBlock(Block block)
    {
        previous = block;
        transform.SetParent(previous.transform);
    }
    public List<Block> GetCommandList()
    {
        if (next != null)
        {
            var list = next.GetCommandList();
            list.Add(this);
            return list;
        }
        else
        {
            var list = new List<Block>();
            list.Add(this);
            return list;
        }
    }
    protected IEnumerator ExecutionAnimation()
    {
        Color originalColor = GetComponent<Image>().color;
        GetComponent<Image>().DOColor(Color.black, Globals.TIME_BETWEEN_TURNS);
        yield return new WaitForSeconds(Globals.TIME_BETWEEN_TURNS);
        GetComponent<Image>().DOColor(originalColor, Globals.TIME_BETWEEN_TURNS);
    }
}
