using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public UI_StartScreen startUIPrefab;

    public Game gamePrefab;
    public UI_Game gameUIPrefab;

    public UI_HighScores highscoresUIPrefab;

    public UI_EnterName enterNameUIPrefab;

    private static Globals instance;

    private void Start()
    {
        this.name = typeof(Globals).Name;

        Globals.instance = this;

        Instantiate(this.startUIPrefab);
    }

    public static void StartGame()
    {
        Destroy(GameObject.FindFirstObjectByType<UI_StartScreen>()?.gameObject);

        Destroy(GameObject.FindFirstObjectByType<UI_HighScores>()?.gameObject);

        Instantiate(Globals.instance.gameUIPrefab);

        Instantiate(Globals.instance.gamePrefab);
    }

    public static void ShowHighScores()
    {
        Destroy(GameObject.FindFirstObjectByType<UI_StartScreen>()?.gameObject);

        Destroy(GameObject.FindFirstObjectByType<UI_EnterName>()?.gameObject);

        Instantiate(Globals.instance.highscoresUIPrefab);
    }

    public static void ShowEnterNameScreen(int score)
    {
        Instantiate(Globals.instance.enterNameUIPrefab).Score(score);
    }
}
