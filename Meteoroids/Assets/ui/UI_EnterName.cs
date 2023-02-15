using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EnterName : MonoBehaviour
{
    public TMPro.TMP_InputField playersNameInput;

    private int score;

    private void Start()
    {
        this.name = typeof(UI_EnterName).Name;

        this.playersNameInput.ActivateInputField();
    }

    public void Score(int score)
    {
        this.score = score;
    }

    public void NewHighScore()
    {
        GameObject.FindFirstObjectByType<HighScores>().add(this.playersNameInput.text, this.score);

        Globals.ShowHighScores();
    }
}
