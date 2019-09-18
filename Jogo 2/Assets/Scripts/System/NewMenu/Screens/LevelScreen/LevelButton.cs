using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LevelButton : MonoBehaviour {
    [SerializeField] private List<Image> stars;

    public TMP_Text number;
    public int levelIndex;
    public int worldIndex;
    
    public void SetInfo(int level, int world, List<bool> stars)
    {
        print(stars.ToString());
        number.text = (level+1).ToString();
        levelIndex = level;
        worldIndex = world;
        for(int i = 0; i < this.stars.Count; i++)
            this.stars[i].DOFade(stars[i] ? 1 : 0.25f, 0);
    }
	public void OpenLevel()
    {
        //abrir transição, etc
        print("OPENING LEVEL " + levelIndex + " OF WORLD " + worldIndex);
    }
}
