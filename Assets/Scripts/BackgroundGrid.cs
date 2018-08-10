using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundGrid : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(BackgroundAnimation());
    }
    private IEnumerator BackgroundAnimation()
    {
        while (true)
        {
            int rnd = Random.Range(0, transform.childCount);
            transform.GetChild(rnd).GetComponent<Tile>().Toggle();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
