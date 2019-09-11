using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class WorldMap : MonoBehaviour {
    private const float DURATION = 0.25f;
    private const float OPENED_SIZE = 890;
    private const float SHOWN_Y = -40;
    private const float HIDE_Y = -740;

    [Header("World Info")]
    [SerializeField] private WorldList worldList;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image path;
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private Transform levelRoot;
    [SerializeField] private CanvasGroup group;

    private int currentWorld;

    public IEnumerator Open(int worldIndex, float duration = DURATION)
    {
        var rect = GetComponent<RectTransform>();
        var size = rect.sizeDelta;
        rect.DOAnchorPosY(SHOWN_Y, duration);
        yield return new WaitForSeconds(duration);
        group.DOFade(1, duration/2);
        rect.DOSizeDelta(new Vector2(OPENED_SIZE, size.y), duration);
        ClearPath();
        BuildPath(worldList.worlds[currentWorld].levelPositions);
        title.text = worldList.worlds[worldIndex].worldName;
        path.sprite = worldList.worlds[worldIndex].map;
        yield return new WaitForSeconds(duration);
    }
    public IEnumerator Close(float duration = DURATION)
    {
        var rect = GetComponent<RectTransform>();
        var size = GetComponent<RectTransform>().sizeDelta;
        rect.DOSizeDelta(new Vector2(220, size.y), duration);
        group.DOFade(0, duration / 2);
        yield return new WaitForSeconds(duration);
        rect.DOAnchorPosY(HIDE_Y, duration);
        yield return new WaitForSeconds(duration);
    }
    public void ChangeWorld(int bias)
    {
        StartCoroutine(Change(bias));
    }
    public IEnumerator Change(int bias)
    {
        if (currentWorld + bias < 0 || currentWorld + bias > worldList.worlds.Count - 1)
            yield break;
        var index = Mathf.Clamp(currentWorld + bias, 0, worldList.worlds.Count-1);
        yield return Close();
        currentWorld = index;
        yield return Open(index);
    }
    public void ClearPath()
    {
        for (int i = levelRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(levelRoot.GetChild(i).gameObject);
        }
    }
    public void BuildPath(List<Vector2> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            var go = Instantiate(levelPrefab, levelRoot).GetComponent<RectTransform>();
            go.anchoredPosition = positions[i];
        }
    }
}
