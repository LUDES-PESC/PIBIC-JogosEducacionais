using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour {
    public int index;
    [SerializeField] private TMP_Text levelNumber;

	public void SetIndex(int index)
    {
        this.index = index;
        levelNumber.text = (index + 1).ToString();
    }
    public void OpenLevel()
    {
        print(index);
        MemoryCard.SetSelectedLevel(index);
        LoadingScreen.LoadScreen("SampleScene");
    }
}

