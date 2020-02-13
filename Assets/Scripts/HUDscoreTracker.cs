using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDscoreTracker : MonoBehaviour
{
    private int score = 0;
    private GUIStyle guiFontStyle = new GUIStyle();

    public void addScore(int value)
    {
        score += value;
    }

    void Start()
    {
        guiFontStyle.fontSize = 48;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(1200, 50, 200, 100), score.ToString(), guiFontStyle);
    }
}
