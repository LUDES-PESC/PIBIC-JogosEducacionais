using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureInventory : MonoBehaviour {
    [SerializeField] private GameObject littleTreasurePrefab;
    [SerializeField] private GameObject bigTreasurePrefab;
    [SerializeField] private Transform root;

    private List<TreasureUI> littleTreasuresUI = new List<TreasureUI>();
    private TreasureUI bigTreasureUI;

    private int littleTreasureIndex = 0;
    private bool gotBigTreasure;

    public void InitializeTreasureIndicator()
    {
        TreasureInfo info = TreasureMap.GetTreasureInfo();
        foreach(Treasure t in info.littleTreasures)
        {
            var go = Instantiate(littleTreasurePrefab, root);
            littleTreasuresUI.Add(go.GetComponent<TreasureUI>());
        }
        bigTreasureUI = Instantiate(bigTreasurePrefab, root).GetComponent<TreasureUI>();

    }
    public void FoundTreasure(bool bigTreasure)
    {
        if (bigTreasure)
        {
            bigTreasureUI.Get();
            gotBigTreasure = true;
        }
        else if (littleTreasureIndex < littleTreasuresUI.Count)
        {
            littleTreasuresUI[littleTreasureIndex].Get();
            littleTreasureIndex++;
        }
    }
    public void Reset()
    {
        gotBigTreasure = false;
        littleTreasureIndex = 0;
        for(int i = root.childCount - 1; i >= 0; i--)
        {
            Destroy(root.GetChild(i).gameObject);
        }
        littleTreasuresUI = new List<TreasureUI>();
        InitializeTreasureIndicator();
    }
    public Result GetResult()
    {
        Result r = new Result();
        r.bigTreasure = gotBigTreasure;
        r.littleTreasures = littleTreasureIndex;
        r.maxLittleTreasures = littleTreasuresUI.Count;
        return r;
    }
}
public struct Result
{
    public bool bigTreasure;
    public int littleTreasures;
    public int maxLittleTreasures;
    public int steps;
    public int maxSteps;
}
