﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMap : MonoBehaviour {
    public static Dictionary<Vector2Int, Treasure> treasures = new Dictionary<Vector2Int, Treasure>();

    public static void LoadMap()
    {
        treasures = new Dictionary<Vector2Int, Treasure>();
        var tr = FindObjectsOfType<Treasure>();
        Debug.Log(tr.Length);
        foreach (Treasure t in tr)
        {
            t.ResetState();
            Debug.Log("Add " + t.name +" at " + t.position.x + "/" + t.position.y);
            treasures.Add(t.position, t);
        }
    }
    public static Treasure TreasureIn(Vector2Int position)
    {
        if (treasures.ContainsKey(position))
            return treasures[position];
        return null;
    }
    public static TreasureInfo GetTreasureInfo()
    {
        TreasureInfo ti = new TreasureInfo();
        foreach(Treasure t in treasures.Values)
        {
            if (t.bigTreasure)
                ti.bigTreasure = t;
            else
                ti.littleTreasures.Add(t);
        }
        return ti;
    }
}
public class TreasureInfo
{
    public Treasure bigTreasure;
    public List<Treasure> littleTreasures = new List<Treasure>();
}
