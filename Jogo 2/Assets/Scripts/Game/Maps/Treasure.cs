﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
    public bool bigTreasure;
    public bool found;
    public Vector2Int position;

    public void Start()
    {
        position = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
    }
    public void Find()
    {
        found = true;
        FindObjectOfType<TreasureInventory>().FoundTreasure(bigTreasure);
    }
    public void ResetState()
    {
        found = false;
    }
}
