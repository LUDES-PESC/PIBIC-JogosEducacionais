using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MapOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float OPENED_X = -761;
    private const float CLOSED_X = -18f;
    private const float ARROW_OPENED_ROTATION = 180;
    private const float ARROW_CLOSED_ROTATION = 0;
    private const float DURATION = 0.5f;

    [SerializeField] private RectTransform arrow;
    private bool opened;
    private bool mouseInside;

    private void Start()
    {
        CloseMap(0);
    }
    private void Update()
    {
        if (opened && !mouseInside && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            CloseMap();
        }
    }
    public void LoadMap() { }
    public void ToggleMap()
    {
        if (opened)
            CloseMap();
        else
            OpenMap();
    }
	public void OpenMap(float duration = DURATION) {
        arrow.DORotate(new Vector3(0,0, ARROW_OPENED_ROTATION), duration);
        GetComponent<RectTransform>().DOAnchorPosX(OPENED_X, duration);
        opened = true;
    }
    public void CloseMap(float duration = DURATION)
    {
        arrow.DORotate(new Vector3(0, 0, ARROW_CLOSED_ROTATION), duration);
        GetComponent<RectTransform>().DOAnchorPosX(CLOSED_X, duration);
        opened = false;
    }
    public void OnPointerEnter(PointerEventData eventData) { mouseInside = true; }
    public void OnPointerExit(PointerEventData eventData) { mouseInside = false; }
}
