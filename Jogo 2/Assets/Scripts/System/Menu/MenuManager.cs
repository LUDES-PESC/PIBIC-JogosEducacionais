using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    private const int firstScreen = 0;
    [SerializeField] private List<Screen> screens;
    private int currentScreen;

    private void Start()
    {
        currentScreen = firstScreen;
        foreach(Screen s in screens)
            s.Hide(0);
        screens[currentScreen].Show(0);
    }
    public void OpenScreen(int screenIndex)
    {
        screens[currentScreen].Hide();
        currentScreen = screenIndex;
        screens[currentScreen].Show();
    }
}
