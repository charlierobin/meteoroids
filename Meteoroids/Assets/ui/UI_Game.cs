using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Game : MonoBehaviour
{
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI lives;

    public void Score(int amount)
    {
        this.name = typeof(UI_Game).Name;

        this.score.text = amount.ToString();
    }

    public void Lives(int newValue)
    {
        this.lives.text = "Lives: " + newValue.ToString();
    }
}
