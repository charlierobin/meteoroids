using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HighScores : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scores;

    private void Start()
    {
        this.name = typeof(UI_HighScores).Name;

        HighScores hs = GameObject.FindAnyObjectByType<HighScores>();

        string s = "";

        foreach (HighScoreEntry entry in hs.data.entries)
        {
            s = s + entry.name + " / " + entry.score + "\n";
        }

        this.scores.text = s;
    }
}
