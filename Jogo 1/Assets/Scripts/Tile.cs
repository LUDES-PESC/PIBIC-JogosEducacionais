using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    private static Color activeColor = Color.black;
    private static Color inactiveColor = Color.white;
    private static Color disableColor = Color.red;

    private Image image;
    private bool mode;

    public void Toggle()
    {
        if (image == null)
            image = GetComponent<Image>();
        mode = !mode;
        Color targetColor = mode ? activeColor : inactiveColor;
        image.DOColor(targetColor, 0.5f);
    }
    public IEnumerator Disable()
    {
        if (image == null)
            image = GetComponent<Image>();
        image.color = disableColor;
        yield return new WaitForSeconds(0.07f);
        mode = false;
        image.color = inactiveColor;
    }
    public bool GetMode()
    {
        return mode;
    }
}
