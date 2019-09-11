using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    private const int FIRST_SCREEN = 0;
    [SerializeField] private List<Screen> screens;

    private int currentScreen;

	private void Start () {
        foreach (Screen s in screens)
            StartCoroutine(s.Close(0));
        currentScreen = FIRST_SCREEN;
        StartCoroutine(screens[FIRST_SCREEN].Open());
	}
    public IEnumerator OpenScreen(int index)
    {
        yield return screens[currentScreen].Close();
        currentScreen = index;
        yield return screens[currentScreen].Open();
    }
}
